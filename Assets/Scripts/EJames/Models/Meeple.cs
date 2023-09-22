namespace EJames.Models
{
    public class Meeple
    {
        public Meeple(MeepleType type)
        {
            Type = type;
        }

        public MeepleType Type { get; }

        public enum MeepleType
        {
            Viziers,
            Elders,
            Merchants,
            Builders,
            Assassins,
        }

        public override string ToString()
        {
            return nameof(Type);
        }
    }
}