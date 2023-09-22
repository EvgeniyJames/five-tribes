#region

using EJames.Controllers;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.CastActions
{
    public class MerchantCastAction : ICastAction
    {
        [Inject]
        private MeepleBagController _meepleBagController;

        Meeple.MeepleType ICastAction.Type => Meeple.MeepleType.Merchants;


        void ICastAction.DoAction(CastActionController.Args args)
        {
            foreach (Meeple meeple in args.LastMeeples)
            {
                _meepleBagController.PlaceMeepleInBag(meeple);
            }
        }
    }
}