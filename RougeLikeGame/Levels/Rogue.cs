using RogueLib.Utilities;

namespace RlGameNS;

// Vans: Updated — now inherits from the new Character-based Player instead of the old IActor-based Player
public class Rogue : Player
{
    public Rogue()
    {
        Name = "Rogue";
    }

    public override char Glyph => '@';
}