#region

using System;
using System.Collections.Generic;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Views
{
    public class MeepleChooseView : BaseView<Meeple>
    {
        [SerializeField]
        private List<MeepleViewData> _meepleViewData;

        public event Action<Meeple> Selected;

        public void OnSelect()
        {
            Selected?.Invoke(Model);
        }

        protected override void InitInternal()
        {
            base.InitInternal();

            foreach (MeepleViewData meepleViewData in _meepleViewData)
            {
                meepleViewData.GO.SetActive(meepleViewData.MeepleType == Model.Type);
            }
        }
    }
}