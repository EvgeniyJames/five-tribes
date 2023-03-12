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
            _meshRenderer.sharedMaterial = _colorMaterials[Model.Color];
        }
    }
}