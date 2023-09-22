#region

using System;
using System.Collections.Generic;
using EJames.Models;

#endregion

namespace EJames.Controllers
{
    public class PlayerResourcesesController
    {
        private List<PlayerResourcesItem> _playerResourcesItems = new List<PlayerResourcesItem>();

        public event Action<Player> PlayerResourceChanged;

        public void SetPlayerResources(Player player, List<Resource> resources)
        {
            PlayerResourcesItem item;
            int itemIndex = _playerResourcesItems.FindIndex(i => i.Player.Equals(player));
            if (itemIndex > -1)
            {
                item = _playerResourcesItems[itemIndex];
            }
            else
            {
                item = new PlayerResourcesItem(player);
                _playerResourcesItems.Add(item);
            }

            foreach (Resource resource in resources)
            {
                item.Add(resource);
            }

            PlayerResourceChanged?.Invoke(player);
        }

        private class PlayerResourcesItem
        {
            private Player _player;
            private List<Resource> _resources = new List<Resource>();

            public PlayerResourcesItem(Player player)
            {
                _player = player;
            }

            public Player Player => _player;

            public void Add(Resource resource)
            {
                _resources.Add(resource);
            }
        }
    }
}