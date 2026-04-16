using System;
using System.Collections.Generic;

namespace RogueLib.Dungeon;

// Vans: New interface — Observer Pattern, receives named game events with an integer value
public interface IObserver
{
    void Update(string subject, int value);
}