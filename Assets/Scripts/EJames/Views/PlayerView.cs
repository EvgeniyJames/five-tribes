#region

using EJames.Models;
using TMPro;
using UnityEngine;

#endregion

namespace EJames.Views
{
    public class PlayerView : BaseView<Player>
    {
        [SerializeField]
        private TMP_Text _playerId;

        protected override void InitInternal()
        {
            base.InitInternal();
            _playerId.text = Model.Id.ToString();
        }
    }
}