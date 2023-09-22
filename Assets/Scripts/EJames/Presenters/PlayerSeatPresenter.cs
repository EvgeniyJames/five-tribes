#region

using System;
using System.Collections.Generic;
using EJames.Models;
using UnityEngine;
using Zenject;
using static EJames.Models.Meeple;

#endregion

namespace EJames.Presenters
{
    public class PlayerSeatPresenter : MonoBehaviour
    {
        [SerializeField]
        private List<MeeplePresenterItem> _items;

        [Inject]
        private GridPresenter _gridPresenter;

        public void SetMeeple(Meeple meeple)
        {
            int index = _items.FindIndex(i => i.Type.Equals(meeple.Type));
            if (index > -1)
            {
                MeeplePresenterItem item = _items[index];
                Views.MeepleView meepleView = _gridPresenter.GetMeepleView(meeple);
                item.SetMeeple(meepleView);
            }
        }

        [Serializable]
        private class MeeplePresenterItem
        {
            [SerializeField]
            private MeepleType _type;

            [SerializeField]
            private List<Transform> _parents;

            public MeepleType Type => _type;

            public List<Transform> Parents => _parents;

            public void SetMeeple(Views.MeepleView meepleView)
            {
                int parentIndex = _parents.FindIndex(t => t.childCount == 0);
                if (parentIndex > -1)
                {
                    meepleView.transform.SetParent(_parents[parentIndex]);
                    meepleView.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
}