using RogueLib.Dungeon;
using RogueLib.Utilities;

// Vans: New base class — replaces the old Enemy system, shared by Player and all enemy types
public abstract class Character : IDamageable, IActor
{
    public Vector2 Pos;

    public abstract char Glyph { get; }

    protected int _maxHealth;
    protected int _currentHealth;
    protected int _attackPower;
    protected int _defenseValue;
    protected int _speed;

    public virtual int GetHealth() => _currentHealth;

    // Vans: Defense reduces incoming damage before applying it
    public virtual void TakeDamage(int amount)
    {
        int dmg = Math.Max(0, amount - _defenseValue);
        _currentHealth -= dmg;

        if (_currentHealth < 0)
            _currentHealth = 0;
    }

    public virtual int GetAttackPower() => _attackPower;

    public virtual void Update() { }
}