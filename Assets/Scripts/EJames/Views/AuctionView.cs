#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Presenters;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Views
{
    public class AuctionView : BaseView<AuctionSlot>
    {
        [SerializeField]
        private List<Transform> _innerAuctionPresenters;

        [SerializeField]
        private GameObject _selector;

        [Inject]
        private PlayersAuctionController _playersAuctionController;

        [Inject]
        private PlayerSequenceController _playerSequenceController;

        [Inject]
        private PlayerFieldPresenters _playerFieldPresenters;

        public void OnPlayerSelect()
        {
            if (_playerSequenceController.CurrentPlayer != null)
            {
                _playersAuctionController.TrySeatOn(_playerSequenceController.CurrentPlayer, Model);
            }
        }

        protected override void InitInternal()
        {
            base.InitInternal();

            Model.PlayersChanged += OnPlayersChanged;
        }

        private void OnPlayersChanged()
        {
            _selector.SetActive(Model.CanSeat);

            int i = 0;
            foreach (Player player in Model.Players)
            {
                PlayerFieldView playerPresenter = _playerFieldPresenters.GetPlayerPresenter(player);
                playerPresenter.SetParent(_innerAuctionPresenters[i++]);
            }
        }
    }
}