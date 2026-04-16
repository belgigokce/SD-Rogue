using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

// Vans: Strategy Pattern — defines a swappable attack behavior for any Character
public interface AttackStrategy
{
    int Execute(Character attacker, IDamageable target);
    string Description { get; }
}