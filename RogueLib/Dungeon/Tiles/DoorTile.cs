using System;
using System.Collections.Generic;
using RogueLib.Utilities;

namespace RlGameNS
{
    public class DoorTile : Tile
    {
        public bool IsOpen;

        public DoorTile(int id) : base(id, '+')
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
            // Logic: If tileSpace is 1 (the player "bumped" it), open the door!
            if (tileSpace == 1 && !IsOpen)
            {
                IsOpen = true;
                IsWalkable = true; // CRITICAL: Tell the engine we can walk here now!
                Console.Write('\r' + "The door creaks open...".PadRight(78));
            }
        }

    }
}
