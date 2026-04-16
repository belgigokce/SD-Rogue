using RogueLib.Dungeon;

namespace RlGameNS;

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

    public Character CreateRandomEnemy(int levelDifficulty = 1)
    {
        string[] types = { "goblin", "goblin", "orc", "troll" };
        var rng = new Random();
        return CreateEnemy(types[rng.Next(types.Length)], levelDifficulty);
    }
}