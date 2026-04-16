using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RlGameNS
{
    // Tanky, high-damage enemy — stats scale with level difficulty
    public class Orc : Character
    {
        public override char Glyph => 'O';

        public Orc(int levelDifficulty = 1)
        {
            _maxHealth = 20 + (levelDifficulty * 4);
            _currentHealth = _maxHealth;
            _attackPower = 7 + (levelDifficulty * 2);
            _defenseValue = 4;
            _speed = 1;
            Pos = Vector2.Zero;
        }

        public void Attack(IDamageable target) => target.TakeDamage(_attackPower);

        public void Move(int x, int y) => Pos = new Vector2(Pos.X + x, Pos.Y + y);

        public override void Update() { }
    }
}