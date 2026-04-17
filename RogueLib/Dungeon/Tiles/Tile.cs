using System.Collections.Generic;
using RogueLib.Utilities;

namespace RlGameNS
{
    public abstract class Tile
    {
        public int TileId;
        public Vector2 Position;
        public bool IsWalkable;
        public char Glyph;

        public Tile(int id, char glyph)
        {
            TileId = id;
            Position = Vector2.Zero;
            Glyph = glyph;
        }

        // --- FIXED: SetPosition belongs in the class body ---
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        // --- TEACHER'S LOGIC: Using the Registry for Neighbors ---
        public virtual List<Tile> GetNeighbors(Dictionary<Vector2, Tile> levelMap)
        {
            List<Tile> neighbors = new List<Tile>();
            Vector2[] directions = { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

            foreach (var dir in directions)
            {
                Vector2 targetPos = Position + dir;
                if (levelMap.ContainsKey(targetPos))
                {
                    neighbors.Add(levelMap[targetPos]);
                }
            }
            return neighbors;
        }

        public virtual void SetTileSpace(int tileSpace)
        {
            // Default implementation
        }
    }
}
