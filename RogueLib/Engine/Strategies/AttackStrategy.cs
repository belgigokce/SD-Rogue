using RogueLib.Dungeon;

namespace RogueLib.Engine.Strategies;

public interface AttackStrategy
{
    int Execute(Character attacker, IDamageable target);
    string Description { get; }
}