namespace RogueLib.Dungeon
{
    // Observer Pattern — receives named game events with an integer value
    public interface IObserver
    {
        void Update(string subject, int value);
    }
}