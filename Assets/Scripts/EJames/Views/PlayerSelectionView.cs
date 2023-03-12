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
            Color playerColor = _colorsController.GetColorByIndex(_currentColorIndex);
            _colorView.color = playerColor;
            Model.Color = _currentColorIndex;
        }

        protected override void InitInternal()
        {
            base.InitInternal();

            _currentColorIndex = Model.Color;
            _id.text = Model.Id.ToString();

            Color playerColor = _colorsController.GetColorByIndex(_currentColorIndex);
            _colorView.color = playerColor;
        }
    }
}