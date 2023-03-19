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
    }
}