namespace RogueLib.Dungeon;

// Vans: New interface — any entity that can receive damage and report health implements this
public interface IDamageable
{
    void TakeDamage(int amount);
    int GetHealth();
}