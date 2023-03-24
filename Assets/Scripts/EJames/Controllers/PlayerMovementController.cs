#region

using System.Collections.Generic;
using System.Text;
using EJames.Models;
using EJames.Popups;
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

        [Inject]
        private PopupsController _popupsController;

        [Inject]
        private PossibleMovementController _possibleMovementController;

        private State _state;

        private Cell _startCell;
        private List<Cell> _movementCells = new List<Cell>();

        private List<Movement> _possibleMovements;

        private Cell _waitingCell;

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
            List<Movement> possibleMovementsByStartCell =
                _possibleMovementController.GetPossibleMovementsByStartCell(cell);
            if (possibleMovementsByStartCell.Count > 0)
            {
                _possibleMovements = possibleMovementsByStartCell;

                StringBuilder sb = new StringBuilder();
                foreach (Movement possibleMovement in _possibleMovements)
                {
                    sb.Append($"{possibleMovement.StartCell}");

                    foreach (Path path in possibleMovement.Path)
                    {
                        foreach (PathNode pathNode in path.PathNodes)
                        {
                            sb.Append($" -> {pathNode.Cell} ({pathNode.MeepleLeft.Type.ToString()})");
                        }

                        Debug.Log(sb);
                        sb.Clear();
                    }
                }

                _playerHandController.SetMeeples(cell.Meeples);

                _startCell = cell;
                _state = State.Moving;
            }
            else
            {
                Debug.Log($"{cell.X}, {cell.Y} can't be start cell");
            }
        }

        private void ProcessMoving(Cell cell)
        {
            bool isMovementPossible;
            bool hasAnyMeeples = cell.HasAnyMeeples() || cell.HasAnyMeeples(_playerHandController.Meeples);
            if (_movementCells.Count > 0)
            {
                int lastIndex = _movementCells.Count - 1;
                Cell lastMovementCell = _movementCells[lastIndex];
                isMovementPossible = lastMovementCell.IsNeighbour(cell) &&
                    hasAnyMeeples;
            }
            else
            {
                isMovementPossible = !_startCell.Equals(cell) && hasAnyMeeples;
            }

            if (isMovementPossible)
            {
                List<Meeple> unionMeeples = cell.GetUnionMeeples(_playerHandController.Meeples);
                if (unionMeeples.Count > 1)
                {
                    _state = State.WaitPlayerDecision;
                    _waitingCell = cell;

                    _popupsController.ShowPopup<PopupMeepleDecision>(
                        new Dictionary<string, object>
                        {
                            { PopupMeepleDecision.Meeples, unionMeeples },
                        });

                    PopupMeepleDecision popupMeepleDecision = _popupsController.GetPopup<PopupMeepleDecision>();
                    popupMeepleDecision.ChooseCallback += OnMeepleChoose;
                }
                else
                {
                    Movement(cell, unionMeeples[0]);
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

        private void Movement(Cell cell, Meeple meeple)
        {
            _movementCells.Add(cell);
            _startCell.RemoveMeeple(meeple);
            cell.AddMeeple(meeple);

            _playerHandController.Meeples.Remove(meeple);
            bool isMoveDone = _playerHandController.Meeples.Count == 0;
            if (isMoveDone)
            {
                _movementCells.Clear();
                _startCell = null;
            }

            _state = isMoveDone ? State.StartCell : State.Moving;
        }

        private void OnMeepleChoose(Meeple meeple)
        {
            Movement(_waitingCell, meeple);

            PopupMeepleDecision popupMeepleDecision = _popupsController.GetPopup<PopupMeepleDecision>();
            popupMeepleDecision.ChooseCallback -= OnMeepleChoose;

            _popupsController.HidePopup<PopupMeepleDecision>();
        }

        private enum State
        {
            StartCell,
            Moving,
            WaitPlayerDecision,
            Finish
        }
    }
}