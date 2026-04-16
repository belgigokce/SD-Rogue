namespace RogueLib.Dungeon.Tiles
{
    public class DoorTile : Tile
    {
        public bool IsOpen;

        public DoorTile(int id) : base(id)
        {
            IsOpen = false;
            IsWalkable = false;
        }

        public void Open()
        {
            IsOpen = true;
            IsWalkable = true;
        }

        public override void SetTileSpace(int tileSpace) { }
    }
}