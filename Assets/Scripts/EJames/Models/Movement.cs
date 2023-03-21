#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class Movement
    {
        public Movement(Cell cell = null, Meeple meepleLeft = null)
        {
            Cell = cell;
            MeepleLeft = meepleLeft;
        }

        public Cell Cell { get; }

        public Meeple MeepleLeft { get; }

        public List<Movement> Movements { get; } = new List<Movement>();
    }
}