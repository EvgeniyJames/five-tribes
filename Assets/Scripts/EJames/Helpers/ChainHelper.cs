﻿#region

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
        private List<PathNode> _pathNodesStack = new List<PathNode>();

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

            ProcessCell(_firstCell, startCellMeeples);

            _startCell.Meeples.AddRange(startCellMeeples);
        }


        public void PrintPaths()
        {
            foreach (Movement movement in _possibleMovements)
            {
                PathPrinter.PrintMovement(movement);
            }
        }


        private void ProcessCell(Cell rootCell, List<Meeple> meeplesInHand)
        {
            Log($"ProcessCell: {rootCell}, {meeplesInHand.Count}");

            if (meeplesInHand.Count > 0)
            {
                Log("Neighbours");
                List<Cell> neighbours = _gridController.GetNeighbours(rootCell);
                foreach (Cell neighbour in neighbours)
                {
                    bool isLastMeeple = meeplesInHand.Count == 1;
                    if (isLastMeeple && !neighbour.HasAnyMeeples(meeplesInHand))
                    {
                        break;
                    }

                    bool alreadyChecked = false;
                    if (_pathNodesStack.Count == 0)
                    {
                        alreadyChecked = neighbour.Equals(_startCell);
                    }
                    else
                    {
                        for (int i = _pathNodesStack.Count - 1; i >= 0 && i >= _pathNodesStack.Count - 3; i--)
                        {
                            PathNode movement = _pathNodesStack[i];
                            Cell movementCell = movement.Cell;
                            if (neighbour.Equals(_startCell) ||
                                (movementCell.X == neighbour.X && movementCell.Y == neighbour.Y))
                            {
                                alreadyChecked = true;
                                break;
                            }
                        }
                    }

                    Log($"{neighbour}, goto? :{!alreadyChecked}");

                    if (!alreadyChecked)
                    {
                        List<Meeple> unionMeeples = rootCell.HasAnyMeeples() ? rootCell.GetUnionMeeples(meeplesInHand) : meeplesInHand;

                        Log("unionMeeples");
                        foreach (Meeple leftMeeple in unionMeeples.ToList())
                        {
                            Log($"Left {leftMeeple.Type.ToString()} on {rootCell}");
                            PathNode movement = new PathNode(rootCell, leftMeeple);

                            _pathNodesStack.Add(movement);
                            rootCell.Meeples.Add(leftMeeple);
                            meeplesInHand.Remove(leftMeeple);

                            ProcessCell(neighbour, meeplesInHand);

                            meeplesInHand.Add(leftMeeple);
                            rootCell.Meeples.Remove(leftMeeple);
                            _pathNodesStack.Remove(movement);
                        }
                    }
                }
            }
            else
            {
                AddMovement();
            }
        }


        private void Log(string log)
        {
            // Debug.Log(log);
        }


        private void AddMovement()
        {
            Movement movement = new Movement(_startCell, _firstCell);
            foreach (PathNode node in _pathNodesStack)
            {
                movement.Path.PathNodes.Add(node);
            }

            if (_possibleMovements.TrueForAll(m => !IsExist(m.Path, movement.Path)))
            {
                // Log("ADDED");
                //PrintPath(newPath);

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