namespace RogueLib.Dungeon.Tiles
{
    public class ExitTile : Tile
    {
        public ExitTile(int id) : base(id)
        {
            IsWalkable = true;
        }

        public override void SetTileSpace(int tileSpace) { }
    }
}