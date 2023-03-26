#region

using System;
using System.Collections.Generic;
using EJames.Models;
using EJames.Popups;
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

        private List<Movement> _possibleMovements;
        private Path _currentPath = new Path();

        private Cell _waitingCell;

        public event Action<Path> MovementFinished;

        public void Start()
        {
            _cellSelectController.Clicked += SelectCell;
        }

        public void Finish()
        {
            _cellSelectController.Clicked -= SelectCell;
        }

        public void SelectCell(Cell cell)
        {
            switch (_state)
            {
                case State.StartCell:
                    ProcessStart(cell);
                    break;
                case State.Moving:
                    ProcessMoving(cell);
                    break;
            }
        }

        public void Movement(Cell cell, Meeple meeple)
        {
            _currentPath.PathNodes.Add(new PathNode(cell, meeple));

            _startCell.RemoveMeeple(meeple);
            cell.AddMeeple(meeple);

            _playerHandController.Meeples.Remove(meeple);
            bool isMoveDone = _playerHandController.Meeples.Count == 0;
            if (isMoveDone)
            {
                MovementFinished?.Invoke(_currentPath);

                _startCell = null;
                _currentPath.PathNodes.Clear();

                _possibleMovementController.FindAllPossibleMovements();
            }

            State state = isMoveDone ? State.StartCell : State.Moving;
            SetState(state);
        }

        private void ProcessStart(Cell cell)
        {
            List<Movement> possibleMovementsByStartCell =
                _possibleMovementController.GetPossibleMovementsByStartCell(cell);
            if (possibleMovementsByStartCell.Count > 0)
            {
                _possibleMovements = possibleMovementsByStartCell;
                _playerHandController.SetMeeples(cell.Meeples);

                _startCell = cell;
                SetState(State.Moving);
            }
        }

        private void SetState(State state)
        {
            _state = state;
        }

        private void ProcessMoving(Cell cell)
        {
            List<Meeple> canLeaveMeeplesHere = new List<Meeple>();
            if (_currentPath.PathNodes.Count == 0)
            {
                _possibleMovements = _possibleMovements.FindAll(m => m.FirstCell.Equals(cell));
            }

            foreach (Movement possibleMovement in _possibleMovements)
            {
                foreach (Path path in possibleMovement.Paths)
                {
                    if (path.PathNodes.Count > _currentPath.PathNodes.Count)
                    {
                        PathNode pathNode = path.PathNodes[_currentPath.PathNodes.Count];
                        if (pathNode.Cell.Equals(cell) &&
                            canLeaveMeeplesHere.FindIndex(m => pathNode.MeepleLeft.Type.Equals(m.Type)) == -1)
                        {
                            canLeaveMeeplesHere.Add(pathNode.MeepleLeft);
                        }
                    }
                }
            }

            if (canLeaveMeeplesHere.Count > 0)
            {
                List<Meeple> unionMeeples = cell.HasAnyMeeples() ?
                    cell.GetUnionMeeples(_playerHandController.Meeples) :
                    _playerHandController.Meeples;
                if (unionMeeples.Count > 1)
                {
                    SetState(State.WaitPlayerDecision);
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
        }
    }
}