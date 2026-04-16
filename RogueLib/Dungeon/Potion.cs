using System;
using RogueLib.Utilities;

namespace RogueLib.Dungeon
{
    public class Potion : Item
    {
        public Potion(int id, Vector2 pos, int healAmount)
            : base(id, "Healing Potion", "Potion", healAmount, '!', pos, ConsoleColor.Magenta) { }

        public override void ApplyTo(Player player)
        {
            player.Heal(EffectValue);
        }
    }
}