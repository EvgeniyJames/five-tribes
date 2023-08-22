#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Presenters;
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

        [Inject]
        private PlayerAuctionPresenter _playerAuctionPresenter;

        private const bool _botAllowed = false;

        void IGameState.OnEnter()
        {
            _playerAuctionPresenter.gameObject.SetActive(true);

            if (_botAllowed)
            {
                _gameTestController.StartGame();
            }
            else
            {
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
        }

        void IGameState.OnExit()
        {
            _playerAuctionPresenter.gameObject.SetActive(false);
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