using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RogueLib.Engine.Strategies
{
    // Concrete Strategy — standard melee attack using attacker's base attack power
    public class MeleeAttack : AttackStrategy
    {
        public string Description => "Melee";

        public override int Execute(Character attacker, IDamageable target)
        {
            int damage = attacker.GetAttackPower();
            target.TakeDamage(damage);
            return damage;
        }
    }
}