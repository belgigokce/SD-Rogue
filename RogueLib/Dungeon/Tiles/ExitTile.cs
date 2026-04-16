using System.Collections.Generic;
namespace RogueLib.Dungeon.Tiles
{
    public class ExitTile : Tile
    {
        public ExitTile(int id) : base(id)
        {
            IsWalkable = true; // You can always walk onto an exit
        }

        public override void SetTileSpace(int tileSpace)
        {
            // Implementation for ExitTile
        }
    }
}