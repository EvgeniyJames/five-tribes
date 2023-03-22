#region

using System.Collections.Generic;

#endregion

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

        public List<Path> Path { get; } = new List<Path>();
    }
}