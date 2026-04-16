namespace RogueLib.Dungeon
{
    // Any entity that can receive damage and report health
    public interface IDamageable
    {
        void TakeDamage(int amount);
        int GetHealth();
    }
}