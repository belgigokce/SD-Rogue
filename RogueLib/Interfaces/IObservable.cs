using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLib.Dungeon;

// Vans: Observer Pattern — anything that can broadcast events implements this
public interface IObservable
{
    // Vans: Subscribe an observer to this subject
    void RegisterObserver(IObserver observer);

    // Vans: Unsubscribe an observer
    void RemoveObserver(IObserver observer);

    // Vans: Push a named event + value to all subscribed observers
    void NotifyObservers(string subject, int value);
}