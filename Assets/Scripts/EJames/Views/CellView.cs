#region

using System;
using System.Collections.Generic;
using EJames.Models;
using TMPro;
using UnityEngine;

#endregion

namespace EJames.Views
{
    public class CellView : BaseView<Cell>
    {
        [SerializeField]
        private List<ColorItem> _colorItems;

        [SerializeField]
        private List<TileActionData> _tileActionData;

        [SerializeField]
        private TMP_Text _value;

        [Header("Meeples")]
        [SerializeField]
        private List<Transform> _meepleTransforms;

        private List<MeepleView> _meepleViews = new List<MeepleView>(3);

        public void SetMeeple(MeepleView meepleView)
        {
            _meepleViews.Add(meepleView);

            int parentIndex = _meepleTransforms.FindIndex(t => t.childCount == 0);
            if (parentIndex > -1)
            {
                Transform viewTransform = meepleView.transform;
                viewTransform.SetParent(_meepleTransforms[parentIndex]);
                viewTransform.localPosition = Vector3.zero;
            }
        }

        protected override void InitInternal()
        {
            base.InitInternal();

            var thisTransform = transform;
            thisTransform.name = $"{nameof(CellView)}_{Model.X}_{Model.Y}";
            thisTransform.localPosition = new Vector3(Model.X, 0, Model.Y);

            SetupCell();
        }

        private void SetupCell()
        {
            Tile tile = Model.Tile;
            foreach (ColorItem colorItem in _colorItems)
            {
                colorItem.GO.SetActive(colorItem.Color.Equals(tile.Color));
            }

            foreach (TileActionData colorItem in _tileActionData)
            {
                colorItem.GO.SetActive(colorItem.Action.Equals(tile.Action));
            }

            _value.text = tile.Value.ToString();
        }

        [Serializable]
        private class ColorItem
        {
            [SerializeField]
            private int _color;

            [SerializeField]
            private GameObject _go;

            public int Color => _color;

            public GameObject GO => _go;
        }

        [Serializable]
        private class TileActionData
        {
            [SerializeField]
            private Tile.TileAction _action;

            [SerializeField]
            private GameObject _go;

            public Tile.TileAction Action => _action;

            public GameObject GO => _go;
        }
    }
}