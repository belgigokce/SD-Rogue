using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

public abstract class Item : IActor, IDrawable
{
    public Vector2 Pos { get; protected set; }
    public string Name { get; protected set; }
    public ConsoleColor Color { get; protected set; }

    public abstract char Glyph { get; }

    protected Item(Vector2 pos, string name, ConsoleColor color)
    {
        Pos = pos;
        Name = name;
        Color = color;
    }

    public virtual void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, Color);
    }

    public bool IsAt(Vector2 pos) => Pos == pos;

    public abstract void ApplyTo(Player player);
}