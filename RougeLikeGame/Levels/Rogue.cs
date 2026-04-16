using RogueLib.Utilities;

namespace RlGameNS
{
    // Concrete player class — inherits everything from the abstract Player
    public class Rogue : Player
    {
        public override char Glyph => '@';

        public Rogue()
        {
            Name = "Rogue";
        }
    }
}