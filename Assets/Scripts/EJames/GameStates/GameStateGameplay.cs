#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Popups;
using EJames.Presenters;
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
        private PopupsController _popupsController;

        [Inject]
        private PlayerMovementController _playerMovementController;

        [Inject]
        private GridPresenter _gridPresenter;

        [Inject]
        private PossibleMovementController _possibleMovementController;

        void IGameState.OnEnter()
        {
            _popupsController.HidePopup<PopupLobby>();
            _popupsController.ShowPopup<GameplayHud>();

            List<Player> players = new List<Player>();
            for (int i = _playersAuctionController.AuctionSlots.Count - 1; i >= 0; i--)
            {
                AuctionSlot auctionSlot = _playersAuctionController.AuctionSlots[i];
                foreach (Player player in auctionSlot.Players)
                {
                    players.Add(player);
                }
            }

            _playerSequenceController.PlayersEnded += OnPlayersSequenceEnd;
            _playerSequenceController.Start(players);

            _playerMovementController.Start();

            // List<Cell> startCells = _possibleMovementController.GetAllPossibleStartCells();
            // foreach (Cell startCell in startCells)
            // {
            //     _gridPresenter.GetCellView(startCell).ColorHighlighter.Highlight(Color.magenta);
            // }
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