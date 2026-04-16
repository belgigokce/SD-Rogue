using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS
{
    // Light, fast enemy — stats scale with level difficulty
    public class Goblin : Character
    {
        public override char Glyph => 'g';

        public Goblin(int levelDifficulty = 1)
        {
            _maxHealth = 8 + (levelDifficulty * 2);
            _currentHealth = _maxHealth;
            _attackPower = 3 + levelDifficulty;
            _defenseValue = 1;
            _speed = 3;
            Pos = Vector2.Zero;
        }

        public void Attack(IDamageable target) => target.TakeDamage(_attackPower);

        public void Move(int x, int y) => Pos = new Vector2(Pos.X + x, Pos.Y + y);

        public override void Update() { }
    }
}