using System.Collections.Generic;
using RogueLib.Utilities;

namespace RogueLib.Dungeon.Tiles
{
    public abstract class Tile
    {
        public int TileId;
        public Vector2 Position;
        public bool IsWalkable;

        public Tile(int id)
        {
            TileId = id;
            Position = Vector2.Zero;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public virtual List<Tile> GetNeighbors(Dictionary<Vector2, Tile> levelMap)
        {
            List<Tile> neighbors = new List<Tile>();
            Vector2[] directions = { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

            foreach (var dir in directions)
            {
                Vector2 targetPos = Position + dir;
                if (levelMap.ContainsKey(targetPos))
                    neighbors.Add(levelMap[targetPos]);
            }
            return neighbors;
        }

        public virtual void SetTileSpace(int tileSpace) { }
    }
}