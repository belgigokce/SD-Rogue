using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

public class MagicAttack : AttackStrategy
{
    private int _manaCost;

    public string Description => $"Magic (cost {_manaCost} mana)";

    public MagicAttack(int manaCost = 10)
    {
        _manaCost = manaCost;
    }

    public int Execute(Character attacker, IDamageable target)
    {
        int damage = (int)(attacker.GetAttackPower() * 1.5);
        target.TakeDamage(damage);
        return damage;
    }
}