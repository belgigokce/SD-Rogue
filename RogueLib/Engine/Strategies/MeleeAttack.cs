using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Concrete Strategy — standard melee attack using attacker's base attack power
public class MeleeAttack : AttackStrategy
{
    public string Description => "Melee";

    public int Execute(Character attacker, IDamageable target)
    {
        int damage = attacker.GetAttackPower();
        target.TakeDamage(damage);
        return damage;
    }
}