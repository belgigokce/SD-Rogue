using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS;

// Vans: Fast but fragile — cannon fodder of the dungeon
public class Goblin : Enemy
{
    // Vans: Start IDs at 200 so Goblin, Orc, Troll IDs never collide
    private static int _nextId = 200;

    // Vans: Scale stats with level difficulty so later floors feel harder
    public Goblin(int levelDifficulty = 1) : base(_nextId++)
    {
        _maxHealth = 8 + (levelDifficulty * 2);
        _currentHealth = _maxHealth;
        _attackPower = 3 + levelDifficulty;
        _defenseValue = 1;
        _speed = 3;           // Vans: Fast movement speed
        Pos = Vector2.Zero;
    }

    // Vans: Lowercase 'g' so player can tell goblins from other enemies at a glance
    public override char Glyph => 'g';

    public override void Attack(IDamageable target)
    {
        target.TakeDamage(_attackPower);
        // Vans: Notify observers so UIManager can display combat feedback
        NotifyObservers("enemyHit", _attackPower);
        NotifyObservers("enemyHealth", _currentHealth);
    }

    public override void Move(int x, int y)
    {
        // Vans: Shift position by the given delta
        Pos = new Vector2(Pos.X + x, Pos.Y + y);
    }

    public override void Draw(IRenderWindow disp)
    {
        // Vans: Green glyphs for goblins — easy to spot against gray walls
        disp.Draw(Glyph, Pos, ConsoleColor.Green);
    }
}