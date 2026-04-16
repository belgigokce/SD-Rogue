using System;
using RogueLib.Utilities;

public class Weapon : Item
{
    public int StrengthBonus { get; }

    public override char Glyph => ')';

    public Weapon(Vector2 pos, int strengthBonus)
        : base(pos, "Weapon", ConsoleColor.Cyan)
    {
        StrengthBonus = strengthBonus;
    }

    public override void ApplyTo(Player player)
    {
        player.AddStrength(StrengthBonus);
    }
}