#region

using System;
using System.Collections.Generic;
using EJames.Models;

#endregion

namespace EJames.Controllers
{
    public class PlayerSequenceController
    {
        private List<Player> _players = new List<Player>();

        private Player _currentPlayer;

        private int _playerIndex;

        public event Action CurrentPlayerChanged;

        public event Action PlayersEnded;

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            private set
            {
                if (value != _currentPlayer)
                {
                    _currentPlayer = value;
                    CurrentPlayerChanged?.Invoke();
                }
            }
        }

        public void Start(List<Player> players)
        {
            _players.Clear();
            foreach (Player player in players)
            {
                _players.Add(player);
            }

            _playerIndex = -1;
            SetNextPlayer();
        }

        public void SetNextPlayer()
        {
            _playerIndex++;
            if (_playerIndex >= _players.Count)
            {
                PlayersEnded?.Invoke();
            }
            else
            {
                CurrentPlayer = _players[_playerIndex];
            }
        }
    }
}