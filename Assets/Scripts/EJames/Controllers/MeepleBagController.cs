#region

using EJames.Models;
using EJames.Utility;

#endregion

namespace EJames.Controllers
{
    public class MeepleBagController : IInitable
    {
        private MeepleBag _meepleBag;

        public MeepleBag MeepleBag => _meepleBag;

        public void PlaceMeepleInBag(Meeple meeple)
        {
            _meepleBag.PlaceMeeple(meeple);
        }

        void IInitable.Init()
        {
            _meepleBag = new MeepleBag();
        }
    }
}