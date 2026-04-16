using RogueLib.Utilities;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace RogueLib.Utilities;

namespace RogueLib.Dungeon
{
    public abstract class Item : IDrawable
    {
        private char v;
        private Utilities.Vector2 pos;
        private ConsoleColor yellow;

        public Item(char glyph, Vector2 pos, ConsoleColor color)

        {

            Glyph = glyph;

            Pos = pos;

            Color = color;

        }

        public Vector2 Pos { get; set; }
        public char Glyph { get; init; }
         public Item(char c, Utilities.Vector2 pos1, Vector2 pos)
        {
            Glyph = c;
            Pos = pos;
        }

        protected Item(char v, Utilities.Vector2 pos, ConsoleColor yellow)
        {
            this.v = v;
            this.pos = pos;
            this.yellow = yellow;
        }

        public abstract void Draw(IRenderWindow disp);
        
    }


}
