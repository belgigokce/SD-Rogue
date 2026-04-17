using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using RogueLib.Dungeon;

using RogueLib.Engine;
using RogueLib.Utilities;
using TileSet = System.Collections.Generic.HashSet<Vector2>;

namespace RlGameNS;

// -----------------------------------------------------------------------
// The Level is the model, all the game world objects live in the model. 
// player input updates the model, the model updates the view, and the 
// controller runs the whole thing. 
//
// Scene is the base class for all game scenes (levels). Scene is an 
// abstract class that implements IDrawable and ICommandable. 
// 
// A dungeon level is a collection or rooms and tunnels in a 78x25 grid. 
// each tile is at a point, or grid location, represented by a Vector2. 
// 
// *TileSets* are HashSets of grid points, TileSets can be used to tell
// GameScreen what tiles to draw. TileSets can be combined with Union and 
// Intersect to create complex tile sets.
// -----------------------------------------------------------------------
public class Level : Scene
{
    // ---- level config ---- 
    protected string? _map;
    protected int _senseRadius = 4;

    // --- Tile Registry (The Bridge) ---
    // This Dictionary maps coordinates to actual Tile objects                  //Updated by BG
    protected Dictionary<Vector2, Tile> _tileRegistry = new Dictionary<Vector2, Tile>();

    protected List<Item> _items = new List<Item>();
    protected List<Enemy> _enemies = new List<Enemy>();


    // --- Tile Sets -----
    // used to keep track of state of tiles on the map 
    protected TileSet _walkables = new TileSet();
    protected TileSet _floor = new TileSet();
    protected TileSet _tunnel = new TileSet();
    protected TileSet _door = new TileSet();
    protected TileSet _decor = new TileSet();
    protected TileSet _discovered = new TileSet();
    protected TileSet _inFov = new TileSet();

    public Level(Player p, string map, Game game)
    {
        if (game == null || p == null || map == null)
            throw new ArgumentNullException("game, player, or map cannot be null");

        _player = p;
        _player.Pos = new Vector2(4, 12); // random, or at stairs
        _map = map;
        _game = game;
        Random rng = new Random();

        initMapTileSets(map);
        //SpawnItems(rng.Next(1, 8));
        SpawnItems(10);
        //SpawnEnemies(rng.Next(2, 5));
        SpawnEnemies(5);
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

    // -----------------------------------------------------------------------
    public override void Update()
    {
        _player!.Update();
        // foreach item update
        // foreach NPC update 
        // check for player death -- on death build RIP message
    }

    public override void Draw(IRenderWindow? disp)
    {


        // using custom RenderWindow, cast to my RenderWindow
        var tilesToDraw = new TileSet(_decor);
        tilesToDraw.IntersectWith(_discovered);
        tilesToDraw.UnionWith(_inFov);

        disp.fDraw(tilesToDraw, _map, ConsoleColor.Gray);

        var rng = new Random();
        if (_player.Turn % 5 == 0)
            _player._color = (ConsoleColor)rng.Next(10, 16);
        _player!.Draw(disp);
        // disp.Draw(_player!.Glyph, _player!.Pos, ConsoleColor.Cyan);

        drawItems(disp);
        drawEnemies(disp);
        disp.Draw(_player.HUD, new Vector2(0, 24), ConsoleColor.Green);
    }

    public override void DoCommand(Command command)
    {
        if (command.Name == "up")
        {
            MovePlayer(Vector2.N);
        }
        else if (command.Name == "down")
        {
            MovePlayer(Vector2.S);
        }
        else if (command.Name == "left")
        {
            MovePlayer(Vector2.W);
        }
        else if (command.Name == "right")
        {
            MovePlayer(Vector2.E);
        }
        else if (command.Name == "quit")
        {
            _levelActive = false;
        }
    }

    // -------------------------------------------------------------------------

    private void drawItems(IRenderWindow disp)
    {
        foreach (var item in _items)
        {
            if (_inFov.Contains(item.Pos))
            {
                item.Draw(disp);
            }
        }
    }

    private void drawEnemies(IRenderWindow disp)
    {
        foreach (var enemy in _enemies)
        {
            if (_inFov.Contains(enemy.Pos))
            {
                enemy.Draw(disp);
            }
        }
    }
    // ------ rules for map ------
    // . - floor, walkable and transparent.
    // + - door, walkable and transparent // # - tunnel, walkable and transparent
    // ' ' - solid stone, not walkable, not transparent.
    // '|' - wall, not walkable, not transparent, but discoverable.
    //  others are treated the same as wall.
    // tunnel, wall, and doorways are decor; once discovered, they are visible.

    private void initMapTileSets(string map)          //Updated by BG
    {
        int idCounter = 0;

        // TEACHER'S LOGIC: Using the Parse method to build the world
        foreach (var (c, p) in Vector2.Parse(map))
        {
            Tile? newTile = null;

            // 1. Identify and create the correct Object (Inheritance)
            if (c == '.')
            {
                // newTile = new FloorTile(idCounter++); // You can create this class
                _floor.Add(p);
                _walkables.Add(p);
            }
            else if (c == '#')
            {
                _tunnel.Add(p);
                _walkables.Add(p);
                _decor.Add(p);
            }
            else if (c == '+')
            {
                newTile = new DoorTile(idCounter++); // You can create this class
                _door.Add(p);
                _decor.Add(p);
            }
            else if (c == 'E') // Using 'E' for your ExitTile
            {
                newTile = new ExitTile(idCounter++);          // 'this' is the Level object
                _floor.Add(p);
                _walkables.Add(p);
                _decor.Add(p); // so the exit tile can be drawn when stepping on it
            }
            else if (c != ' ')
            {
                // newTile = new WallTile(idCounter++); // You can create this class
                _decor.Add(p);
            }

            // 2. If an object was created, register it
            if (newTile != null)
            {
                newTile.SetPosition(p); // Tell the tile where it lives
                _tileRegistry.Add(p, newTile);
            }
        }
    }

    //      for (int row = 0; row < lines.Length; ++row) {
    //         for (int col = 0; col < lines[row].Length; ++col) {
    //            char tile = lines[row][col];
    //
    //            if (tile == '.' || tile == '+' || tile == '#') {
    //               _walkables.Add(new Vector2(col, row));
    //               _decor.Add(new Vector2(col, row));
    //            } else if (tile != ' ') {
    //               _decor.Add(new Vector2(col, row));
    //            }
    //         }
    //      }


    // ------------------------------------------------------
    // Commands 
    // ------------------------------------------------------


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

        RegisterCommand(ConsoleKey.Q, "quit");
    }


