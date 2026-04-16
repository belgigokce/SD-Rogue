using RogueLib.Dungeon;
using RogueLib.Utilities;

public class Player : IActor, IDrawable
{
    public string Name { get; set; }
    public Vector2 Pos;
    public char Glyph => '@';
    public ConsoleColor _color = ConsoleColor.White;

    protected int _level = 0;
    protected int _hp = 12;
    protected int _str = 16;
    protected int _arm = 4;
    protected int _exp = 0;
    protected int _gold = 0;
    protected int _maxHp = 12;
    protected int _maxStr = 16;
    protected int _turn = 0;

    public int Turn => _turn;
    public Inventory Inventory { get; }

    public Player()
    {
        Name = "Rogue";
        Pos = Vector2.Zero;
        Inventory = new Inventory();
    }

    public string HUD =>
        $"Level:{_level}  Gold: {_gold}    Hp: {_hp}({_maxHp})" +
        $"  Str: {_str}({_maxStr})" +
        $"  Arm: {_arm}   Exp: {_exp}/{10} Turn: {_turn}";

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
        _hp += amount;
        if (_hp > _maxHp)
            _hp = _maxHp;
    }

    public void AddStrength(int amount)
    {
        _str += amount;
        if (_str > _maxStr)
            _maxStr = _str;
    }

    public void AddArmor(int amount)
    {
        _arm += amount;
    }
}