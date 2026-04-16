namespace RogueLib.Dungeon;

// Anything that exists in the world and has a glyph
public interface IActor
{
    char Glyph { get; }
}