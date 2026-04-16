using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

public class Weapon : Item
{
    private int StrengthBonus { get; }
    
    public Weapon(int id, Vector2 pos, int strengthBonus)
        : base(id, "Iron Sword", "Weapon", strengthBonus,'/', pos ,ConsoleColor.Cyan)
    {
        StrengthBonus = strengthBonus;
    }

    public override void ApplyTo(Player player)
    {
        player.AddStrength(StrengthBonus);
    }
}