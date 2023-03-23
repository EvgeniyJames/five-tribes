#region

using System;
using System.Collections.Generic;
using EJames.Controllers;
using EJames.Helpers;
using EJames.Models;
using EJames.Utility;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Presenters
{
    public class GridPresenter : MonoBehaviour
    {
        [SerializeField]
        private CellView _cellView;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private MeepleView _meepleView;

        [Inject]
        private GridController _gridController;

        [Inject]
        private Instantiator _instantiator;

        private List<CellView> _cellViews = new List<CellView>();
        private List<MeepleView> _meepleViews = new List<MeepleView>();

        private List<Movement> _possibleMovements = new List<Movement>();
        private HashSet<Cell> _possibleStartCells = new HashSet<Cell>();

        public MeepleView GetMeepleView(Meeple meeple)
        {
            MeepleView meepleView = null;
            int viewIndex = _meepleViews.FindIndex(mv => mv.Model.Equals(meeple));
            if (viewIndex > -1)
            {
                meepleView = _meepleViews[viewIndex];
            }

            return meepleView;
        }

        protected void Awake()
        {
            foreach (Cell cell in _gridController.Cells)
            {
                CellView cellView = _instantiator.InstantiatePrefab<CellView>(_cellView, _parent);
                _cellViews.Add(cellView);

                cellView.Init(cell);

                foreach (Meeple meeple in cell.Meeples)
                {
                    MeepleView meepleView = _instantiator.InstantiatePrefab<MeepleView>(_meepleView, _parent);
                    meepleView.name = $"MeepleView_{_meepleViews.Count}";

                    _meepleViews.Add(meepleView);

                    meepleView.Init(meeple);
                    cellView.SetMeeple(meepleView);
                }
            }

            TestChains();
        }

        private void TestChains()
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

            Debug.Log(DateTime.Now - startTime);
            Debug.Log($"Find {_possibleMovements.Count} possible movements");
            Debug.Log($"Find {_possibleStartCells.Count} possible start cells");
        }
    }
}