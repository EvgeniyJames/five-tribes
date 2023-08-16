#region

#endregion

using System;
using UnityEngine;

namespace EJames.Models
{
    public class Movement
    {
        public Movement(Cell startCell, Cell firstCell)
        {
            StartCell = startCell;
            FirstCell = firstCell;
        }

        public Cell StartCell { get; }

        public Cell FirstCell { get; }

        public Path Path { get; } = new Path();

        public override bool Equals(object other)
        {
            return Equals(other as Movement);
        }

        protected bool Equals(Movement other)
        {
            if (other == null)
            {
                return false;
            }

            return Equals(StartCell, other.StartCell) && Equals(FirstCell, other.FirstCell) && Equals(Path, other.Path);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartCell, FirstCell, Path);
        }
    }
}