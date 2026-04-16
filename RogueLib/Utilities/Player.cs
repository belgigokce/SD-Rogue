using RogueLib.Dungeon;
using RogueLib.Engine.Strategies;
using RogueLib.Utilities;

public abstract class Player : Character, IDrawable
{
    public string Name { get; set; }

    public override char Glyph => '@';

    public ConsoleColor _color = ConsoleColor.White;

    protected int _level = 0;
    protected int _exp = 0;
    protected int _gold = 0;
    protected int _turn = 0;

    public int Turn => _turn;

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
    }

    public string HUD =>
        $"Lvl:{_level} Gold:{_gold} HP:{_currentHealth}/{_maxHealth} Turn:{_turn}";

    public void SetAttackStrategy(AttackStrategy strategy)
    {
        _attackStrategy = strategy;
    }

    public int AttackEnemy(IDamageable target)
    {
        return _attackStrategy.Execute(this, target);
    }

    public override void Update()
    {
        _turn++;
    }

    public void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, _color);
    }
}