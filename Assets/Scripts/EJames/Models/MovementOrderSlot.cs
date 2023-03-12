#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class MovementOrderSlot
    {
        public int Price { get; }
        public int MaxPlayers { get; }

        public Stack<Player> Players { get; } = new Stack<Player>();
    }
}