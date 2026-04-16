using RogueLib.Dungeon;
using RogueLib.Engine.Strategies;   // Vans: Needed for AttackStrategy + concrete strategies
using RogueLib.Utilities;

public abstract class Player : Character, IDrawable
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

    // Vans: Strategy Pattern field — MeleeAttack is the default weapon
    protected AttackStrategy _attackStrategy = new MeleeAttack();

    public Player()
    {
        Name = "Rogue";
        Pos = Vector2.Zero;
    }

    public string HUD =>
        $"Level:{_level}  Gold: {_gold}    Hp: {_hp}({_maxHp})" +
        $"  Str: {_str}({_maxStr})" +
        $"  Arm: {_arm}   Exp: {_exp}/{10} Turn: {_turn}";

    // Vans: Strategy Pattern setter — swap attack type at runtime
    // Vans: e.g. picking up a magic staff calls SetAttackStrategy(new MagicAttack())
    public void SetAttackStrategy(AttackStrategy strategy)
    {
        _attackStrategy = strategy;
    }

    // Vans: Called by Level when the player is adjacent to an enemy.
    // Vans: Player now extends Character so no unsafe cast needed.
    public int AttackEnemy(IDamageable target)
    {
        return _attackStrategy.Execute(this, target);
    }

    public virtual void Update()
    {
        _turn++;
    }

    public virtual void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, _color);
    }
}