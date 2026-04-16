using System;
using System.Text;
using RogueLib.Dungeon;
using FilterSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RogueLib.Utilities
{
    public class ScreenBuff : IRenderWindow
    {
        protected const ConsoleColor _notAColor = (ConsoleColor)(-1);

        private readonly int _width;
        private readonly int _height;
        private readonly char[,] _back;
        private readonly char[,] _front;
        private readonly ConsoleColor[,] _backColor;
        private readonly ConsoleColor[,] _frontColor;

        public int Width => _width;
        public int Height => _height;

        public ScreenBuff(int width = 78, int height = 25)
        {
            _width = width;
            _height = height;
            _back = new char[_width, _height];
            _front = new char[_width, _height];
            _backColor = new ConsoleColor[_width, _height];
            _frontColor = new ConsoleColor[_width, _height];
            ResetFront();
        }

        public virtual void fDraw(FilterSet fs, string s, ConsoleColor color = _notAColor)
            => BuildOffsetFrame(s, Vector2.Zero, color, fs);

        public virtual void Draw(string s, ConsoleColor color = _notAColor)
            => BuildOffsetFrame(s, Vector2.Zero, color);

        public virtual void Draw(string s, Vector2 pos, ConsoleColor color = _notAColor)
            => BuildOffsetFrame(s, pos, color);

        public virtual void Draw(char c, Vector2 pos, ConsoleColor foregroundColor = _notAColor)
        {
            if (!IsValidPos(pos)) return;
            _back[pos.X, pos.Y] = c;
            _backColor[pos.X, pos.Y] = (foregroundColor != _notAColor) ? foregroundColor : Console.ForegroundColor;
        }

        public void Display() => FlushToScreen();

        protected bool IsValidPos(Vector2 pos)
            => pos.X >= 0 && pos.X < _width && pos.Y >= 0 && pos.Y < _height;

        protected void ResetFront()
        {
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                {
                    _front[x, y] = '\0';
                    _frontColor[x, y] = (ConsoleColor)(-1);
                }
        }

        protected void BuildOffsetFrame(string s, Vector2 offset, ConsoleColor color = _notAColor,
                                        FilterSet? filterSet = null)
        {
            var fgColor = (color == _notAColor) ? Console.ForegroundColor : color;
            foreach (var (c, p) in Vector2.Parse(s))
            {
                if (filterSet is not null && !filterSet.Contains(p)) continue;
                var op = p + offset;
                if (!IsValidPos(op)) continue;
                _back[op.X, op.Y] = c;
                _backColor[op.X, op.Y] = fgColor;
            }
        }

        protected void FlushToScreen()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_back[x, y] == _front[x, y] && _backColor[x, y] == _frontColor[x, y]) continue;

                    Console.SetCursorPosition(x, y);
                    var saveColor = Console.ForegroundColor;
                    Console.ForegroundColor = _backColor[x, y];
                    Console.Write(_back[x, y]);
                    Console.ForegroundColor = saveColor;

                    _front[x, y] = _back[x, y];
                    _frontColor[x, y] = _backColor[x, y];
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(0, _height);
        }
    }
}