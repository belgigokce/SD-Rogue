using RogueLib.Dungeon;

namespace RlGameNS;

// Vans: Factory Pattern — caller asks for an enemy by name string.
// Vans: Adding a new enemy type only requires editing this one file.
public class EnemyFactory
{
    // Vans: Create a specific enemy type scaled to the current floor difficulty
    public Enemy CreateEnemy(string type, int levelDifficulty = 1)
    {
        return type.ToLower() switch
        {
            "goblin" => new Goblin(levelDifficulty),
            "orc" => new Orc(levelDifficulty),
            "troll" => new Troll(levelDifficulty),
            // Vans: Fail loudly so a typo in a spawn list is caught immediately
            _ => throw new ArgumentException($"Unknown enemy type: '{type}'")
        };
    }

    // Vans: Convenience method — weighted random spawn (goblins are most common)
    public Enemy CreateRandomEnemy(int levelDifficulty = 1)
    {
        // Vans: Goblins appear twice as often as orcs or trolls
        string[] types = { "goblin", "goblin", "orc", "troll" };
        var rng = new Random();
        return CreateEnemy(types[rng.Next(types.Length)], levelDifficulty);
    }
}