    public void MovePlayer(Vector2 delta)
    {
        Vector2 newPos = _player!.Pos + delta;

        Enemy targetEnemy = _enemies.FirstOrDefault(e => e.Pos == newPos);
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(_player.AttackPower);

            if (targetEnemy.CurrentHealth > 0)
            {
                _player.TakeDamage(targetEnemy.AttackPower);

                if (_player.CurrentHealth <= 0)
                {
                    _levelActive = false;
                }
            }
            else
            {
                _player.GetExp(targetEnemy.ExpValue);
                _enemies.Remove(targetEnemy);
                _player.Pos = newPos;
            }

            updateDiscovered();
            return;
        }

        // --- 2. TILE INTERACTION LAYER (The "Bump") ---                           // Updated by BG
        // This handles opening doors OR triggering traps before moving
        if (_tileRegistry.TryGetValue(newPos, out Tile targetTile))
        {
            targetTile.SetTileSpace(1); // Signal an interaction

            // IF IT'S AN EXIT: Switch levels immediately
            if (targetTile is ExitTile)
            {
                if (_game is MyGame myGame)
                {
                    myGame.NextLevel();
                    return; // Stop here, the new level is now loading!
                }
            }

            // If the tile became walkable (like a door opening), update our sets
            if (targetTile is DoorTile && targetTile.IsWalkable && !_walkables.Contains(newPos))
            {
                _walkables.Add(newPos);
                updateDiscovered();
                return; // End turn after opening a door
            }
        }

        if (_walkables.Contains(newPos))
        {
            _player.Pos = newPos;
            Item steppedOnItem = _items.FirstOrDefault(i => i.Pos == newPos);

            if (steppedOnItem != null)
            {
                steppedOnItem.ApplyTo(_player);

                _items.Remove(steppedOnItem);
            }


            // exit tile is already checkked up
            /*if (_tileRegistry.TryGetValue(newPos, out Tile steppingOn))
            {
               steppingOn.SetTileSpace(1);         //This triggers "You've reached the stairs!" message

                   // Check if we just stepped on the Exit
                   if (steppingOn is ExitTile)
                   {
                       // 1. Stop the current level loop
                       this._levelActive = false;

                       // Load the next map."
                       if (this._game is MyGame myGame)         // actual map switching
                       {
                           myGame.NextLevel();
                       }
                   }
               }
            */
            updateDiscovered();

        }
    }

    private void SpawnItems(int count)
    {
        Random rng = new Random();

        List<Vector2> validSpots = _floor.ToList();

        for (int i = 0; i < count; i++)
        {
            if (validSpots.Count == 0) break;

            int index = rng.Next(validSpots.Count);
            Vector2 spawnPos = validSpots[index];

            validSpots.RemoveAt(index);

            int roll = rng.Next(10);

            switch (roll)
            {
                case 0:
                    _items.Add(new Gold(i, spawnPos, rng.Next(10, 50)));
                    break;

                case 1:
                    _items.Add(new Armour(i, spawnPos, rng.Next(1, 5)));
                    break;

                case 2:
                    _items.Add(new Weapon(i, spawnPos, rng.Next(3, 10)));
                    break;

                case 3:
                    _items.Add(new Potion(i, spawnPos, rng.Next(5, 10)));
                    break;
            }

        }
    }

    private void SpawnEnemies(int count)
    {
        Random rng = new Random();
        List<Vector2> validSpots = _floor.ToList();

        for (int i = 0; i < count; i++)
        {
            if (validSpots.Count == 0) break;
            int index = rng.Next(validSpots.Count);
            Vector2 spawnPos = validSpots[index];
            validSpots.RemoveAt(index);

            int typeRoll = rng.Next(3);
            switch (typeRoll)
            {
                case 0: _enemies.Add(new Goblin(i, spawnPos)); break;
                case 1: _enemies.Add(new Troll(i, spawnPos)); break;
                case 2: _enemies.Add(new Skeleton(i, spawnPos)); break;
            }
        }
    }

    public void QuitLevel()
    {
        _levelActive = false;
    }
}


