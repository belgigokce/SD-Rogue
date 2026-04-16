using RogueLib.Dungeon;
using RogueLib.Utilities;
using System;

namespace RogueLib.Engine.Strategies
{
    // Concrete Strategy — ranged attack deals 75% of attack power
    public class RangedAttack : AttackStrategy
    {
        private int _range;

        public int Range => _range;
        public string Description => $"Ranged (range {_range})";

        public RangedAttack(int range = 5)
        {
            _range = range;
        }

        public override int Execute(Character attacker, IDamageable target)
        {
            int damage = Math.Max(1, (int)(attacker.GetAttackPower() * 0.75));
            target.TakeDamage(damage);
            return damage;
        }
    }
}