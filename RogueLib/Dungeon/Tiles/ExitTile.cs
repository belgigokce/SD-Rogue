using System;
using RogueLib.Dungeon;
namespace RlGameNS;

public class ExitTile : Tile
{

    public ExitTile(int id) : base(id, 'E')
    {

        IsWalkable = true; // You can always walk onto an exit

    }

    public override void SetTileSpace(int tileSpace)
    {
        // tileSpace 1 is the signal from MovePlayer that someone stepped here
        if (tileSpace == 1)
        {
            Console.SetCursorPosition(0, 25);
            Console.Write('\r' + "You've reached the stairs! Escaping...".PadRight(78));


        }
    }
}
