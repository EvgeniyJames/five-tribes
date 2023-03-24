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
                StringBuilder pathString = new StringBuilder();
                foreach (PathNode movement in path.PathNodes)
                {
                    pathString.Append(
                        $"{movement.MeepleLeft.Type.ToString()} on ({movement.Cell.X}:{movement.Cell.Y}); ");
                }

                Debug.Log($"Path: {pathString}");
            }
        }

        private void ProcessCell(Cell rootCell, List<Meeple> meeplesInHand)
        {
            if (meeplesInHand.Count > 0)
            {
                List<Cell> neighbours = _gridController.GetNeighbours(rootCell);
                foreach (Cell neighbour in neighbours)
                {
                    bool alreadyChecked = false;
                    for (int i = _pathNodesStack.Count - 1, j = 0; i >= 0 && j < 2; i--, j++)
                    {
                        PathNode movement = _pathNodesStack[i];
                        if (movement.Cell.X == neighbour.X && movement.Cell.Y == neighbour.Y)
                        {
                            alreadyChecked = true;
                            break;
                        }
                    }

                    if (!alreadyChecked)
                    {
                        List<Meeple> unionMeeples = meeplesInHand;
                        if (rootCell.HasAnyMeeples())
                        {
                            unionMeeples = rootCell.GetUnionMeeples(meeplesInHand);
                        }

                        foreach (Meeple leftMeeple in unionMeeples.ToList())
                        {
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

        private void AddPath()
        {
            Path newPath = new Path();
            foreach (PathNode movement in _pathNodesStack)
            {
                newPath.PathNodes.Add(movement);
            }

            if (_paths.TrueForAll(path => !IsExist(path, newPath)))
            {
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
                    if (other.PathNodes[i].Equals(path.PathNodes[i]))
                    {
                        exist = true;
                        break;
                    }
                }
            }

            return exist;
        }

        private List<Meeple> GetAddedMeeplesOnCell(Cell cell)
        {
            List<Meeple> meeples = new List<Meeple>();
            foreach (PathNode pathNode in _pathNodesStack)
            {
                if (pathNode.Cell.Equals(cell))
                {
                    meeples.Add(pathNode.MeepleLeft);
                }
            }

            return meeples;
        }
    }
}