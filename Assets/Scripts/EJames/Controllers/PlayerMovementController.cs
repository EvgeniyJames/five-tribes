#region

using System.Collections.Generic;
using EJames.Models;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class PlayerMovementController
    {
        [Inject]
        private CellSelectController _cellSelectController;

        [Inject]
        private PlayerHandController _playerHandController;

        private State _state;

        private Cell _startCell;
        private List<Cell> _movementCells = new List<Cell>();

        public void Start()
        {
            _cellSelectController.Clicked += SelectCell;
        }

        public void Finish()
        {
            _cellSelectController.Clicked -= SelectCell;
        }

        private void SelectCell(Cell cell)
        {
            switch (_state)
            {
                case State.StartCell:
                    ProcessStart(cell);
                    break;
                case State.Moving:
                    ProcessMoving(cell);
                    break;
                case State.Finish:
                    ProcessFinish(cell);
                    break;
            }
        }

        private void ProcessStart(Cell cell)
        {
            _playerHandController.SetMeeples(cell.Meeples);

            _startCell = cell;
            _state = State.Moving;
        }

        private void ProcessMoving(Cell cell)
        {
            bool isMovementPossible;
            if (_movementCells.Count > 0)
            {
                int lastIndex = _movementCells.Count - 1;
                Cell lastMovementCell = _movementCells[lastIndex];
                isMovementPossible = lastMovementCell.IsNeighbour(cell);
            }
            else
            {
                isMovementPossible = _startCell.IsNeighbour(cell);
            }

            if (isMovementPossible)
            {
                foreach (Meeple cellMeeple in cell.Meeples)
                {
                    int handMeepleIndex = _playerHandController.Meeples.FindIndex(m => m.Type.Equals(cellMeeple.Type));
                    if (handMeepleIndex > -1)
                    {
                        _movementCells.Add(cell);
                        Meeple meeple = _playerHandController.Meeples[handMeepleIndex];

                        _startCell.RemoveMeeple(meeple);
                        cell.AddMeeple(meeple);

                        _playerHandController.Meeples.RemoveAt(handMeepleIndex);

                        if (_playerHandController.Meeples.Count == 0)
                        {
                            _state = State.Finish;
                        }

                        break;
                    }
                }
            }
            else
            {
                Debug.Log($"Can't go to {cell.X}, {cell.Y}");
            }
        }

        private void ProcessFinish(Cell cell)
        {
        }

        private enum State
        {
            StartCell,
            Moving,
            Finish
        }
    }
}