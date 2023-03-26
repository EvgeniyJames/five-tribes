#region

using System.Collections.Generic;
using System.Threading.Tasks;
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

        public void StartGame()
        {
            DoGame();
        }

        private async void DoGame()
        {
            while (_possibleMovementController.PossibleMovements.Count > 0)
            {
                Debug.Log($"{_possibleMovementController.PossibleMovements.Count} possible moves");

                Movement possibleMovement = _possibleMovementController.PossibleMovements[0];

                _gridPresenter.GetCellView(possibleMovement.StartCell).ColorHighlighter.Highlight(Color.magenta);
                _playerMovementController.SelectCell(possibleMovement.StartCell);
                await Task.Delay(300);

                foreach (Path path in possibleMovement.Paths)
                {
                    foreach (PathNode pathNode in path.PathNodes)
                    {
                        _gridPresenter.GetCellView(pathNode.Cell)
                            .ColorHighlighter.Highlight(GetMeepleColor(pathNode.MeepleLeft));

                        await Task.Delay(300);
                    }

                    await Task.Delay(300);

                    foreach (PathNode pathNode in path.PathNodes)
                    {
                        _playerMovementController.Movement(pathNode.Cell, pathNode.MeepleLeft);
                        await Task.Delay(300);
                    }

                    break;
                }

                await Task.Delay(300);

                foreach (Cell cell in _gridController.Cells)
                {
                    _gridPresenter.GetCellView(cell).ColorHighlighter.OffHighlight();
                }
            }

            Debug.Log("_possibleMovementController.PossibleMovements.Count == 0");
        }

        private Color GetMeepleColor(Meeple meeple)
        {
            return _meepleColors[meeple.Type];
        }
    }
}