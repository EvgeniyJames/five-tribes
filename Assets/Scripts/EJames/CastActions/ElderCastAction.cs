#region

using EJames.Controllers;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.CastActions
{
    public class ElderCastAction : ICastAction
    {
        [Inject]
        private PlayerMeeplesController _playerMeeplesController;

        Meeple.MeepleType ICastAction.Type => Meeple.MeepleType.Elders;

        void ICastAction.DoAction(CastActionController.Args args)
        {
            _playerMeeplesController.SetPlayerMeeples(args.Player, args.LastMeeples);
        }
    }
}