#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class Cell
    {
        public Tile Tile { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public List<Meeple> Meeples { get; } = new List<Meeple>(3);
    }
}