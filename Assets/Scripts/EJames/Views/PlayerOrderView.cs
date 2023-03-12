#region

using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Views
{
    public class PlayerOrderView : BaseView<List<Player>>
    {
        [SerializeField]
        private PlayerView _playerViewPrefab;

        [SerializeField]
        private Transform _parent;

        [Inject]
        private Instantiator _instantiator;

        private List<PlayerView> _playerViews = new List<PlayerView>();

        protected override void InitInternal()
        {
            base.InitInternal();

            foreach (Player player in Model)
            {
                PlayerView playerView = _instantiator.InstantiatePrefab<PlayerView>(_playerViewPrefab, _parent);
                playerView.Init(player);

                _playerViews.Add(playerView);
            }
        }
    }
}