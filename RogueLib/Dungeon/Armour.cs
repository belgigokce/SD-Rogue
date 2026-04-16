using System;
using RogueLib.Utilities;

namespace RogueLib.Dungeon
{
    public class Armour : Item
    {
        public Armour(int id, Vector2 pos, int armorBonus)
            : base(id, "Leather Armour", "Armour", armorBonus, ']', pos, ConsoleColor.Blue) { }

        public override void ApplyTo(Player player)
        {
            player.AddArmor(EffectValue);
        }
    }
}