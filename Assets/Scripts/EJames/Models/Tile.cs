namespace EJames.Models
{
    public class Tile
    {
        public int Value { get; set; }

        public int Color { get; set; }

        public TileAction Action { get; set; } = TileAction.None;

        public enum TileAction
        {
            None,
            Oasis,
            Village,
            SmallMarket,
            LargeMarket,
            SacredPlaces,
        }
    }
}