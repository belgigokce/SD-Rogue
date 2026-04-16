using System;
using System.Collections.Generic;
namespace RogueLib.Dungeon;

// Vans: Observer Pattern — anything that wants to watch game events implements this
public interface IObserver
{
    // Vans: Called by IObservable when a named event fires with an integer value
    void Update(string subject, int value);
}