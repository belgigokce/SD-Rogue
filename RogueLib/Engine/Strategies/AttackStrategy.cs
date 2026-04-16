using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RogueLib.Engine.Strategies
{
    public abstract class AttackStrategy
    {
        public abstract int Execute(Character attacker, IDamageable target);
    }
}