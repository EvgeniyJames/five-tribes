#region

using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class PlayerMovementFinishController : IInitable
    {
        [Inject]
        private PlayerMovementController _playerMovementController;

        [Inject]
        private MeepleBagController _meepleBagController;

        void IInitable.Init()
        {
            _playerMovementController.MovementFinished += OnMovementFinished;
        }

        private void OnMovementFinished(Path path)
        {
            PathNode lastNode = path.PathNodes[path.PathNodes.Count - 1];
            List<Meeple> sameMeeplesOnCell = lastNode.Cell.GetMeeplesOfType(lastNode.MeepleLeft.Type);

            foreach (Meeple meeple in sameMeeplesOnCell)
            {
                lastNode.Cell.RemoveMeeple(meeple);
                _meepleBagController.PlaceMeepleInBag(meeple);
            }
        }
    }
}