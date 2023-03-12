#region

using EJames.Controllers;
using EJames.Models;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Views
{
    public class OrderSlotView : BaseView<OrderSlot>
    {
        [Inject]
        private PlayerFieldPresentersController _playerFieldPresentersController;

        protected override void InitInternal()
        {
            base.InitInternal();
            Model.PlayerChanged += OnPlayerChanged;

            OnPlayerChanged(Model.Player);
        }

        private void OnPlayerChanged(Player player)
        {
            if (player != null)
            {
                PlayerFieldView playerFieldView = _playerFieldPresentersController.GetPlayerPresenter(player);

                Transform presenterTransform = playerFieldView.transform;
                presenterTransform.SetParent(transform);
                presenterTransform.localPosition = Vector3.zero;
            }
        }
    }
}