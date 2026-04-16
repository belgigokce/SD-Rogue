using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS;

// Vans: New enemy — light, fast enemy that inherits from Character for shared combat logic
public class Goblin : Character
{
    public override char Glyph => 'g';

    // Vans: Stats scale with level difficulty
    public Goblin(int levelDifficulty = 1)
    {
        _maxHealth = 8 + (levelDifficulty * 2);
        _currentHealth = _maxHealth;
        _attackPower = 3 + levelDifficulty;
        _defenseValue = 1;
        _speed = 3;

        Pos = Vector2.Zero;
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(_attackPower);
    }

    public void Move(int x, int y)
    {
        Pos = new Vector2(Pos.X + x, Pos.Y + y);
    }

    public override void Update() { }
}