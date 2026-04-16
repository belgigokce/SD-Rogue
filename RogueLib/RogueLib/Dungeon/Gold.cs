using System;
using RogueLib.Utilities;

public class Gold : Item
{
    public int Amount { get; }

    public override char Glyph => '$';

    public Gold(Vector2 pos, int amount)
        : base(pos, "Gold", ConsoleColor.Yellow)
    {
        Amount = amount;
    }

    public override void ApplyTo(Player player)
    {
        player.AddGold(Amount);
    }
}