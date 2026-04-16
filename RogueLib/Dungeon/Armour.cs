using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;
using RogueLib.Dungeon;

public class Armour : Item
{
    private int ArmorBonus { get; }

    public override void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, Color);
    }

    public Armour(int id, Vector2 pos, int armorBonus)
        : base(id, "Leather Armour", "Armour", armorBonus, ']', pos, ConsoleColor.Blue)
    {
        ArmorBonus = armorBonus;
    }

    public override void ApplyTo(Player player)
    {
        player.AddArmor(ArmorBonus);
    }
}