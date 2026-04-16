namespace RogueLib.Dungeon;

// Anything that can take damage implements this
public interface IDamageable
{
    void TakeDamage(int amount);
    int GetHealth();
}