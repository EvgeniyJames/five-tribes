#region

using System.Collections.Generic;
using System.Drawing;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class ColorsController
    {
        [Inject]
        private PlayersController _playersController;

        private List<Color> _colors = new List<Color>
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Yellow,
            Color.Magenta,
        };

        public int ColorsCount => _colors.Count;

        public Color GetColorByIndex(int index)
        {
            return _colors[index % _colors.Count];
        }

        public int GetColorIndex(Color color)
        {
            return _colors.FindIndex(c => c.Equals(color));
        }

        public int GetNextFreeColorIndex(int currentColor)
        {
            int nextFreeColor = -1;
            for (int i = 0; i < ColorsCount; i++)
            {
                currentColor = (currentColor + 1) % ColorsCount;
                if (IsColorFree(currentColor))
                {
                    nextFreeColor = currentColor;
                    break;
                }
            }

            return nextFreeColor;
        }

        public int GetPreviousFreeColorIndex(int currentColor)
        {
            int nextFreeColor = -1;
            for (int i = 0; i < ColorsCount; i++)
            {
                currentColor--;
                if (currentColor < 0)
                {
                    currentColor = ColorsCount - 1;
                }

                if (IsColorFree(currentColor))
                {
                    nextFreeColor = currentColor;
                    break;
                }
            }

            return nextFreeColor;
        }

        public bool IsColorFree(int index)
        {
            bool isFree = true;
            foreach (Player player in _playersController.Players)
            {
                if (index == player.Color)
                {
                    isFree = false;
                    break;
                }
            }

            return isFree;
        }
    }
}