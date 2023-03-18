#region

using System;
using System.Collections.Generic;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Controllers
{
    public class PlayersAuctionController
    {
        private List<AuctionSlot> _auctionSlots = new List<AuctionSlot>
        {
            new AuctionSlot
            {
                Price = 0,
                MaxPlayer = 3
            },
            new AuctionSlot
            {
                Price = 1,
                MaxPlayer = 1
            },
            new AuctionSlot
            {
                Price = 3,
                MaxPlayer = 1
            },
        };

        public event Action PlayerSeat;

        public List<AuctionSlot> AuctionSlots => _auctionSlots;

        public bool TrySeatOn(Player player, AuctionSlot slot)
        {
            bool canPlayerSeat = slot.CanSeat;
            if (canPlayerSeat)
            {
                Debug.Log($"{player.Id} seat on {slot.Price}");

                slot.AddPlayer(player);
                PlayerSeat?.Invoke();
            }

            return canPlayerSeat;
        }
    }
}