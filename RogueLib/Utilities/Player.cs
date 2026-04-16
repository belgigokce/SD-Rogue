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

    // Vans: [START] - New, Strategy Pattern, default attack is melee, swappable at runtime
    protected AttackStrategy _attackStrategy = new MeleeAttack();
    // Vans: [END]

    public Player()
    {
        Name = "Rogue";
        Pos = Vector2.Zero;

        // Vans: [START] - New, stats moved here from old field declarations, now initialized via Character base fields
        _maxHealth = 12;
        _currentHealth = 12;
        _attackPower = 5;
        _defenseValue = 2;
        _speed = 1;
        // Vans: [END]
    }

    public string HUD =>
        $"Lvl:{_level} Gold:{_gold} HP:{_currentHealth}/{_maxHealth} Turn:{_turn}";  // Vans: [START] - Updated, HUD now shows current/max HP from Character fields // Vans: [END]

    // Vans: [START] - New, allows swapping attack strategy at runtime
    public void SetAttackStrategy(AttackStrategy strategy)
    {
        _attackStrategy = strategy;
    }
    // Vans: [END]

    // Vans: [START] - New, delegates attack to the current strategy
    public int AttackEnemy(IDamageable target)
    {
        return _attackStrategy.Execute(this, target);
    }
    // Vans: [END]

    public override void Update()
    {
        _turn++;
    }

    public void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, _color);
    }
}