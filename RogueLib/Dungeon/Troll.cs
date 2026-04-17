using System;

namespace RogueLib.Dungeon;

public class Troll : Enemy {
    public Troll(int id, Vector2 pos) : base(id, pos, 30, 8, 4, 1, 'T', ConsoleColor.DarkRed, 4) { }
    
    public override string Speak() => "Troll hungry. You look like snack.";
} 