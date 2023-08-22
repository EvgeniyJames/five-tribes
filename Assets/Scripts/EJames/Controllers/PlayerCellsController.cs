#region

using System;
using System.Collections.Generic;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Controllers
{
    public class PlayerCellsController
    {
        private List<PlayerCellsItem> _playerCellMap = new List<PlayerCellsItem>();

        public event Action PlayerCellChanged;


        public void SetPlayerCell(Player player, Cell cell)
        {
            PlayerCellsItem item;
            int itemIndex = _playerCellMap.FindIndex(i => i.Player.Equals(player));
            if (itemIndex > -1)
            {
                item = _playerCellMap[itemIndex];
            }
            else
            {
                item = new PlayerCellsItem(player);
                _playerCellMap.Add(item);
            }

            if (!item.Contains(cell))
            {
                item.Add(cell);
                PlayerCellChanged?.Invoke();

                Debug.Log($"Cell {cell} belong to Player {player.Id}");
            }
        }


        private class PlayerCellsItem
        {
            private Player _player;
            private List<Cell> _cells = new List<Cell>();


            public PlayerCellsItem(Player player)
            {
                _player = player;
            }


            public Player Player => _player;


            public bool Contains(Cell cell)
            {
                return _cells.Contains(cell);
            }


            public void Add(Cell cell)
            {
                if (!Contains(cell))
                {
                    _cells.Add(cell);
                }
            }
        }
    }
}