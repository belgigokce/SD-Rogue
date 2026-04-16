using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLib.Dungeon;

// Vans: New interface — Observer Pattern, any subject that broadcasts events to observers implements this
public interface IObservable
{
    // Vans: Subscribe an observer
    void RegisterObserver(IObserver observer);

    // Vans: Unsubscribe an observer
    void RemoveObserver(IObserver observer);

    // Vans: Broadcast a named event and value to all observers
    void NotifyObservers(string subject, int value);
}