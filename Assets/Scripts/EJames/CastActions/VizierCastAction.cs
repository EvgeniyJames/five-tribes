#region

using EJames.Controllers;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.CastActions
{
    public class VizierCastAction : ICastAction
    {
        [Inject]
        private PlayerMeeplesController _playerMeeplesController;

        Meeple.MeepleType ICastAction.Type => Meeple.MeepleType.Viziers;


        void ICastAction.DoAction(CastActionController.Args args)
        {
            _playerMeeplesController.SetPlayerMeeples(args.Player, args.LastMeeples);
        }
    }
}