using System;
using System.Collections.Generic;
using System.Text;
using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Concrete Strategy 3 — ignores armour, costs mana, hits for 150 % power
public class MagicAttack : AttackStrategy
{
    private int _manaCost;
    public string Description => $"Magic (cost {_manaCost} mana)";

    // Vans: Default mana cost of 10
    public MagicAttack(int manaCost = 10) => _manaCost = manaCost;

    // Vans: Bypass physical defences — 150 % of base attack power
    public int Execute(Character attacker, IDamageable target)
    {
        int damage = (int)(attacker.GetAttackPower() * 1.5);
        target.TakeDamage(damage);
        return damage;
    }
}