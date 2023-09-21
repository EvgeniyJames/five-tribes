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

        [Inject]
        private PlayerCellsController _playerCellsController;

        [Inject]
        private PlayerSequenceController _playerSequenceController;

        [Inject]
        private CastActionController _castActionController;

        void IInitable.Init()
        {
            _playerMovementController.MovementFinished += OnMovementFinished;
        }

        private void OnMovementFinished(Path path)
        {
            PathNode lastNode = path.PathNodes[path.PathNodes.Count - 1];
            Cell lastNodeCell = lastNode.Cell;
            List<Meeple> sameMeeplesOnCell = lastNodeCell.GetMeeplesOfType(lastNode.MeepleLeft.Type);

            bool isCellClear = lastNodeCell.Meeples.Count == sameMeeplesOnCell.Count;
            if (isCellClear)
            {
                _playerCellsController.SetPlayerCell(_playerSequenceController.CurrentPlayer, lastNodeCell);
            }

            _castActionController.ProcessCastAction(
                new CastActionController.Args(
                        sameMeeplesOnCell,
                        lastNodeCell,
                        _playerSequenceController.CurrentPlayer
                    ));

            foreach (Meeple meeple in sameMeeplesOnCell)
            {
                lastNodeCell.RemoveMeeple(meeple);

                //TODO: rebase to cast actions
                _meepleBagController.PlaceMeepleInBag(meeple);
            }
        }
    }
}