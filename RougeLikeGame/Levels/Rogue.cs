using RogueLib.Utilities;

namespace RlGameNS;

public class Rogue : Player
{
    public Rogue()
    {
        Name = "Rogue";
    }

    public override char Glyph => '@';
}