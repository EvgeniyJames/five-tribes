#region

using System.Collections.Generic;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Views
{
    public class AuctionView : BaseView<AuctionSlot>
    {
        [SerializeField]
        private List<Transform> _innerAuctionPresenters;

        [SerializeField]
        private GameObject _selector;

        public void OnPlayerSelect()
        {
            Debug.Log(Model.Price);
        }

        protected override void InitInternal()
        {
            base.InitInternal();

            Model.PlayersChanged += OnPlayersChanged;
        }

        private void OnPlayersChanged()
        {
            _selector.SetActive(Model.CanSeat);
        }
    }
}