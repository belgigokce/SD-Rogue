using System;
using RogueLib.Utilities;

namespace RogueLib.Dungeon
{
    public class Weapon : Item
    {
        public Weapon(int id, Vector2 pos, int strengthBonus)
            : base(id, "Iron Sword", "Weapon", strengthBonus, '/', pos, ConsoleColor.Cyan) { }

        public override void ApplyTo(Player player)
        {
            player.AddStrength(EffectValue);
        }
    }
}