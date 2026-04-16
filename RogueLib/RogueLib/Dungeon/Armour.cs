using System;
using RogueLib.Utilities;

public class Armour : Item
{
    public int ArmorBonus { get; }

    public override char Glyph => ']';

    public Armour(Vector2 pos, int armorBonus)
        : base(pos, "Armour", ConsoleColor.Blue)
    {
        ArmorBonus = armorBonus;
    }

    public override void ApplyTo(Player player)
    {
        player.AddArmor(ArmorBonus);
    }
}