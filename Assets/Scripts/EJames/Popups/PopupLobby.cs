#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.GameStates;
using EJames.Models;
using EJames.Utility;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Popups
{
    public class PopupLobby : BasePopup
    {
        [SerializeField]
        private PlayerSelectionView _playerViewPrefab;

        [SerializeField]
        private Transform _playersParent;

        [Inject]
        private PlayersController _playersController;

        [Inject]
        private GameStateController _gameStateController;

        [Inject]
        private ColorsController _colorsController;

        [Inject]
        private Instantiator _instantiator;

        private Dictionary<Player, PlayerSelectionView> _playerSelectionViews =
            new Dictionary<Player, PlayerSelectionView>();

        private int _userCount;

        public void OnAddPlayer()
        {
            if (_playersController.CanAddPlayer)
            {
                Player newPlayer = new Player
                {
                    Id = _userCount++,
                    Color = _colorsController.GetNextFreeColorIndex(-1)
                };
                _playersController.AddPlayer(newPlayer);

                PlayerSelectionView playerView =
                    _instantiator.InstantiatePrefab<PlayerSelectionView>(_playerViewPrefab, _playersParent);
                playerView.Init(newPlayer);
            }
            else
            {
                Debug.Log($"_playersController.Players.Count: {_playersController.Players.Count}");
            }
        }

        public void OnStart()
        {
            _gameStateController.SetState(GameState.Auction);
        }
    }
}