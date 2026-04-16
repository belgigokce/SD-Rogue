namespace RogueLib.Dungeon
{
    // Observer Pattern — any subject that broadcasts events to observers
    public interface IObservable
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers(string subject, int value);
    }
}