using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

public class Potion : Item
{
    public int HealAmount { get; }
    
    public Potion(int id, Vector2 pos, int healAmount)
        : base(id, "Healing Potion","Potion", healAmount, '!', pos, ConsoleColor.Magenta)
    {
        HealAmount = healAmount;
    }

    public override void ApplyTo(Player player)
    {
        player.Heal(HealAmount);
    }
}