using RogueLib.Dungeon;

namespace RlGameNS;

// Vans: New factory — Factory Pattern, creates Character instances by type name, decouples Level from specific enemy classes
public class EnemyFactory
{
    // Vans: Returns a Character so Level never needs to know the concrete enemy type
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

    // Vans: Picks a random enemy type, goblins weighted higher
    public Character CreateRandomEnemy(int levelDifficulty = 1)
    {
        string[] types = { "goblin", "goblin", "orc", "troll" };
        var rng = new Random();
        return CreateEnemy(types[rng.Next(types.Length)], levelDifficulty);
    }
}