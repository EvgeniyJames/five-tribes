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
        private HashSet<Cell> _possibleStartCells = new HashSet<Cell>();

        public TimeSpan LastOperationTime { get; private set; }

        public List<Movement> PossibleMovements => _possibleMovements;

        void IInitable.Init()
        {
            FindAllPossibleMovements();
        }

        private void FindAllPossibleMovements()
        {
            DateTime startTime = DateTime.Now;

            foreach (Cell startCell in _gridController.Cells)
            {
                foreach (Cell firstCell in _gridController.GetNeighbours(startCell))
                {
                    ChainHelper chainHelper = new ChainHelper(_gridController, startCell, firstCell);
                    chainHelper.CalculateMovements();

                    if (chainHelper.Paths.Count > 0)
                    {
                        Movement movement = new Movement(startCell, firstCell);
                        movement.Path.AddRange(chainHelper.Paths);

                        _possibleMovements.Add(movement);
                        _possibleStartCells.Add(startCell);
                    }
                }
            }

            LastOperationTime = DateTime.Now - startTime;
        }
    }
}