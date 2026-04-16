using RogueLib.Utilities;

namespace RogueLib.Dungeon;

public abstract class Character : IMoveable, IDamageable
{
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; set; }
    public int AttackPower { get; set; }
    public int DefenseValue { get; set; }
    public Vector2 Pos { get; set; }

    protected Character(Vector2 startPosition, int maxHealth, int attack, int defense)
    {
        Pos = startPosition;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        AttackPower = attack;
        DefenseValue = defense;
    }

    public void Move(Vector2 direction)
    {
        Pos += direction;
    }
    
    public virtual void TakeDamage(int amount)
    {
        int actualDamage = amount - DefenseValue;
        if (actualDamage < 0) actualDamage = 0; 
            
        CurrentHealth -= actualDamage;
    }
}