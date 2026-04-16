using System;
using RogueLib.Utilities;

namespace RlGameNS
{
    // Factory Pattern — creates Character instances by type name
    // Level never needs to know the concrete enemy class
    public class EnemyFactory
    {
        public Character CreateEnemy(string type, int levelDifficulty = 1)
        {
            return type.ToLower() switch
            {
                "goblin" => new Goblin(levelDifficulty),
                "orc" => new Orc(levelDifficulty),
                "troll" => new Troll(levelDifficulty),
                _ => throw new ArgumentException($"Unknown enemy type: {type}")
            };
        }

        // Picks a random enemy type — goblins weighted higher
        public Character CreateRandomEnemy(int levelDifficulty = 1)
        {
            string[] types = { "goblin", "goblin", "orc", "troll" };
            return CreateEnemy(types[new Random().Next(types.Length)], levelDifficulty);
        }
    }
}