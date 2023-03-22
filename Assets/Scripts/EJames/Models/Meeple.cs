namespace EJames.Models
{
    public class Meeple
    {
        public Meeple(MeepleType type)
        {
            Type = type;
        }

        public MeepleType Type { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Meeple)obj);
        }

        public override int GetHashCode()
        {
            return (int)Type;
        }

        protected bool Equals(Meeple other)
        {
            return Type == other.Type;
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