#region

using System.Collections.Generic;
using System.Threading.Tasks;
using EJames.Helpers;
using EJames.Models;
using EJames.Presenters;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class GameTestController
    {
        [Inject]
        private GridPresenter _gridPresenter;

        [Inject]
        private PlayerMovementController _playerMovementController;

        [Inject]
        private PossibleMovementController _possibleMovementController;

        [Inject]
        private GridController _gridController;

        private Dictionary<Meeple.MeepleType, Color> _meepleColors = new Dictionary<Meeple.MeepleType, Color>
        {
            { Meeple.MeepleType.Assassins, Color.red },
            { Meeple.MeepleType.Builders, Color.blue },
            { Meeple.MeepleType.Elders, Color.white },
            { Meeple.MeepleType.Merchants, Color.green },
            { Meeple.MeepleType.Viziers, Color.yellow },
        };

        private const int _delay = 300;

        public void StartGame()
        {
            DoGame();
        }

        private async void DoGame()
        {
            _playerMovementController.CalculatePossibleCells();
            while (_possibleMovementController.PossibleMovements.Count > 0)
            {
                Debug.Log($"{_possibleMovementController.PossibleMovements.Count} possible moves");

                Movement possibleMovement = _possibleMovementController.PossibleMovements[0];

                // _gridPresenter.GetCellView(possibleMovement.StartCell).ColorHighlighter.Highlight(Color.magenta);

                Debug.Log($"possibleMovement.StartCell: {possibleMovement.StartCell}");

                _playerMovementController.SelectCell(possibleMovement.StartCell);
                await Task.Delay(_delay);

                Path path = possibleMovement.Path;
                foreach (PathNode pathNode in path.PathNodes)
                {
                    _gridPresenter.GetCellView(pathNode.Cell)
                        .ColorHighlighter.Highlight(GetMeepleColor(pathNode.MeepleLeft));

                    await Task.Delay(_delay);
                }

                Debug.Log("I choose...");
                PathPrinter.PrintMovement(possibleMovement);

                await Task.Delay(_delay);

                foreach (PathNode pathNode in path.PathNodes)
                {
                    Debug.Log($"Step to: {pathNode.Cell}, left: {pathNode.MeepleLeft}");
                    _playerMovementController.Movement(pathNode.Cell, pathNode.MeepleLeft);
                    await Task.Delay(_delay);
                }

                Debug.Log("Path done");

                await Task.Delay(_delay);
                _gridPresenter.HighlightOff();
            }

            Debug.Log("_possibleMovementController.PossibleMovements.Count == 0");
        }

        private Color GetMeepleColor(Meeple meeple)
        {
            return _meepleColors[meeple.Type];
        }
    }
}