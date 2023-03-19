#region

using System;
using System.Collections.Generic;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Views
{
    public class MeepleView : BaseView<Meeple>
    {
        [SerializeField]
        private List<MeepleViewData> _meepleViewData;

        protected override void InitInternal()
        {
            base.InitInternal();

            foreach (MeepleViewData meepleViewData in _meepleViewData)
            {
                meepleViewData.GO.SetActive(meepleViewData.MeepleType == Model.Type);
            }
        }

        [Serializable]
        private class MeepleViewData
        {
            [SerializeField]
            private Meeple.MeepleType _meepleType;

            [SerializeField]
            private GameObject _go;

            public Meeple.MeepleType MeepleType => _meepleType;

            public GameObject GO => _go;
        }
    }
}