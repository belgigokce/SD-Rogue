using RogueLib.Engine;
using RogueLib.Dungeon;
using RogueLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

using TileSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RlGameNS;

public class Level : Scene
{
    protected string? _map;
    protected int _senseRadius = 4;

    protected TileSet _walkables = new();
    protected TileSet _floor = new();
    protected TileSet _tunnel = new();
    protected TileSet _door = new();
    protected TileSet _decor = new();
    protected TileSet _discovered = new();
    protected TileSet _inFov = new();

    // ✅ FIX: Enemy -> Character system
    private List<Character> _enemies = new();
    private EnemyFactory _factory = new();
    private int _difficulty = 1;

    private UIManager _ui = UIManager.Instance;

    public Level(Player p, string map, Game game)
    {
        if (game == null || p == null || map == null)
            throw new ArgumentNullException("game, player, or map cannot be null");

        _player = p;
        _player.Pos = new Vector2(4, 12);

        _map = map;
        _game = game;

        initMapTileSets(map);
        updateDiscovered();
        registerCommandsWithScene();
    }

    // ---------------- UPDATE ----------------
    public override void Update()
    {
        _player!.Update();

        foreach (var enemy in _enemies)
            enemy.Update();
    }

    // ---------------- DRAW ----------------
    public override void Draw(IRenderWindow disp)
    {
        var tilesToDraw = new TileSet(_decor);
        tilesToDraw.IntersectWith(_discovered);
        tilesToDraw.UnionWith(_inFov);

        disp.fDraw(tilesToDraw, _map!, ConsoleColor.Gray);

        var rng = new Random();
        if (_player.Turn % 5 == 0)
            _player._color = (ConsoleColor)rng.Next(10, 16);

        _player!.Draw(disp);

        drawItems(disp);
        drawEnemies(disp);

        disp.Draw(_player.HUD, new Vector2(0, 24), ConsoleColor.Green);
    }

    // ---------------- COMMANDS ----------------
    public override void DoCommand(Command command)
    {
        if (command.Name == "up") MovePlayer(Vector2.N);
        else if (command.Name == "down") MovePlayer(Vector2.S);
        else if (command.Name == "left") MovePlayer(Vector2.W);
        else if (command.Name == "right") MovePlayer(Vector2.E);
        else if (command.Name == "quit") _levelActive = false;
        else if (command.Name == "attack") AttackNearestEnemy();
    }

    // ---------------- MOVEMENT ----------------
    public void MovePlayer(Vector2 delta)
    {
        var newPos = _player!.Pos + delta;

        if (_walkables.Contains(newPos))
        {
            var oldPos = _player.Pos;
            _player.Pos = newPos;

            _walkables.Remove(newPos);
            _walkables.Add(oldPos);

            updateDiscovered();
        }
    }

    // ---------------- ENEMY SPAWN ----------------
    private void spawnEnemies()
    {
        string[] spawnList = { "goblin", "goblin", "orc", "goblin", "troll" };
        var walkableList = _walkables.ToList();
        var rng = new Random();

        foreach (var type in spawnList)
        {
            Character enemy = _factory.CreateEnemy(type, _difficulty);

            if (walkableList.Count > 0)
            {
                int idx = rng.Next(walkableList.Count);
                enemy.Pos = walkableList[idx];
                walkableList.RemoveAt(idx);
            }

            _enemies.Add(enemy);
        }
    }

    // ---------------- DRAW ENEMIES ----------------
    private void drawEnemies(IRenderWindow disp)
    {
        foreach (var enemy in _enemies)
        {
            if (_inFov.Contains(enemy.Pos))
                disp.Draw(enemy.Glyph, enemy.Pos, ConsoleColor.Red);
        }

        _ui.Draw(disp);
    }

    // ---------------- ATTACK SYSTEM ----------------
    private void AttackNearestEnemy()
    {
        var adjacent = new[] { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

        foreach (var dir in adjacent)
        {
            var checkPos = _player!.Pos + dir;
            var enemy = _enemies.FirstOrDefault(e => e.Pos == checkPos);

            if (enemy != null)
            {
                int dmg = _player.AttackEnemy(enemy);

                _ui.DisplayMessage($"You hit for {dmg} damage!");

                RemoveIfDead(enemy);
                return;
            }
        }

        _ui.DisplayMessage("Nothing to attack nearby.");
    }

    // ---------------- REMOVE DEAD ----------------
    private void RemoveIfDead(Character enemy)
    {
        if (enemy.GetHealth() <= 0)
        {
            _enemies.Remove(enemy);
            _ui.Update("enemyDied", 25);
        }
    }

    // ---------------- MAP INIT ----------------
    private void initMapTileSets(string map)
    {
        _floor = new();
        _tunnel = new();
        _door = new();
        _decor = new();

        foreach (var (c, p) in Vector2.Parse(map))
        {
            if (c == '.') _floor.Add(p);
            else if (c == '+') _door.Add(p);
            else if (c == '#') _tunnel.Add(p);
            else if (c != ' ') _decor.Add(p);
        }

        _walkables = _floor.Union(_tunnel).Union(_door).ToHashSet();
    }

    // ---------------- COMMAND REG ----------------
    private void registerCommandsWithScene()
    {
        spawnEnemies();

        RegisterCommand(ConsoleKey.UpArrow, "up");
        RegisterCommand(ConsoleKey.W, "up");
        RegisterCommand(ConsoleKey.K, "up");

        RegisterCommand(ConsoleKey.DownArrow, "down");
        RegisterCommand(ConsoleKey.S, "down");
        RegisterCommand(ConsoleKey.J, "down");

        RegisterCommand(ConsoleKey.LeftArrow, "left");
        RegisterCommand(ConsoleKey.A, "left");
        RegisterCommand(ConsoleKey.H, "left");

        RegisterCommand(ConsoleKey.RightArrow, "right");
        RegisterCommand(ConsoleKey.D, "right");
        RegisterCommand(ConsoleKey.L, "right");

        RegisterCommand(ConsoleKey.Q, "quit");
        RegisterCommand(ConsoleKey.Spacebar, "attack");
    }

    private void drawItems(IRenderWindow disp) { }

    public void QuitLevel() => _levelActive = false;

    // ---------------- FOV ----------------
    protected void updateDiscovered()
    {
        _inFov = fovCalc(_player!.Pos, _senseRadius);

        _discovered ??= new TileSet();
        _discovered.UnionWith(_inFov);
    }

    protected TileSet fovCalc(Vector2 pos, int sens)
        => Vector2.getAllTiles()
            .Where(t => (pos - t).RookLength < sens)
            .ToHashSet();
}