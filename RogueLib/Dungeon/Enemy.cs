using RogueLib.Utilities;

namespace RogueLib.Dungeon
{
    public abstract class Enemy : Character
    {
        public int EnemyId { get; protected set; }
        public int Speed { get; protected set; }

        protected Enemy(int enemyId, Vector2 startPosition, int maxHealth,
            int attack, int defense, int speed)
            : base(startPosition, maxHealth, attack, defense)
        {
            EnemyId = enemyId;
            Speed = speed;
        }

        public virtual void AttackTarget(Character target)
        {
            target.TakeDamage(GetAttackPower());
        }
    }
}