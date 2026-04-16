using System.Collections.Generic;
using RogueLib.Utilities;
namespace RogueLib.Dungeon.Tiles
{

    public abstract class Tile // The class is still abstract because I don't want "just a Tile"

    {
        protected int _tileId;
        protected Vector2 _tilePosition;
        protected bool _isWalkable;

        public Tile(int id)
        {
            _tileId = id;
            _tilePosition = Vector2.Zero;
        }


        // Use 'virtual' so children can use this logic or change it if they need to
        public virtual List<Tile> GetTileSpace()
        {
            List<Tile> neighbors = new List<Tile>();

            // This is where you use your Vector2 math!
            Vector2 north = _tilePosition + Vector2.N;
            Vector2 south = _tilePosition + Vector2.S;
            Vector2 east = _tilePosition + Vector2.E;
            Vector2 west = _tilePosition + Vector2.W;

            // Note: You'll eventually add logic here to grab the 
            // actual Tiles from your Level at these positions.

        // --- NEW: Add this so Vector2.Parse can tell the tile where it lives ---
        public void SetPosition(Vector2 position)
        {
            _tilePosition = position;
        }

        // Use 'virtual' so children can use this logic or change it if they need to
        // --- TEACHER'S LOGIC: Change GetTileSpace to use the Level Registry ---
        public virtual List<Tile> GetNeighbors(Dictionary<Vector2, Tile> levelMap)
        {
            List<Tile> neighbors = new List<Tile>();

            // Use the Cardinal directions from your Vector2 class
            Vector2[] directions = { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

            foreach (var dir in directions)
            {
                Vector2 targetPos = _tilePosition + dir;

                // Check the "Registry" (the Dictionary) to see if a tile exists there
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


}
