#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Utility;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Presenters
{
    public class PlayerFieldPresenters : MonoBehaviour
    {
        [SerializeField]
        private PlayerFieldView _playerFieldViewPrefab;

        [Inject]
        private Instantiator _instantiator;

        [Inject]
        private PlayersController _playersController;

        [Inject]
        private PlayerOrderController _playerOrderController;

        private Dictionary<Player, PlayerFieldView> _playerPresenters = new Dictionary<Player, PlayerFieldView>();

        public PlayerFieldView GetPlayerPresenter(Player player)
        {
            return _playerPresenters[player];
        }

        public void CreatePlayerPresenter(Player player)
        {
            PlayerFieldView presenter =
                _instantiator.InstantiatePrefab<PlayerFieldView>(_playerFieldViewPrefab, transform);

            _playerPresenters.Add(player, presenter);

            presenter.Init(player);

            _playerOrderController.SetPlayerOrder(player, _playerPresenters.Count - 1);
        }

        protected void Awake()
        {
            _playersController.PlayerAdded += CreatePlayerPresenter;
        }

        protected void OnDestroy()
        {
            _playersController.PlayerAdded -= CreatePlayerPresenter;
        }
    }
}