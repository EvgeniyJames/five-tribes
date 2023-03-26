#region

using EJames.Controllers;
using EJames.Models;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Presenters
{
    public class MeepleBagPresenter : MonoBehaviour
    {
        [SerializeField]
        private float _yDelta;

        [Inject]
        private MeepleBagController _meepleBagController;

        [Inject]
        private GridPresenter _gridPresenter;

        protected void OnEnable()
        {
            _meepleBagController.MeepleBag.MeeplePlaced += OnMeeplePlacedInBag;
        }

        protected void OnDisable()
        {
            _meepleBagController.MeepleBag.MeeplePlaced -= OnMeeplePlacedInBag;
        }

        private void OnMeeplePlacedInBag(Meeple meeple)
        {
            MeepleView meepleView = _gridPresenter.GetMeepleView(meeple);
            Transform meepleTransform = meepleView.transform;

            meepleTransform.SetParent(transform);
            meepleTransform.localPosition = Vector3.zero + new Vector3(0, _yDelta * transform.childCount, 0);
        }
    }
}