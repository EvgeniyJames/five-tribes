namespace EJames.Models
{
    public class Meeple
    {
        public int Color { get; set; }

        public MeepleType Type { get; set; }

        public enum MeepleType
        {
            Viziers,
            Elders,
            Merchants,
            Builders,
            Assassins,
        }
    }
}