using System;
using RogueLib.Utilities;

public class Potion : Item
{
    public int HealAmount { get; }

    public override char Glyph => '!';

    public Potion(Vector2 pos, int healAmount)
        : base(pos, "Potion", ConsoleColor.Magenta)
    {
        HealAmount = healAmount;
    }

    public override void ApplyTo(Player player)
    {
        player.Heal(HealAmount);
    }
}