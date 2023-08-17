#region

using System.Collections.Generic;
using System.Linq;
using EJames.Controllers;
using EJames.Models;

#endregion

namespace EJames.Helpers
{
    public class ChainHelper
    {
        private GridController _gridController;
        private Cell _startCell;
        private Cell _firstCell;

        private List<FieldMovement> _fieldMovements = new List<FieldMovement>();
        private Stack<PathNode> _pathNodesStack = new Stack<PathNode>();

        private List<Movement> _possibleMovements = new List<Movement>();

        public ChainHelper(GridController gridController, Cell startCell, Cell firstCell)
        {
            _gridController = gridController;
            _startCell = startCell;
            _firstCell = firstCell;
        }

        public List<Movement> PossibleMovements => _possibleMovements;

        public void CalculateMovements()
        {
            List<Meeple> startCellMeeples = _startCell.Meeples.ToArray().ToList();
            _startCell.Meeples.Clear();

            ProcessCell(_firstCell, startCellMeeples, _startCell);

            _startCell.Meeples.AddRange(startCellMeeples);
        }

        public void PrintPaths()
        {
            foreach (Movement movement in _possibleMovements)
            {
                PathPrinter.PrintMovement(movement);
            }
        }

        private void ProcessCell(Cell rootCell, List<Meeple> meeplesInHand, Cell from)
        {
            //Global condition to stop go deeper
            // If we are here, path complete
            if (meeplesInHand.Count > 0)
            {
                bool lastMeeple = meeplesInHand.Count == 1;
                List<Meeple> unionMeeples = new List<Meeple>();
                if (rootCell.HasAnyMeeples())
                {
                    unionMeeples = rootCell.GetUnionMeeples(meeplesInHand);
                }
                else if (!lastMeeple)
                {
                    unionMeeples = meeplesInHand;
                }

                foreach (Meeple leftMeeple in unionMeeples.ToList())
                {
                    PathNode movement = new PathNode(rootCell, leftMeeple);
                    _pathNodesStack.Push(movement);
                    rootCell.Meeples.Add(leftMeeple);
                    meeplesInHand.Remove(leftMeeple);

                    List<Cell> neighbours = _gridController.GetNeighboursWithout(rootCell, from);
                    foreach (Cell neighbour in neighbours)
                    {
                        ProcessCell(neighbour, meeplesInHand, rootCell);
                    }

                    meeplesInHand.Add(leftMeeple);
                    rootCell.Meeples.Remove(leftMeeple);
                    _pathNodesStack.Pop();
                }
            }
            else
            {
                AddMovement();
            }
        }

        private void AddMovement()
        {
            Movement movement = new Movement(_startCell, _firstCell);
            foreach (PathNode node in _pathNodesStack)
            {
                movement.Path.PathNodes.Add(node);
            }

            for (int i = 0; i < movement.Path.PathNodes.Count - 1; i++)
            {
                PathNode node = movement.Path.PathNodes[i];
                PathNode nextNode = movement.Path.PathNodes[i + 1];

                if (!node.Cell.IsNeighbour(nextNode.Cell))
                {
                    int a = 0;
                }

            }

            if (_possibleMovements.TrueForAll(m => !IsExist(m.Path, movement.Path)))
            {
                _possibleMovements.Add(movement);
            }
        }

        private bool IsExist(Path path, Path other)
        {
            bool exist = false;
            if (path.PathNodes.Count == other.PathNodes.Count)
            {
                for (int i = 0; i < other.PathNodes.Count; i++)
                {
                    if (!other.PathNodes[i].Equals(path.PathNodes[i]))
                    {
                        exist = false;
                        break;
                    }

                    exist = true;
                }
            }

            return exist;
        }
    }
}