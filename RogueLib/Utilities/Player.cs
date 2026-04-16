using RogueLib.Dungeon;
using RogueLib.Utilities;
using System;

// Inheriting from Character is the "glue" that makes combat work
public class Player : Character, IActor, IDrawable
{
    public string Name { get; set; }
    public char Glyph => '@';
    public ConsoleColor _color = ConsoleColor.White;

    protected int _level = 0;
    protected int _exp = 0;
    protected int _gold = 0;
    protected int _turn = 0;
    protected int _maxStr = 16;

    public int Turn => _turn;
    public Inventory Inventory { get; }

    public Player() : base(Vector2.Zero, 12, 16, 4) 
    {
        Name = "Rogue";
        Inventory = new Inventory();
    }

    public string HUD =>
        $"Level:{_level}  Gold: {_gold}    Hp: {CurrentHealth}({MaxHealth})" +
        $"  Str: {AttackPower}" +
        $"  Arm: {DefenseValue}   Exp: {_exp}/{10} Turn: {_turn}";

    public virtual void Update()
    {
        _turn++;
    }

    public virtual void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, _color);
    }

    public void AddGold(int amount)
    {
        _gold += amount;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    public void AddStrength(int amount)
    {
        AttackPower += amount;
    }

    public void AddArmor(int amount)
    {
        DefenseValue += amount;
    }

    public void GetExp(int amount)
    {
        _exp += amount;
    }
}