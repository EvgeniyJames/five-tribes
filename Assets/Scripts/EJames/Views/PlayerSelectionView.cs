#region

using EJames.Controllers;
using EJames.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

#endregion

namespace EJames.Views
{
    public class PlayerSelectionView : BaseView<Player>
    {
        [SerializeField]
        private Image _colorView;

        [SerializeField]
        private TMP_Text _id;

        [Inject]
        private ColorsController _colorsController;

        private int _currentColorIndex;

        public void OnNextColor()
        {
            SetColorIndex(_colorsController.GetNextFreeColorIndex(_currentColorIndex));
        }

        public void OnPreviousColor()
        {
            SetColorIndex(_colorsController.GetPreviousFreeColorIndex(_currentColorIndex));
        }

        private void SetColorIndex(int index)
        {
            if (index != _currentColorIndex)
            {
                _currentColorIndex = index;
                OnColorChanged();
            }
        }

        private void OnColorChanged()
        {
            SetColor(_colorsController.GetColorByIndex(_currentColorIndex));
            Model.Color = _currentColorIndex;
        }

        private void SetColor(System.Drawing.Color playerColor)
        {
            _colorView.color = new Color(playerColor.R, playerColor.G, playerColor.B, playerColor.A);
        }

        protected override void InitInternal()
        {
            base.InitInternal();

            _currentColorIndex = Model.Color;
            _id.text = Model.Id.ToString();

            SetColor(_colorsController.GetColorByIndex(_currentColorIndex));
        }
    }
}