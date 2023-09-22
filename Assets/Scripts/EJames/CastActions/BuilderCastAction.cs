#region

using System.Drawing;
using EJames.Controllers;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.CastActions
{
    public class BuilderCastAction : ICastAction
    {
        [Inject]
        private MeepleBagController _meepleBagController;

        [Inject]
        private GridController _gridController;

        [Inject]
        private ColorsController _colorsController;

        Meeple.MeepleType ICastAction.Type => Meeple.MeepleType.Builders;


        void ICastAction.DoAction(CastActionController.Args args)
        {
            foreach (Meeple meeple in args.LastMeeples)
            {
                _meepleBagController.PlaceMeepleInBag(meeple);
            }

            System.Collections.Generic.List<Cell> allNeighbours = _gridController.Get9Neighbours(args.LastCell);

            int colorIndex = _colorsController.GetColorIndex(Color.Blue);

            int _blueNeighbours = 0;
            foreach (Cell neighbour in allNeighbours)
            {
                if (neighbour.Tile.Color.Equals(colorIndex))
                {
                    _blueNeighbours++;
                }
            }

            //TODO: Add coins to player
            int coinsCount = _blueNeighbours * args.LastMeeples.Count;
        }
    }
}