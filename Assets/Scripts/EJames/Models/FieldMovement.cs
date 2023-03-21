#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class FieldMovement
    {
        public FieldMovement(Cell startCell, Movement movement)
        {
            StartCell = startCell;
            FirstMovement = movement;
        }

        public Movement FirstMovement { get; }

        public Cell StartCell { get; }

        public List<Path> GetAllPaths()
        {
            List<Path> paths = new List<Path>();

            return paths;
        }
    }
}