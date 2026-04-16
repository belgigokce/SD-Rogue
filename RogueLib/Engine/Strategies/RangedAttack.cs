using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Concrete Strategy — ranged attack deals 75% of attack power with a configurable range
public class RangedAttack : AttackStrategy
{
    private int _range;

    public int Range => _range;
    public string Description => $"Ranged (range {_range})";

    public RangedAttack(int range = 5)
    {
        _range = range;
    }

    public int Execute(Character attacker, IDamageable target)
    {
        int damage = Math.Max(1, (int)(attacker.GetAttackPower() * 0.75));
        target.TakeDamage(damage);
        return damage;
    }
}