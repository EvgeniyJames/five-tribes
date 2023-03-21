#region

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

            Cell startCell = _gridController.GetCell(0, 0);
            ChainHelper chainHelper = new ChainHelper(
                _gridController,
                startCell,
                _gridController.GetCell(1, 0),
                startCell.Meeples.Count);

            chainHelper.CalculateMovements();
        }
    }
}