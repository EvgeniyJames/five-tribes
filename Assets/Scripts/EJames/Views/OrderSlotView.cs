#region

using EJames.Models;
using EJames.Presenters;
using Zenject;

#endregion

namespace EJames.Views
{
    public class OrderSlotView : BaseView<OrderSlot>
    {
        [Inject]
        private PlayerFieldPresenters _playerFieldPresenters;

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
                PlayerFieldView playerFieldView = _playerFieldPresenters.GetPlayerPresenter(player);
                playerFieldView.SetParent(transform);
            }
        }
    }
}