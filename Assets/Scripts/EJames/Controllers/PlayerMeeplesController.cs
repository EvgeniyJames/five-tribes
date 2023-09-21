#region

using System;
using System.Collections.Generic;
using EJames.Models;

#endregion

namespace EJames.Controllers
{
    public class PlayerMeeplesController
    {
        private List<PlayerMeepleItem> _playerMeepleItems = new List<PlayerMeepleItem>();

        public event Action<List<Meeple>> PlayerMeeplesChanged;


        public void SetPlayerMeeples(Player player, List<Meeple> meeples)
        {
            PlayerMeepleItem item;
            int itemIndex = _playerMeepleItems.FindIndex(i => i.Player.Equals(player));
            if (itemIndex > -1)
            {
                item = _playerMeepleItems[itemIndex];
            }
            else
            {
                item = new PlayerMeepleItem(player);
                _playerMeepleItems.Add(item);
            }

            foreach (Meeple meeple in meeples)
            {
                item.Add(meeple);
            }

            PlayerMeeplesChanged?.Invoke(meeples);
        }


        private class PlayerMeepleItem
        {
            private Player _player;
            private List<Meeple> _meeples = new List<Meeple>();


            public PlayerMeepleItem(Player player)
            {
                _player = player;
            }


            public Player Player => _player;


            public void Add(Meeple meeple)
            {
                _meeples.Add(meeple);
            }
        }
    }
}