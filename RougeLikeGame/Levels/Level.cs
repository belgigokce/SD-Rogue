using System;
using System.Collections.Generic;
using System.Linq;
using RogueLib.Dungeon;
using RogueLib.Dungeon.Tiles;
using RogueLib.Engine;
using RogueLib.Utilities;
using TileSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RlGameNS
{
    public class Level : Scene
    {
        // ---- config ----
        protected string? _map;
        protected int _senseRadius = 4;

        // ---- tile registry (maps coordinates to Tile objects) ----
        protected Dictionary<Vector2, Tile> _tileRegistry = new();

        // ---- tile sets ----
        protected TileSet _walkables = new();
        protected TileSet _floor = new();
        protected TileSet _tunnel = new();
        protected TileSet _door = new();
        protected TileSet _decor = new();
        protected TileSet _discovered = new();
        protected TileSet _inFov = new();

        // ---- entities ----
        private List<Item> _items = new();
        private List<Character> _enemies = new();
        private EnemyFactory _factory = new();
        private int _difficulty = 1;
        private UIManager _ui = UIManager.Instance;

        // -----------------------------------------------------------------------
        public Level(Player p, string map, Game game)
        {
            if (game == null || p == null || map == null)
                throw new ArgumentNullException("game, player, or map cannot be null");

            _player = p;
            _player.Pos = new Vector2(4, 12);
            _map = map;
            _game = game;

            initMapTileSets(map);

            // FIX: always spawn 3–7 items so the floor is never empty
            SpawnItems(new Random().Next(3, 8));
            SpawnEnemies();
            updateDiscovered();
            registerCommandsWithScene();
        }

        // -----------------------------------------------------------------------
        public override void Update()
        {
            _player!.Update();

            // Give every enemy an update tick (Troll regenerates here, etc.)
            foreach (var enemy in _enemies)
                enemy.Update();
        }

        // -----------------------------------------------------------------------
        public override void Draw(IRenderWindow disp)
        {
            var tilesToDraw = new TileSet(_decor);
            tilesToDraw.IntersectWith(_discovered);
            tilesToDraw.UnionWith(_inFov);

            disp.fDraw(tilesToDraw, _map!, ConsoleColor.Gray);

            // Colour flicker effect every 5 turns
            var rng = new Random();
            if (_player!.Turn % 5 == 0)
                _player._color = (ConsoleColor)rng.Next(10, 16);

            _player!.Draw(disp);
            drawItems(disp);
            drawEnemies(disp);    // also calls _ui.Draw at the end

            disp.Draw(_player.HUD, new Vector2(0, 24), ConsoleColor.Green);
        }

        // -----------------------------------------------------------------------
        public override void DoCommand(Command command)
        {
            if (command.Name == "up") MovePlayer(Vector2.N);
            else if (command.Name == "down") MovePlayer(Vector2.S);
            else if (command.Name == "left") MovePlayer(Vector2.W);
            else if (command.Name == "right") MovePlayer(Vector2.E);
            else if (command.Name == "attack") AttackNearestEnemy();
            else if (command.Name == "quit") _levelActive = false;
        }

        // -----------------------------------------------------------------------
        public void MovePlayer(Vector2 delta)
        {
            Vector2 newPos = _player!.Pos + delta;

            // ---- bump-attack any enemy at the target cell ----
            var targetEnemy = _enemies.FirstOrDefault(e => e.Pos == newPos);
            if (targetEnemy != null)
            {
                int dmg = _player.AttackEnemy(targetEnemy);
                _ui.DisplayMessage($"You hit for {dmg} damage!");

                if (targetEnemy.GetHealth() <= 0)
                {
                    _player.GetExp(25);
                    _enemies.Remove(targetEnemy);
                    _ui.Update("enemyDied", 25);
                }
                else
                {
                    // Enemy retaliates
                    _player.TakeDamage(targetEnemy.GetAttackPower());
                    _ui.DisplayMessage($"Enemy hits back!  HP: {_player.CurrentHealth}/{_player.MaxHealth}");

                    if (_player.CurrentHealth <= 0)
                        _levelActive = false;
                }

                updateDiscovered();
                return;
            }

            // ---- open a closed door ----
            if (_tileRegistry.TryGetValue(newPos, out Tile? targetTile))
            {
                if (targetTile is DoorTile door && !door.IsOpen)
                {
                    door.Open();
                    _walkables.Add(newPos);
                    _ui.DisplayMessage("You open the door.");
                    updateDiscovered();
                    return;
                }
            }

            // ---- normal movement ----
            if (_walkables.Contains(newPos))
            {
                _player.Pos = newPos;

                // Pick up any item on this tile
                var steppedOnItem = _items.FirstOrDefault(i => i.Pos == newPos);
                if (steppedOnItem != null)
                {
                    steppedOnItem.ApplyTo(_player);
                    _ui.DisplayMessage($"You picked up {steppedOnItem.ItemName}!");
                    _items.Remove(steppedOnItem);
                }

                if (_tileRegistry.TryGetValue(newPos, out Tile? steppingOn))
                    steppingOn.SetTileSpace(1);

                updateDiscovered();
            }
        }

        // -----------------------------------------------------------------------
        // Space-bar attack: hits the first enemy found in an adjacent cell
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

                    if (enemy.GetHealth() <= 0)
                    {
                        _player.GetExp(25);
                        _enemies.Remove(enemy);
                        _ui.Update("enemyDied", 25);
                    }
                    return;
                }
            }

            _ui.DisplayMessage("Nothing to attack nearby.");
        }

        // -----------------------------------------------------------------------
        // Spawns a fixed roster of enemies onto random floor tiles
        private void SpawnEnemies()
        {
            string[] roster = { "goblin", "goblin", "orc", "goblin", "troll" };
            var floorList = _floor.ToList();
            var rng = new Random();

            foreach (var type in roster)
            {
                if (floorList.Count == 0) break;

                Character enemy = _factory.CreateEnemy(type, _difficulty);
                int idx = rng.Next(floorList.Count);
                enemy.Pos = floorList[idx];
                floorList.RemoveAt(idx);
                _enemies.Add(enemy);
            }
        }

        // -----------------------------------------------------------------------
        // FIX: rng.Next(4) guarantees every slot spawns one of the 4 item types.
        // Previously rng.Next(10) left 60 % of slots empty and could spawn 0 items.
        private void SpawnItems(int count)
        {
            var rng = new Random();
            var validSpots = _floor.ToList();

            for (int i = 0; i < count; i++)
            {
                if (validSpots.Count == 0) break;

                int index = rng.Next(validSpots.Count);
                Vector2 spawnPos = validSpots[index];
                validSpots.RemoveAt(index);

                switch (rng.Next(4))  // always picks one of the 4 types
                {
                    case 0: _items.Add(new Gold(i, spawnPos, rng.Next(10, 50))); break;
                    case 1: _items.Add(new Armour(i, spawnPos, rng.Next(1, 5))); break;
                    case 2: _items.Add(new Weapon(i, spawnPos, rng.Next(3, 10))); break;
                    case 3: _items.Add(new Potion(i, spawnPos, rng.Next(5, 10))); break;
                }
            }
        }

        // -----------------------------------------------------------------------
        private void drawItems(IRenderWindow disp)
        {
            foreach (var item in _items)
                if (_inFov.Contains(item.Pos))
                    item.Draw(disp);
        }

        private void drawEnemies(IRenderWindow disp)
        {
            foreach (var enemy in _enemies)
                if (_inFov.Contains(enemy.Pos))
                    disp.Draw(enemy.Glyph, enemy.Pos, ConsoleColor.Red);

            _ui.Draw(disp); // draws the pending UI message on row 23
        }

        // -----------------------------------------------------------------------
        // Builds all tile sets and the tile registry from the raw map string
        private void initMapTileSets(string map)
        {
            int idCounter = 0;
            _floor = new();
            _tunnel = new();
            _door = new();
            _decor = new();
            _tileRegistry = new();

            foreach (var (c, p) in Vector2.Parse(map))
            {
                Tile? newTile = null;

                if (c == '.') { _floor.Add(p); }
                else if (c == '#') { _tunnel.Add(p); _decor.Add(p); }
                else if (c == '+') { newTile = new DoorTile(idCounter++); _door.Add(p); _decor.Add(p); }
                else if (c == 'E') { newTile = new ExitTile(idCounter++); _floor.Add(p); }
                else if (c != ' ') { _decor.Add(p); }

                if (newTile != null)
                {
                    newTile.SetPosition(p);
                    _tileRegistry.Add(p, newTile);
                }
            }

            _walkables = _floor.Union(_tunnel).Union(_door).ToHashSet();
        }

        // -----------------------------------------------------------------------
        private void registerCommandsWithScene()
        {
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

            RegisterCommand(ConsoleKey.Spacebar, "attack");
            RegisterCommand(ConsoleKey.Q, "quit");
        }

        // -----------------------------------------------------------------------
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

        public void QuitLevel() => _levelActive = false;
    }
}