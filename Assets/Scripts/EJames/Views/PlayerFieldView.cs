#region

using System.Collections.Generic;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Views
{
    public class PlayerFieldView : BaseView<Player>
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private List<Material> _colorMaterials;


        protected override void InitInternal()
        {
            base.InitInternal();

            Model.ColorChanged += OnColorChanged;
            OnColorChanged();
        }


        private void OnColorChanged()
        {
            _meshRenderer.sharedMaterial = _colorMaterials[Model.Color];
        }
    }
}