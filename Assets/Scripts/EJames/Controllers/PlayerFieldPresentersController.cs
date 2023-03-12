﻿#region

using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class PlayerFieldPresentersController : MonoBehaviour
    {
        [SerializeField]
        private PlayerFieldView _playerFieldViewPrefab;

        [Inject]
        private Instantiator _instantiator;

        [Inject]
        private PlayersController _playersController;

        private Dictionary<Player, PlayerFieldView> _playerPresenters = new Dictionary<Player, PlayerFieldView>();

        public PlayerFieldView GetPlayerPresenter(Player player)
        {
            return _playerPresenters[player];
        }

        public void AddPlayerPresenter(Player player)
        {
            PlayerFieldView presenter =
                _instantiator.InstantiatePrefab<PlayerFieldView>(_playerFieldViewPrefab, transform);

            _playerPresenters.Add(player, presenter);

            presenter.Init(player);
        }

        protected void Awake()
        {
            _playersController.PlayerAdded += AddPlayerPresenter;
        }

        protected void OnDestroy()
        {
            _playersController.PlayerAdded -= AddPlayerPresenter;
        }
    }
}