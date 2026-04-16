using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS;

public class Troll : Character
{
    private int _regenRate = 2;

    public override char Glyph => 'T';

    public Troll(int levelDifficulty = 1)
    {
        _maxHealth = 35 + (levelDifficulty * 5);
        _currentHealth = _maxHealth;
        _attackPower = 10 + (levelDifficulty * 2);
        _defenseValue = 6;
        _speed = 1;

        Pos = Vector2.Zero;
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(_attackPower);
    }

    public override void Update()
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Math.Min(_currentHealth + _regenRate, _maxHealth);
        }
    }

    public void Move(int x, int y)
    {
        Pos = new Vector2(Pos.X + x, Pos.Y + y);
    }
}