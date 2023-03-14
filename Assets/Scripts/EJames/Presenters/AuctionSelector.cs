#region

using EJames.Views;
using UnityEngine;

#endregion

namespace EJames.Presenters
{
    public class AuctionSelector : MonoBehaviour
    {
        [SerializeField]
        private AuctionView _auctionView;

        public void OnClick()
        {
            _auctionView.OnPlayerSelect();
        }
    }
}