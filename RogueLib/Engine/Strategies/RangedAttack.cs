using System;
using System.Collections.Generic;
using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Concrete Strategy 2 — weaker hit but keeps the player at a safe distance
public class RangedAttack : AttackStrategy
{
    private int _range;

    // Vans: Expose range so Level can check if target is within reach
    public int Range => _range;
    public string Description => $"Ranged (range {_range})";

    // Vans: Default range of 5 tiles
    public RangedAttack(int range = 5) => _range = range;

    // Vans: 75 % of normal power — trade damage for safety
    public int Execute(Character attacker, IDamageable target)
    {
        int damage = Math.Max(1, (int)(attacker.GetAttackPower() * 0.75));
        target.TakeDamage(damage);
        return damage;
    }
}