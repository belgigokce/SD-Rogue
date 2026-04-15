namespace RogueLib.Dungeon;

public interface IEquppable
{
    void Equip();
    int CalculateEffectValue(int bonus);
    void SetItemType();
}