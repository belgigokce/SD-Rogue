using RogueLib.Utilities;

namespace RogueLib.Dungeon;

public abstract class Character : IMoveable, IDamageable
{
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public int AttackPower { get; protected set; }
    public int DefenseValue { get; protected set; }
    public Vector2 Position { get; protected set; }

    protected Character(Vector2 startPosition, int maxHealth, int attack, int defense)
    {
        Position = startPosition;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        AttackPower = attack;
        DefenseValue = defense;
    }

    public void Move(Vector2 direction)
    {
        Position += direction;
    }
    
    public virtual void TakeDamage(int amount)
    {
        int actualDamage = amount - DefenseValue;
        if (actualDamage < 0) actualDamage = 0; 
            
        CurrentHealth -= actualDamage;
    }
}