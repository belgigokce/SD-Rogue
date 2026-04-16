using System;
using System.Collections.Generic;
using System.Text;
using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Concrete Strategy 1 — straightforward physical hit, no frills
public class MeleeAttack : AttackStrategy
{
    // Vans: Label for UI display
    public string Description => "Melee";

    // Vans: Deal full attack power as damage
    public int Execute(Character attacker, IDamageable target)
    {
        int damage = attacker.GetAttackPower();
        target.TakeDamage(damage);
        return damage;
    }
}
