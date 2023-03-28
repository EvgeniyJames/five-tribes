#region

using System;
using System.Collections.Generic;
using EJames.Helpers;
using EJames.Models;
using EJames.Utility;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class PossibleMovementController : IInitable
    {
        [Inject]
        private GridController _gridController;

        private List<Movement> _possibleMovements = new List<Movement>();

        public TimeSpan LastOperationTime { get; private set; }

        public List<Movement> PossibleMovements => _possibleMovements;

        public List<Movement> GetPossibleMovementsByStartCell(Cell startCell)
        {
            List<Movement> movements = new List<Movement>();

            foreach (Movement possibleMovement in _possibleMovements)
            {
                if (possibleMovement.StartCell.Equals(startCell))
                {
                    movements.Add(possibleMovement);
                }
            }

            return movements;
        }

        public List<Cell> GetAllPossibleStartCells()
        {
            List<Cell> startCells = new List<Cell>();
            foreach (Movement possibleMovement in PossibleMovements)
            {
                startCells.Add(possibleMovement.StartCell);
            }

            return startCells;
        }

        public void FindAllPossibleMovements()
        {
            DateTime startTime = DateTime.Now;

            _possibleMovements.Clear();

            foreach (Cell startCell in _gridController.Cells)
            {
                if (startCell.HasAnyMeeples())
                {
                    List<Cell> neighbours = _gridController.GetNeighbours(startCell);
                    foreach (Cell firstCell in neighbours)
                    {
                        ChainHelper chainHelper = new ChainHelper(_gridController, startCell, firstCell);
                        chainHelper.CalculateMovements();

                        if (chainHelper.Paths.Count > 0)
                        {
                            Movement movement = new Movement(startCell, firstCell);
                            movement.Paths.AddRange(chainHelper.Paths);

                            _possibleMovements.Add(movement);
                        }
                    }
                }
            }

            LastOperationTime = DateTime.Now - startTime;
        }

        void IInitable.Init()
        {
            FindAllPossibleMovements();
        }
    }
}