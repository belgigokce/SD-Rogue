using System;
using System.Collections.Generic;

namespace RogueLib.Utilities
{
    public struct Vector2 : IEquatable<Vector2>, IComparable<Vector2>
    {
        public int X { get; }
        public int Y { get; }

        public Vector2(int x, int y) => (X, Y) = (x, y);
        public Vector2() : this(0, 0) { }

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);

        public static readonly Vector2 S = new Vector2(0, 1);
        public static readonly Vector2 E = new Vector2(1, 0);
        public static readonly Vector2 N = -1 * S;
        public static readonly Vector2 W = -1 * E;
        public static readonly Vector2 NE = N + E;
        public static readonly Vector2 NW = N + W;
        public static readonly Vector2 SE = S + E;
        public static readonly Vector2 SW = S + W;

        public override string ToString() => $"({X}, {Y})";

        public bool Equals(Vector2 other) => X == other.X && Y == other.Y;
        public override bool Equals(object? obj) => obj is Vector2 other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public int CompareTo(Vector2 other)
            => Y.CompareTo(other.Y) != 0 ? Y.CompareTo(other.Y) : X.CompareTo(other.X);

        public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);
        public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);
        public static Vector2 operator *(int left, Vector2 right) => new(right.X * left, right.Y * left);
        public static Vector2 operator *(Vector2 left, int right) => new(left.X * right, left.Y * right);
        public static Vector2 operator /(Vector2 left, int right) => new(left.X / right, left.Y / right);
        public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);
        public static bool operator !=(Vector2 a, Vector2 b) => !a.Equals(b);

        public static int manhattanDistance(Vector2 a, Vector2 b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        public static bool IsDistanceWithin(Vector2 a, Vector2 b, int distance)
            => (a - b).LengthSquared <= distance * distance;

        public int LengthSquared => X * X + Y * Y;
        public float Length => (float)Math.Sqrt(LengthSquared);
        public int RookLength => Math.Abs(X) + Math.Abs(Y);
        public int KingLength => Math.Max(Math.Abs(X), Math.Abs(Y));

        public static IEnumerable<Vector2> getAllTiles(int w = 78, int h = 25)
        {
            for (int row = 0; row < h; row++)
                for (int col = 0; col < w; col++)
                    yield return new Vector2(col, row);
        }

        public static IEnumerable<(char ch, Vector2)> Parse(string text)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));

            int x = 0, y = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (ch == '\r')
                {
                    if (i + 1 < text.Length && text[i + 1] == '\n') i++;
                    x = 0; y++; continue;
                }
                if (ch == '\n') { x = 0; y++; continue; }
                yield return (ch, new Vector2(x, y));
                x++;
            }
        }
    }
}