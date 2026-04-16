using System;
using System.Collections.Generic;
using System.Text;
using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Strategy Pattern interface — defines the contract all attack types must follow.
// Vans: Swap strategies at runtime without changing Player or Character code.
public interface AttackStrategy
{
    // Vans: Execute the attack from attacker → target, return damage dealt
    int Execute(Character attacker, IDamageable target);

    // Vans: Human-readable label shown in UI / debug
    string Description { get; }
}