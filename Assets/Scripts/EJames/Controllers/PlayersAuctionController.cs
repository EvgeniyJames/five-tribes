#region

using System.Collections.Generic;
using EJames.Models;

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

        public bool CanPlayerSeat(Player player, AuctionSlot slot)
        {
            return slot.Players.Count < slot.MaxPlayer;
        }

        public void SeatOn(Player player, AuctionSlot slot)
        {
            slot.Players.Push(player);
        }
    }
}