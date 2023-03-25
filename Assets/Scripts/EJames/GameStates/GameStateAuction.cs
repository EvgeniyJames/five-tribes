#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.GameStates
{
    public class GameStateAuction : IGameState
    {
        [Inject]
        private PlayerSequenceController _playerSequenceController;

        [Inject]
        private PlayersAuctionController _playersAuctionController;

        [Inject]
        private GameStateController _gameStateController;

        [Inject]
        private PlayerOrderController _playerOrderController;

        [Inject]
        private GameTestController _gameTestController;

        void IGameState.OnEnter()
        {
            _gameTestController.StartGame();

            _playersAuctionController.PlayerSeat += OnPlayerSeat;
            _playerSequenceController.PlayersEnded += OnPlayersSequenceEnd;

            List<Player> players = new List<Player>();
            foreach (OrderSlot slot in _playerOrderController.OrderSlots)
            {
                if (slot.Player != null)
                {
                    players.Add(slot.Player);
                }
            }

            _playerSequenceController.Start(players);
        }

        void IGameState.OnExit()
        {
        }

        private void OnPlayerSeat()
        {
            _playerSequenceController.SetNextPlayer();
        }

        private void OnPlayersSequenceEnd()
        {
            _playersAuctionController.PlayerSeat -= OnPlayerSeat;
            _playerSequenceController.PlayersEnded -= OnPlayersSequenceEnd;

            _gameStateController.SetState(GameState.Gameplay);
        }
    }
}