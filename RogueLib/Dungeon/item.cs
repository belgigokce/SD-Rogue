using System;
using RogueLib.Utilities;

namespace RogueLib.Dungeon
{
    public abstract class Item : IDrawable, INameable
    {
        public int ItemId { get; protected set; }
        public string ItemName { get; protected set; }
        public string ItemType { get; protected set; }
        public int EffectValue { get; protected set; }
        public Vector2 Pos { get; set; }
        public char Glyph { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public Item(int id, string name, string type, int effect, char glyph, Vector2 pos, ConsoleColor color)
        {
            ItemId = id;
            ItemName = name;
            ItemType = type;
            EffectValue = effect;
            Glyph = glyph;
            Pos = pos;
            Color = color;
        }

        public virtual void Draw(IRenderWindow disp)
        {
            disp.Draw(Glyph, Pos, Color);
        }

        public void Rename(string newName)
        {
            ItemName = newName;
        }

        public abstract void ApplyTo(Player player);
    }
}