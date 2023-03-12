#region

using System;

#endregion

namespace EJames.Models
{
    public class OrderSlot
    {
        private Player _player;

        public event Action<Player> PlayerChanged;

        public int Order { get; set; }

        public Player Player
        {
            get => _player;
            set
            {
                if (_player != value)
                {
                    _player = value;
                    PlayerChanged?.Invoke(Player);
                }
            }
        }
    }
}