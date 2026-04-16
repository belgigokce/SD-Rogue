using RogueLib.Utilities;

namespace RogueLib.Dungeon;

public abstract class Enemy : Character, IDrawable
{
    public int EnemyId { get; protected set; }
    public int Speed { get; protected set;  }
    public char Glyph { get; }
    public int ExpValue { get; protected set; }
    public ConsoleColor Color { get; }

    protected Enemy(int enemyId, Vector2 startPosition, int maxHealth, 
        int attack, int defense, int speed, char glyph, ConsoleColor color, int expValue) : base (startPosition, maxHealth, attack, defense)
    {
        EnemyId = enemyId;
        Speed = speed;
        Glyph = glyph;       
        Color = color;
        ExpValue = expValue;
    }

    public virtual void AttackTarget(Character target)
    {
        target.TakeDamage(AttackPower);
    }

    public void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, Color);
    }
}