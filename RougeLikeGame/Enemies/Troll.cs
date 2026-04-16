using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS;

// Vans: Boss-tier enemy — regenerates HP every turn, demonstrating polymorphism via Update()
public class Troll : Enemy
{
    private static int _nextId = 400;
    private int _regenRate;

    public Troll(int levelDifficulty = 1) : base(_nextId++)
    {
        _maxHealth = 35 + (levelDifficulty * 5);
        _currentHealth = _maxHealth;
        _attackPower = 10 + (levelDifficulty * 2);
        _defenseValue = 6;
        _speed = 1;
        _regenRate = 2;           // Vans: Heal 2 HP per turn
        Pos = Vector2.Zero;
    }

    // Vans: 'T' — unmistakeable boss glyph
    public override char Glyph => 'T';

    public override void Attack(IDamageable target)
    {
        target.TakeDamage(_attackPower);
        NotifyObservers("enemyHit", _attackPower);
        NotifyObservers("enemyHealth", _currentHealth);
    }

    // Vans: Override Update() to add regeneration — polymorphism in action
    public override void Update()
    {
        base.Update();  // Vans: Let the base Enemy run its own tick first
        if (_currentHealth < _maxHealth)
        {
            // Vans: Clamp so we never exceed max HP
            _currentHealth = Math.Min(_currentHealth + _regenRate, _maxHealth);
        }
    }

    public override void Move(int x, int y)
    {
        Pos = new Vector2(Pos.X + x, Pos.Y + y);
    }

    public override void Draw(IRenderWindow disp)
    {
        // Vans: Magenta marks the troll as a special threat
        disp.Draw(Glyph, Pos, ConsoleColor.Magenta);
    }
}