using System.Collections.Generic;
using RogueLib.Utilities;

namespace RogueLib.Dungeon.Tiles
{
    public class DoorTile : Tile
    {
        public bool IsOpen;

        public DoorTile(int id) : base(id)
        {
            IsOpen = false;
            IsWalkable = false; // Doors are usually closed blocks at first
        }

        public void Open()
        {
            IsOpen = true;
            IsWalkable = true;
        }

        // Notice: We don't HAVE to write GetTileSpace here anymore!
        // DoorTile already knows how to do it because of the Tile class.

        
        public override void SetTileSpace(int tileSpace)
        {
            // This is the implementation specific to the Door!
            // This is where you write the code for how a Door handles tileSpace.
        }

    }
}
