using RogueLib.Engine;
using RogueLib.Dungeon;
using RogueLib.Utilities;
using TileSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RlGameNS;

public class Level : Scene
{
    protected string? _map;
    protected int _senseRadius = 4;

    protected TileSet _walkables;
    protected TileSet _floor;
    protected TileSet _tunnel;
    protected TileSet _door;
    protected TileSet _decor;
    protected TileSet _discovered;
    protected TileSet _inFov;

    // Vans: Factory Pattern — enemies are created through EnemyFactory, not with 'new' directly
    private List<Character> _enemies = new();
    private EnemyFactory _factory = new();
    private int _difficulty = 1;

    // Vans: Observer Pattern — singleton UIManager watches all enemies for combat events
    private UIManager _ui = UIManager.Instance;

    public Level(Player p, string map, Game game)
    {
        if (game == null || p == null || map == null)
            throw new ArgumentNullException("game, player, or map cannot be null");

        _player = p;
        _player.Pos = new Vector2(4, 12);
        _map = map;
        _game = game;   // Vans: Fixed original bug — was "_game = _game" (assigned to itself)

        initMapTileSets(map);
        updateDiscovered();
        registerCommandsWithScene();
    }

    protected void updateDiscovered()
    {
        _inFov = fovCalc(_player!.Pos, _senseRadius);
        if (_discovered is null)
            _discovered = new TileSet();
        _discovered.UnionWith(_inFov);
    }

    protected TileSet fovCalc(Vector2 pos, int sens)
        => Vector2.getAllTiles().Where(t => (pos - t).RookLength < sens).ToHashSet();

    public override void Update()
    {
        _player!.Update();

        // Vans: Each enemy takes its turn after the player
        foreach (var enemy in _enemies)
            enemy.Update();
    }

    public override void Draw(IRenderWindow? disp)
    {
        var tilesToDraw = new TileSet(_decor);
        tilesToDraw.IntersectWith(_discovered);
        tilesToDraw.UnionWith(_inFov);

        disp.fDraw(tilesToDraw, _map, ConsoleColor.Gray);

        var rng = new Random();
        if (_player.Turn % 5 == 0)
            _player._color = (ConsoleColor)rng.Next(10, 16);

        _player!.Draw(disp);
        drawItems(disp);
        drawEnemies(disp);
        disp.Draw(_player.HUD, new Vector2(0, 24), ConsoleColor.Green);
    }

    public override void DoCommand(Command command)
    {
        if (command.Name == "up") MovePlayer(Vector2.N);
        else if (command.Name == "down") MovePlayer(Vector2.S);
        else if (command.Name == "left") MovePlayer(Vector2.W);
        else if (command.Name == "right") MovePlayer(Vector2.E);
        else if (command.Name == "quit") _levelActive = false;
        // Vans: Space bar triggers adjacent melee / ranged / magic attack via Strategy Pattern
        else if (command.Name == "attack") AttackNearestEnemy();
    }

    private void drawItems(IRenderWindow disp) { }

    private void drawEnemies(IRenderWindow disp)
    {
        foreach (var enemy in _enemies)
        {
            // Vans: Only render enemies inside the player's field of view
            if (_inFov.Contains(enemy.Pos))
                enemy.Draw(disp);
        }
        // Vans: Let UIManager draw its pending combat message (Observer Pattern output)
        _ui.Draw(disp);
    }

    private void initMapTileSets(string map)
    {
        _floor = new TileSet();
        _tunnel = new TileSet();
        _door = new TileSet();
        _decor = new TileSet();

        foreach (var (c, p) in Vector2.Parse(map))
        {
            if (c == '.') _floor.Add(p);
            else if (c == '+') _door.Add(p);
            else if (c == '#') _tunnel.Add(p);
            else if (c != ' ') _decor.Add(p);
        }

        _walkables = _floor.Union(_tunnel).Union(_door).ToHashSet();
    }

    private void registerCommandsWithScene()
    {
        // Vans: Spawn enemies first so _walkables is ready for placement
        spawnEnemies();

        RegisterCommand(ConsoleKey.UpArrow, "up");
        RegisterCommand(ConsoleKey.W, "up");
        RegisterCommand(ConsoleKey.K, "up");

        RegisterCommand(ConsoleKey.DownArrow, "down");
        RegisterCommand(ConsoleKey.S, "down");
        RegisterCommand(ConsoleKey.J, "down");

        RegisterCommand(ConsoleKey.LeftArrow, "left");   // Vans: Fixed — was DownArrow
        RegisterCommand(ConsoleKey.A, "left");
        RegisterCommand(ConsoleKey.H, "left");

        RegisterCommand(ConsoleKey.RightArrow, "right");  // Vans: Fixed — was DownArrow
        RegisterCommand(ConsoleKey.D, "right");
        RegisterCommand(ConsoleKey.L, "right");

        RegisterCommand(ConsoleKey.Q, "quit");

        // Vans: Space triggers attack command — wired to Strategy Pattern via AttackNearestEnemy()
        RegisterCommand(ConsoleKey.Spacebar, "attack");
    }

    public void MovePlayer(Vector2 delta)
    {
        var newPos = _player!.Pos + delta;
        if (_walkables.Contains(newPos))
        {
            var oldPos = _player!.Pos;
            _player!.Pos = newPos;
            _walkables.Remove(newPos);
            _walkables.Add(oldPos);
            updateDiscovered();
        }
    }

    public void QuitLevel() => _levelActive = false;

    // Vans: Factory Pattern — EnemyFactory creates each enemy; we just place and register them
    private void spawnEnemies()
    {
        string[] spawnList = { "goblin", "goblin", "orc", "goblin", "troll" };
        var walkableList = _walkables.ToList();
        var rng = new Random();

        foreach (var type in spawnList)
        {
            var enemy = _factory.CreateEnemy(type, _difficulty);

            // Vans: Pick a random walkable tile that isn't already occupied
            if (walkableList.Count > 0)
            {
                int idx = rng.Next(walkableList.Count);
                enemy.Pos = walkableList[idx];
                walkableList.RemoveAt(idx);
            }

            // Vans: Observer Pattern — UIManager subscribes to every enemy's events
            enemy.RegisterObserver(_ui);
            _enemies.Add(enemy);
        }
    }

    // Vans: Strategy Pattern in action — damage calculation is delegated to _attackStrategy
    private void AttackNearestEnemy()
    {
        var adjacent = new[] { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

        foreach (var dir in adjacent)
        {
            var checkPos = _player!.Pos + dir;
            var enemy = _enemies.FirstOrDefault(e => e.Pos == checkPos);

            if (enemy != null)
            {
                // Vans: Player's current AttackStrategy decides how much damage is dealt
                int dmg = _player.AttackEnemy(enemy);
                _ui.DisplayMessage($"You hit for {dmg} damage!");
                RemoveIfDead(enemy);
                return;
            }
        }
        _ui.DisplayMessage("Nothing to attack nearby.");
    }

    // Vans: Clean up dead enemies and award XP via the Observer
    private void RemoveIfDead(Enemy enemy)
    {
        if (enemy.GetHealth() <= 0)
        {
            _enemies.Remove(enemy);
            // Vans: Broadcast "enemyDied" so UIManager shows the XP message
            _ui.Update("enemyDied", 25);
        }
    }
}