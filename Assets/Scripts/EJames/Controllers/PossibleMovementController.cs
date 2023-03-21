#region

using System.Collections.Generic;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class PossibleMovementController
    {
        [Inject]
        private GridController _gridController;

        private List<Movement> _possibleMovements = new List<Movement>();

        public void FindCombinations()
        {
        }

        private void ProcessCellMovement(Cell startCell, Cell firstCell)
        {
            if (firstCell.HasAnyMeeples(startCell.Meeples))
            {
            }

            List<Cell> cells = _gridController.Cells;
            foreach (Cell cell in cells)
            {
                List<Meeple> meeples = cell.Meeples;
                bool canBeStartCell = !cell.Equals(startCell) && cell.HasAnyMeeples(meeples);
                if (canBeStartCell)
                {
                }
            }

            List<Meeple> cellMeeples = startCell.Meeples;
            int movesCount = cellMeeples.Count;

            int x = startCell.X;
            int y = startCell.Y;
            for (int moveIndex = 0; moveIndex < movesCount; moveIndex++)
            {
                x++;

                if (x > 0 && x < _gridController.FieldX)
                {
                    Cell nextCell = _gridController.GetCell(x, y);
                    for (int i = 0; i < nextCell.Meeples.Count; i++)
                    {
                        Meeple meeple = nextCell.Meeples[i];
                        if (cellMeeples.FindIndex(m => m.Type == meeple.Type) > -1)
                        {
                            AddPossibleMovement(startCell, nextCell, meeple);
                        }
                    }
                }
            }
        }

        private void AddPossibleMovement(Cell from, Cell to, Meeple meeple)
        {
            // _possibleMovements.FindIndex(pm => pm.CellFrom)
        }
    }
}