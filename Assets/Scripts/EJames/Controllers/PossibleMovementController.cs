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
            List<Cell> cells = _gridController.Cells;
            foreach (Cell cell in cells)
            {
                ProcessCellMovement(cell);
            }
        }

        private void ProcessCellMovement(Cell cell)
        {
            List<Meeple> cellMeeples = cell.Meeples;
            int movesCount = cellMeeples.Count;

            int x = cell.X;
            int y = cell.Y;
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
                            AddPossibleMovement(cell, nextCell, meeple);
                        }
                    }
                }
            }
        }

        private void AddPossibleMovement(Cell from, Cell to, Meeple meeple)
        {
            // _possibleMovements.FindIndex(pm => pm.CellFrom)
        }

        private class Movement
        {
            public Movement(Cell cellFrom, Cell cellTo, Meeple meeple)
            {
                CellFrom = cellFrom;
                CellTo = cellTo;
                Meeple = meeple;
            }

            public Cell CellFrom { get; }

            public Cell CellTo { get; }

            public Meeple Meeple { get; }
        }
    }
}