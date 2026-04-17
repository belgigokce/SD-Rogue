using System;

namespace RogueLib.Dungeon;

public class Goblin : Enemy {
    public Goblin(int id, Vector2 pos) : base(id, pos, 10, 3, 1, 2, 'G', ConsoleColor.Green, 2) { }
    
    public override string Speak() => "Shiny! Give it! Give it now!";
}