#region

using System;
using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class Cell
    {
        public event Action<Meeple> MeepleAdded;

        public event Action<Meeple> MeepleRemoved;

        public Tile Tile { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public List<Meeple> Meeples { get; } = new List<Meeple>(3);

        public bool IsNeighbour(Cell other)
        {
            bool isNeighbour = false;
            if (other.X == X)
            {
                isNeighbour = Math.Abs(other.Y - Y) == 1;
            }
            else if (other.Y == Y)
            {
                isNeighbour = Math.Abs(other.X - X) == 1;
            }

            return isNeighbour;
        }

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

            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tile, X, Y);
        }

        public void AddMeeple(Meeple meeple)
        {
            Meeples.Add(meeple);
            MeepleAdded?.Invoke(meeple);
        }

        public void RemoveMeeple(Meeple meeple)
        {
            Meeples.Remove(meeple);
            MeepleRemoved?.Invoke(meeple);
        }

        public bool HasAnyMeeples()
        {
            return Meeples.Count > 0;
        }

        public bool HasMeeple(Meeple meeple)
        {
            return Meeples.FindIndex(m => m.Type.Equals(meeple.Type)) > -1;
        }

        public bool HasAnyMeeples(List<Meeple> meeples)
        {
            bool hasMeeples = false;
            foreach (Meeple meeple in meeples)
            {
                hasMeeples |= HasMeeple(meeple);
            }

            return hasMeeples;
        }

        public List<Meeple> GetUnionMeeples(List<Meeple> meeples)
        {
            List<Meeple> meeplesToCheck = HasAnyMeeples() ? Meeples : meeples;
            List<Meeple> meeplesUnion = new List<Meeple>();

            foreach (Meeple meeple in meeplesToCheck)
            {
                int meepleIndex = meeples.FindIndex(m => m.Type == meeple.Type);
                if (meepleIndex > -1 &&
                    meeplesUnion.FindIndex(m => m.Type == meeple.Type) == -1)
                {
                    meeplesUnion.Add(meeples[meepleIndex]);
                }
            }

            return meeplesUnion;
        }

        public List<Meeple> GetMeeplesOfType(Meeple.MeepleType meepleType)
        {
            List<Meeple> meeples = new List<Meeple>();

            foreach (Meeple meeple in Meeples)
            {
                if (meeple.Type.Equals(meepleType))
                {
                    meeples.Add(meeple);
                }
            }

            return meeples;
        }

        public override string ToString()
        {
            return $"Cell ({X}, {Y})";
        }

        protected bool Equals(Cell other)
        {
            return Equals(Tile, other.Tile) && X == other.X && Y == other.Y;
        }
    }
}