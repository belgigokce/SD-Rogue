using System.Collections.Generic;
using RogueLib.Utilities;

namespace RogueLib.Dungeon.Tiles
{
    public abstract class Tile
    {
        protected int _tileId;
        protected Vector2 _tilePosition;
        protected bool _isWalkable;

        public Tile(int id)
        {
            _tileId = id;
            _tilePosition = Vector2.Zero;
        }

        // --- FIXED: SetPosition belongs in the class body ---
        public void SetPosition(Vector2 position)
        {
            _tilePosition = position;
        }

        // --- TEACHER'S LOGIC: Using the Registry for Neighbors ---
        public virtual List<Tile> GetNeighbors(Dictionary<Vector2, Tile> levelMap)
        {
            List<Tile> neighbors = new List<Tile>();
            Vector2[] directions = { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

            foreach (var dir in directions)
            {
                Vector2 targetPos = _tilePosition + dir;
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