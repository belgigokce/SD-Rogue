using System;
using RogueLib.Dungeon;
using RogueLib.Engine.Strategies;

namespace RogueLib.Utilities
{
    // Abstract player base — Rogue (or any other class) inherits this
    public abstract class Player : Character, IDrawable
    {
        public string Name { get; set; }
        public ConsoleColor _color = ConsoleColor.White;

        protected int _level = 0;
        protected int _exp = 0;
        protected int _gold = 0;
        protected int _turn = 0;

        public int Turn => _turn;
        public Inventory Inventory { get; }

        // Strategy Pattern — default is melee, swappable at runtime
        protected AttackStrategy _attackStrategy = new MeleeAttack();

        public Player()
        {
            Name = "Rogue";
            Pos = Vector2.Zero;
            _maxHealth = 12;
            _currentHealth = 12;
            _attackPower = 5;
            _defenseValue = 2;
            _speed = 1;
            Inventory = new Inventory();
        }

        public string HUD =>
            $"Level:{_level}  Gold:{_gold}    Hp:{_currentHealth}/{_maxHealth}" +
            $"  Str:{_attackPower}  Arm:{_defenseValue}  Exp:{_exp}  Turn:{_turn}";

        // Swap the attack strategy at runtime
        public void SetAttackStrategy(AttackStrategy strategy) => _attackStrategy = strategy;

        // Delegates attack to the current strategy and returns damage dealt
        public int AttackEnemy(IDamageable target) => _attackStrategy.Execute(this, target);

        public override void Update() { _turn++; }

        public virtual void Draw(IRenderWindow disp)
        {
            disp.Draw(Glyph, Pos, _color);
        }

        // Item effect methods called by Item subclasses
        public void AddGold(int amount) { _gold += amount; }
        public void Heal(int amount) { _currentHealth = Math.Min(_currentHealth + amount, _maxHealth); }
        public void AddStrength(int amount) { _attackPower += amount; }
        public void AddArmor(int amount) { _defenseValue += amount; }
        public void GetExp(int amount) { _exp += amount; }
    }
}