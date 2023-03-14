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

        public List<AuctionSlot> AuctionSlots => _auctionSlots;

        public bool CanPlayerSeat(Player player, AuctionSlot slot)
        {
            return slot.Players.Count < slot.MaxPlayer;
        }

        public bool TrySeatOn(Player player, AuctionSlot slot)
        {
            bool canPlayerSeat = CanPlayerSeat(player, slot);
            if (canPlayerSeat)
            {
                slot.AddPlayer(player);
            }

            return canPlayerSeat;
        }
    }
}