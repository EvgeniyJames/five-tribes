#region

using System;
using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;
using UnityEngine;

#endregion

namespace EJames.Controllers
{
    public class PlayersController : IInitable
    {
        private List<Player> _players = new List<Player>();

        public event Action<Player> PlayerAdded;

        public List<Player> Players => _players;

        public bool CanAddPlayer => _players.Count < 4;

        public void AddPlayer(Player player)
        {
            _players.Add(player);
            PlayerAdded?.Invoke(player);
            Debug.Log(player.Id);
        }

        void IInitable.Init()
        {
        }
    }
}