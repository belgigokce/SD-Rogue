using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS;

// Vans: Slow tank — hits hard, hard to kill
public class Orc : Enemy
{
    private static int _nextId = 300;

    public Orc(int levelDifficulty = 1) : base(_nextId++)
    {
        _maxHealth = 20 + (levelDifficulty * 4);
        _currentHealth = _maxHealth;
        _attackPower = 7 + (levelDifficulty * 2);
        _defenseValue = 4;
        _speed = 1;           // Vans: Slow but durable
        Pos = Vector2.Zero;
    }

    // Vans: Uppercase 'O' distinguishes orcs from other enemies
    public override char Glyph => 'O';

    public override void Attack(IDamageable target)
    {
        target.TakeDamage(_attackPower);
        NotifyObservers("enemyHit", _attackPower);
        NotifyObservers("enemyHealth", _currentHealth);
    }

    public override void Move(int x, int y)
    {
        Pos = new Vector2(Pos.X + x, Pos.Y + y);
    }

    public override void Draw(IRenderWindow disp)
    {
        // Vans: Red for orcs — danger signal
        disp.Draw(Glyph, Pos, ConsoleColor.Red);
    }
}