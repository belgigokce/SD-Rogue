using System;

namespace RogueLib.Dungeon;

public class Skeleton : Enemy {
    public Skeleton(int id, Vector2 pos) : base(id, pos, 15, 5, 2, 2, 'S', ConsoleColor.Gray, 1) { }
    
    public override string Speak() => "More bones for the pile...";
}