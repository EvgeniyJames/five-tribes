#region

using System.Collections.Generic;
using System.Linq;
using EJames.Controllers;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class ChainHelper
    {
        private GridController _gridController;

        private Cell _startCell;
        private Cell _firstCell;

        private int _depth;

        private List<FieldMovement> _fieldMovements = new List<FieldMovement>();

        private List<Cell> _excludeCells = new List<Cell>();
        private HashSet<Cell> _finishedCells = new HashSet<Cell>();

        private List<Path> _paths = new List<Path>();

        public ChainHelper(GridController gridController, Cell startCell, Cell firstCell, int depth)
        {
            _gridController = gridController;
            _startCell = startCell;
            _firstCell = firstCell;
            _depth = depth;

            Debug.Log($"_start: {_startCell.X}, {_startCell.Y}; _first: {_firstCell.X}, {_firstCell.Y}; d: {_depth}");

            _excludeCells.Add(_startCell);
        }

        public void CalculateMovements()
        {
            Movement firstMovement = new Movement(_firstCell);
            ProcessCell(_firstCell, _depth - 1, firstMovement, _startCell.Meeples.ToList());

            FieldMovement fieldMovement = new FieldMovement(_startCell, firstMovement);
            int a = 0;
        }

        private void ProcessCell(Cell rootCell, int depth, Movement fieldMovement, List<Meeple> meeplesInHand)
        {
            Debug.Log($"ProcessCell: {rootCell.X}, {rootCell.Y}, {depth}");
            if (depth > 0)
            {
                foreach (Meeple meeple in meeplesInHand)
                {

                }






                int localPathsCount = 0;
                _excludeCells.Add(rootCell);
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        int neighbourX = rootCell.X + x;
                        int neighbourY = rootCell.Y + y;

                        if (_gridController.InField(neighbourX, neighbourY))
                        {
                            Cell neighbour = _gridController.GetCell(neighbourX, neighbourY);
                            if (!_excludeCells.Contains(neighbour) && neighbour.IsNeighbour(rootCell))
                            {
                                Movement movement = new Movement(neighbour);
                                fieldMovement.Movements.Add(movement);

                                // ProcessCell(neighbour, depth - 1, movement);
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("return");
                _finishedCells.Add(rootCell);
            }
        }
    }
}