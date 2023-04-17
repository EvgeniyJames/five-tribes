#region

using System;
using System.Collections.Generic;
using EJames.Helpers;
using EJames.Models;
using EJames.Popups;
using EJames.Presenters;
using EJames.Views;
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

        [Inject]
        private GridPresenter _gridPresenter;

        private State _state;

        private Cell _startCell;

        private List<Movement> _possibleMovements = new List<Movement>();
        private HashSet<Cell> _nextPossibleCells = new HashSet<Cell>();
        private Path _currentPath = new Path();

        private Cell _waitingCell;

        public event Action<Path> MovementFinished;


        public void Start()
        {
            CalculatePossibleCells();

            _cellSelectController.Clicked += SelectCell;
        }


        public void Finish()
        {
            _cellSelectController.Clicked -= SelectCell;
        }


        public void SelectCell(Cell cell)
        {
            if (_nextPossibleCells.Contains(cell))
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
            else
            {
                Debug.Log($"Can't peek {cell}");
            }
        }


        public void Movement(Cell cell, Meeple meeple)
        {
            _currentPath.PathNodes.Add(new PathNode(cell, meeple));
            _possibleMovements.RemoveAll(
                m =>
                {
                    int depth = _currentPath.PathNodes.Count - 1;
                    PathNode node = m.Path.PathNodes[depth];
                    return !node.Cell.Equals(cell) || !node.MeepleLeft.Equals(meeple);
                });

            PrintPossibleMovements();

            UpdatePossibleCellsWithDepth(_currentPath.PathNodes.Count);
            HighlightPossibleCells();

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
                CalculatePossibleCells();
                UpdateAndHighlight();
            }

            State state = isMoveDone ? State.StartCell : State.Moving;
            SetState(state);
        }


        private void UpdateAndHighlight()
        {
            UpdatePossibleCells();
            HighlightPossibleCells();
        }


        private void CalculatePossibleCells()
        {
            List<Cell> allPossibleStartCells = _possibleMovementController.GetAllPossibleStartCells();
            foreach (Cell startCell in allPossibleStartCells)
            {
                _nextPossibleCells.Add(startCell);
            }

            _possibleMovements.Clear();
            foreach (Cell possibleCell in _nextPossibleCells)
            {
                List<Movement> possibleMovementsByStartCell =
                    _possibleMovementController.GetPossibleMovementsByStartCell(possibleCell);
                _possibleMovements.AddRange(possibleMovementsByStartCell);
            }

            PrintPossibleMovements();
        }


        private void PrintPossibleMovements()
        {
            foreach (Movement movement in _possibleMovements)
            {
                PathPrinter.PrintMovement(movement);
            }
        }


        private void ProcessStart(Cell cell)
        {
            _playerHandController.SetMeeples(cell.Meeples);
            _startCell = cell;

            _possibleMovements = _possibleMovements.FindAll(m => m.StartCell.Equals(cell));
            PrintPossibleMovements();
            UpdateAndHighlight();

            SetState(State.Moving);
        }


        private void UpdatePossibleCells()
        {
            _nextPossibleCells.Clear();
            foreach (Movement possibleMovement in _possibleMovements)
            {
                _nextPossibleCells.Add(possibleMovement.FirstCell);
            }
        }


        private void UpdatePossibleCellsWithDepth(int depth)
        {
            _nextPossibleCells.Clear();
            if (depth < 3)
            {
                foreach (Movement possibleMovement in _possibleMovements)
                {
                    _nextPossibleCells.Add(possibleMovement.Path.PathNodes[depth].Cell);
                }
            }
        }


        private void HighlightPossibleCells()
        {
            _gridPresenter.HighlightOff();
            foreach (Cell cell in _nextPossibleCells)
            {
                CellView cellView = _gridPresenter.GetCellView(cell);
                cellView.ColorHighlighter.Highlight(Color.magenta);
            }
        }


        private void SetState(State state)
        {
            _state = state;
        }


        private void ProcessMoving(Cell cell)
        {
            if (_currentPath.PathNodes.Count == 0)
            {
                _possibleMovements = _possibleMovements.FindAll(m => m.FirstCell.Equals(cell));
                PrintPossibleMovements();
            }

            List<Meeple> canLeaveMeeplesHere = new List<Meeple>();
            foreach (Movement possibleMovement in _possibleMovements)
            {
                Path path = possibleMovement.Path;
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

            if (canLeaveMeeplesHere.Count > 0)
            {
                List<Meeple> unionMeeples = cell.HasAnyMeeples() ? cell.GetUnionMeeples(_playerHandController.Meeples) : _playerHandController.Meeples;
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