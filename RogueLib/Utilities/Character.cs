using System;
using RogueLib.Dungeon;

namespace RogueLib.Utilities
{
    public abstract class Character : IDamageable, IActor
    {
        public Vector2 Pos;
        public abstract char Glyph { get; }

        protected int _maxHealth;
        protected int _currentHealth;
        protected int _attackPower;
        protected int _defenseValue;
        protected int _speed;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        // Parameterless — used by Player
        protected Character() { }

        // Parameterized — used by Enemy subclasses
        protected Character(Vector2 startPosition, int maxHealth, int attack, int defense)
        {
            Pos = startPosition;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _attackPower = attack;
            _defenseValue = defense;
        }

        public virtual int GetHealth() => _currentHealth;
        public virtual int GetAttackPower() => _attackPower;

        public virtual void TakeDamage(int amount)
        {
            int dmg = Math.Max(0, amount - _defenseValue);
            _currentHealth -= dmg;
            if (_currentHealth < 0) _currentHealth = 0;
        }

        public virtual void Update() { }
    }
}