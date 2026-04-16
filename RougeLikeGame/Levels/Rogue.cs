using RogueLib.Engine;
using RogueLib.Utilities;

namespace RlGameNS;

public class Rogue : Player
{

    public Rogue() : base()
    {
        Name = "Rogue";
        // Default strategy is MeleeAttack (set in Player base already).
        // Uncomment a line below to demo strategy swapping at start:
        // SetAttackStrategy(new RangedAttack(range: 6));
        // SetAttackStrategy(new MagicAttack(manaCost: 8));
    }
}