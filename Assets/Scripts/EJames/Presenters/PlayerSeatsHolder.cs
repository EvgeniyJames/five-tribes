using System.Collections.Generic;
using EJames.Controllers;
using UnityEngine;
using Zenject;

namespace EJames.Presenters
{
    public class PlayerSeatsHolder : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerSeatPresenter> _presenters;

        [Inject]
        private PlayerMeeplesController _playerMeeplesController;

        protected void OnEnable()
        {
            _playerMeeplesController.PlayerMeeplesChanged += OnPlayerMeeplesChanged;
        }

        private void OnPlayerMeeplesChanged(List<Models.Meeple> meeples)
        {
            PlayerSeatPresenter playerSeatPresenter = _presenters[0];
            foreach (Models.Meeple meeple in meeples)
            {
                playerSeatPresenter.SetMeeple(meeple);
            }
        }
    }
}

