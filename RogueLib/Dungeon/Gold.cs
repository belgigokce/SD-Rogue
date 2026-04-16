using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

public class Gold : Item
{
    public Gold(int id, Vector2 pos, int amount)
        : base(id, "Gold", "Currency", amount, '$', pos, ConsoleColor.Yellow) {}

    public override void ApplyTo(Player player)
    {
        player.AddGold(EffectValue);
    }
}