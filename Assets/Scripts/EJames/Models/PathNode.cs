#region

using System;

#endregion

namespace EJames.Models
{
    public class PathNode
    {
        public PathNode(Cell cell, Meeple meepleLeft)
        {
            Cell = cell;
            MeepleLeft = meepleLeft;
        }

        public Cell Cell { get; }

        public Meeple MeepleLeft { get; }

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

            return Equals((PathNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cell, MeepleLeft);
        }

        protected bool Equals(PathNode other)
        {
            return Equals(Cell, other.Cell) && Equals(MeepleLeft, other.MeepleLeft);
        }
    }
}