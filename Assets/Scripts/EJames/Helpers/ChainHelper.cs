#region

using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private List<FieldMovement> _fieldMovements = new List<FieldMovement>();
        private List<PathNode> _pathNodesStack = new List<PathNode>();

        private List<Path> _paths = new List<Path>();


        public ChainHelper(GridController gridController, Cell startCell, Cell firstCell)
        {
            _gridController = gridController;
            _startCell = startCell;
            _firstCell = firstCell;
        }


        public List<Path> Paths => _paths;


        public void CalculateMovements()
        {
            List<Meeple> startCellMeeples = _startCell.Meeples.ToArray().ToList();
            _startCell.Meeples.Clear();

            ProcessCell(_firstCell, startCellMeeples);

            _startCell.Meeples.AddRange(startCellMeeples);
        }


        public void PrintPaths()
        {
            foreach (Path path in _paths)
            {
                PrintPath(path);
            }
        }


        private void PrintPath(Path path)
        {
            StringBuilder pathString = new StringBuilder();
            pathString.Append($"{_startCell}");

            foreach (PathNode movement in path.PathNodes)
            {
                pathString.Append($" -> {movement.Cell} ({movement.MeepleLeft.Type.ToString()})");
            }

            Debug.Log($"Path: {pathString}");
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
                AddPath();
            }
        }


        private void Log(string log)
        {
            // Debug.Log(log);
        }


        private void AddPath()
        {
            Path newPath = new Path();
            foreach (PathNode movement in _pathNodesStack)
            {
                newPath.PathNodes.Add(movement);
            }

            if (_paths.TrueForAll(path => !IsExist(path, newPath)))
            {
                // Log("ADDED");
                //PrintPath(newPath);

                _paths.Add(newPath);
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