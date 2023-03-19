#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Presenters
{
    public class PlayerHandPresenter : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> _meeplesTransforms;

        [Inject]
        private PlayerHandController _playerHandController;

        [Inject]
        private GridPresenter _gridPresenter;

        protected void OnEnable()
        {
            _playerHandController.MeeplesUpdated += OnPlayerHandUpdated;
        }

        protected void OnDisable()
        {
            _playerHandController.MeeplesUpdated -= OnPlayerHandUpdated;
        }

        private void OnPlayerHandUpdated()
        {
            for (int i = 0; i < _playerHandController.Meeples.Count; i++)
            {
                Meeple meeple = _playerHandController.Meeples[i];
                MeepleView meepleView = _gridPresenter.GetMeepleView(meeple);
                meepleView.transform.position = _meeplesTransforms[i].position;
            }
        }
    }
}