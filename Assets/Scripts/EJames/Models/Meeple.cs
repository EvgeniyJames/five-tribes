namespace EJames.Models
{
    public class Meeple
    {
        public Meeple(MeepleType type)
        {
            Type = type;
        }

        public MeepleType Type { get; }

        public override string ToString()
        {
            return nameof(Type);
        }

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