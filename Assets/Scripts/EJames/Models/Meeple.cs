namespace EJames.Models
{
    public class Meeple
    {
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