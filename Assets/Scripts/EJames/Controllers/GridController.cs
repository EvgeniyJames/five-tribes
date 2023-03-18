#region

using System;
using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class GridController : IInitable
    {
        [Inject]
        private ColorsController _colorsController;

        private List<InitData> _initData = new List<InitData>
        {
            new InitData
            {
                Color = System.Drawing.Color.Red,
                Value = 8,
                TileAction = Tile.TileAction.Oasis,
                Amount = 6
            },
            new InitData
            {
                Color = System.Drawing.Color.Red,
                Value = 6,
                TileAction = Tile.TileAction.SmallMarket,
                Amount = 8
            },
            new InitData
            {
                Color = System.Drawing.Color.Red,
                Value = 4,
                TileAction = Tile.TileAction.LargeMarket,
                Amount = 4
            },
            new InitData
            {
                Color = System.Drawing.Color.Blue,
                Value = 6,
                TileAction = Tile.TileAction.SacredPlaces,
                Amount = 4
            },
            new InitData
            {
                Color = System.Drawing.Color.Blue,
                Value = 5,
                TileAction = Tile.TileAction.Village,
                Amount = 5
            },
            new InitData
            {
                Color = System.Drawing.Color.Blue,
                Value = 5,
                TileAction = Tile.TileAction.Village,
                Amount = 5
            },
        };

        private List<Tile> _tiles = new List<Tile>(_cellsAmount);
        private List<Cell> _cells = new List<Cell>(_cellsAmount);

        private const int _cellsAmount = _fieldX * _fieldY;

        private const int _fieldX = 6;
        private const int _fieldY = 5;

        private const int _tilesBlueAmount = 12;
        private const int _tilesRedAmount = 18;

        public event Action GridInitialized;

        public List<Cell> Cells => _cells;

        void IInitable.Init()
        {
            foreach (InitData initData in _initData)
            {
                for (int i = 0; i < initData.Amount; i++)
                {
                    AddTile(initData.Color, initData.Value, initData.TileAction);
                }
            }

            AddTile(System.Drawing.Color.Blue, 15, Tile.TileAction.SacredPlaces);
            AddTile(System.Drawing.Color.Blue, 10, Tile.TileAction.SacredPlaces);
            AddTile(System.Drawing.Color.Blue, 12, Tile.TileAction.SacredPlaces);

            InitCells();
        }

        private void InitCells()
        {
            for (int x = 0; x < _fieldX; x++)
            {
                for (int y = 0; y < _fieldY; y++)
                {
                    _cells.Add(
                        new Cell
                        {
                            Tile = _tiles[_cells.Count],
                            X = x,
                            Y = y
                        });
                }
            }

            GridInitialized?.Invoke();
        }

        private void AddTile(System.Drawing.Color color, int value, Tile.TileAction action)
        {
            _tiles.Add(
                    new Tile
                    {
                        Color = _colorsController.GetColorIndex(color),
                        Value = value,
                        Action = action
                    }
                );
        }

        private class InitData
        {
            public int Amount { get; set; }

            public System.Drawing.Color Color { get; set; }

            public int Value { get; set; }

            public Tile.TileAction TileAction { get; set; }
        }
    }
}