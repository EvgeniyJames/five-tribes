#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Presenters
{
    public class PlayerAuctionPresenter : MonoBehaviour
    {
        [SerializeField]
        private List<AuctionView> _auctionViews;

        [Inject]
        private PlayersAuctionController _playersAuctionController;

        protected void Awake()
        {
            for (int i = 0; i < _playersAuctionController.AuctionSlots.Count; i++)
            {
                AuctionSlot auctionSlot = _playersAuctionController.AuctionSlots[i];
                _auctionViews[i].Init(auctionSlot);
            }
        }
    }
}