#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.GameStates
{
    public class GameStateGameplay : IGameState
    {
        [Inject]
        private PlayersAuctionController _playersAuctionController;

        [Inject]
        private PlayerSequenceController _playerSequenceController;

        [Inject]
        private PlayerOrderController _playerOrderController;

        void IGameState.OnEnter()
        {
            List<Player> players = new List<Player>();
            for (int i = _playersAuctionController.AuctionSlots.Count - 1; i >= 0; i--)
            {
                AuctionSlot auctionSlot = _playersAuctionController.AuctionSlots[i];
                foreach (Player player in auctionSlot.Players)
                {
                    Debug.Log(player.Id);
                    players.Add(player);
                }
            }

            _playerSequenceController.PlayersEnded += OnPlayersSequenceEnd;
            _playerSequenceController.Start(players);
        }

        void IGameState.OnExit()
        {
        }

        private void OnPlayersSequenceEnd()
        {
            _playerSequenceController.PlayersEnded -= OnPlayersSequenceEnd;
        }
    }
}