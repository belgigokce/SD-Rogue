namespace RogueLib.Dungeon;

public interface IConsumable
{
    void Consume();
    int CalculateEffect(int volume);
    void SetItemType();
}