namespace RogueLib.Dungeon
{
    // Any world entity with a glyph implements this
    public interface IActor
    {
        char Glyph { get; }
    }
}