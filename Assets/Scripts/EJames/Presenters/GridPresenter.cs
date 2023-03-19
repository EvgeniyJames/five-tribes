﻿#region

using System.Collections.Generic;
using EJames.Controllers;
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
                    meepleView.Init(meeple);
                    cellView.SetMeeple(meepleView);
                }
            }
        }
    }
}