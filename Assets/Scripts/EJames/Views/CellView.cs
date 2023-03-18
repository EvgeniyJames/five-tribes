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