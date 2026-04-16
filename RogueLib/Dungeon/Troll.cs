namespace RogueLib.Dungeon;

// Troll: Heavy hitter, lots of health
public class Troll : Enemy {
    public Troll(int id, Vector2 pos) : base(id, pos, 30, 8, 4, 1, 'T', ConsoleColor.DarkRed, 4) { }
}