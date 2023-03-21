#region

using System;
using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Popups
{
    public class PopupMeepleDecision : BasePopup
    {
        [SerializeField]
        private MeepleChooseView _meepleChooseView;

        [SerializeField]
        private Transform _parent;

        [Inject]
        private Instantiator _instantiator;

        private List<MeepleChooseView> _meepleChooseViews = new List<MeepleChooseView>();

        public const string Meeples = nameof(Meeples);

        public event Action<Meeple> ChooseCallback;

        protected override void ShowInternal()
        {
            base.ShowInternal();

            if (Args != null)
            {
                if (Args.TryGetValue(Meeples, out object meeplesGeneric) &&
                    meeplesGeneric is List<Meeple> meeples)
                {
                    foreach (Meeple meeple in meeples)
                    {
                        MeepleChooseView meepleChooseView =
                            _instantiator.InstantiatePrefab<MeepleChooseView>(_meepleChooseView, _parent);
                        meepleChooseView.Init(meeple);

                        meepleChooseView.Selected += OnMeepleSelected;

                        _meepleChooseViews.Add(meepleChooseView);
                    }
                }
            }
        }

        protected override void HideInternal()
        {
            base.HideInternal();

            foreach (MeepleChooseView view in _meepleChooseViews)
            {
                _instantiator.DestroyGameObject(view.gameObject);
            }

            _meepleChooseViews.Clear();
        }

        private void OnMeepleSelected(Meeple meeple)
        {
            ChooseCallback?.Invoke(meeple);
        }
    }
}