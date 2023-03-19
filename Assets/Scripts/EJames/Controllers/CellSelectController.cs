#region

using System;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Controllers
{
    public class CellSelectController
    {
        public event Action<Cell> Clicked;

        public void CellClicked(Cell cell)
        {
            Debug.Log($"Clicked on {cell.X}, {cell.Y}");
            Clicked?.Invoke(cell);
        }
    }
}