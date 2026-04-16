namespace RogueLib.Dungeon;

// Vans: Updated from a class to a proper interface — any world entity with a glyph implements this
public interface IActor
{
    char Glyph { get; }
}