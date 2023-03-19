#region

using EJames.Controllers;
using EJames.Models;
using TMPro;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Popups
{
    public class GameplayHud : BasePopup
    {
        [SerializeField]
        private TMP_Text _player;

        [Inject]
        private PlayerSequenceController _playerSequenceController;

        protected void OnEnable()
        {
            _playerSequenceController.CurrentPlayerChanged += OnPlayerChanged;
            OnPlayerChanged();
        }

        protected void OnDisable()
        {
            _playerSequenceController.CurrentPlayerChanged -= OnPlayerChanged;
        }

        private void OnPlayerChanged()
        {
            Player player = _playerSequenceController.CurrentPlayer;
            if (player != null)
            {
                _player.text = $"Player: {player.Id}";
            }
        }
    }
}