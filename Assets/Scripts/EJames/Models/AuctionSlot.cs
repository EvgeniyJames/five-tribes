#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class AuctionSlot
    {
        public int Price { get; set; }

        public int MaxPlayer { get; set; }

        public Stack<Player> Players { get; set; } = new Stack<Player>();

        public bool CanSeat => Players.Count < MaxPlayer;
    }
}