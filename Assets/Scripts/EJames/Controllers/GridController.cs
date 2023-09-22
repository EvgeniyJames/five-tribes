#region

using System;
using System.Collections.Generic;
using System.Drawing;
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

        private List<Tile> _tiles = new List<Tile>(_cellsAmount);
        private List<Cell> _cells = new List<Cell>(_cellsAmount);
        private List<Meeple> _meeples = new List<Meeple>(_meeplesAmount);

        private const int _cellsAmount = _fieldX * _fieldY;

        private const int _fieldX = 6;
        private const int _fieldY = 5;

        private const int _initialMeeplesAmount = 3;
        private const int _meeplesAmount = 90;

        private const int _tilesBlueAmount = 12;
        private const int _tilesRedAmount = 18;

        private List<Cell> _tempList = new List<Cell>();

        public event Action GridInitialized;

        public List<Cell> Cells => _cells;

        public int FieldX => _fieldX - 1;

        public int FieldY => _fieldY - 1;

        public Cell GetCell(int x, int y)
        {
            Cell cell = null;
            int cellIndex = Cells.FindIndex(c => c.X.Equals(x) && c.Y.Equals(y));
            if (cellIndex > -1)
            {
                cell = Cells[cellIndex];
            }

            return cell;
        }

        public List<Cell> GetNeighboursWithout(Cell cell, Cell filter)
        {
            List<Cell> neighbours = GetNeighbours(cell);
            neighbours.RemoveAll(c => c.Equals(filter));
            return neighbours;
        }

        public List<Cell> GetNeighbours(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>
            {
                GetCell(cell.X - 1, cell.Y),
                GetCell(cell.X + 1, cell.Y),
                GetCell(cell.X, cell.Y + 1),
                GetCell(cell.X, cell.Y - 1)
            };

            neighbours.RemoveAll(c => c == null);

            return neighbours;
        }

        public List<Cell> Get9Neighbours(Cell cell)
        {
            _tempList.Clear();

            _tempList.Add(GetCell(cell.X - 1, cell.Y - 1));
            _tempList.Add(GetCell(cell.X, cell.Y - 1));
            _tempList.Add(GetCell(cell.X + 1, cell.Y - 1));

            _tempList.Add(GetCell(cell.X - 1, cell.Y));
            _tempList.Add(GetCell(cell.X + 1, cell.Y));

            _tempList.Add(GetCell(cell.X - 1, cell.Y + 1));
            _tempList.Add(GetCell(cell.X, cell.Y + 1));
            _tempList.Add(GetCell(cell.X + 1, cell.Y + 1));

            _tempList.Add(cell);

            _tempList.RemoveAll(c => c == null);

            return _tempList;
        }

        void IInitable.Init()
        {
            InitTiles();
            InitCells();
            InitMeeples();

            GridInitialized?.Invoke();
        }

        private void InitMeeples()
        {
            foreach (MeeplesInitData meeplesInitData in _meeplesInitData)
            {
                for (int i = 0; i < meeplesInitData.Amount; i++)
                {
                    _meeples.Add(new Meeple(meeplesInitData.Type));
                }
            }

            _meeples.Shuffle();

            foreach (Cell cell in _cells)
            {
                for (int i = 0; i < _initialMeeplesAmount; i++)
                {
                    cell.AddMeeple(_meeples[0]);
                    _meeples.RemoveAt(0);
                }
            }
        }

        private void InitTiles()
        {
            foreach (TilesInitData initData in _tilesInitData)
            {
                for (int i = 0; i < initData.Amount; i++)
                {
                    AddTile(initData.Color, initData.Value, initData.TileAction);
                }
            }

            _tiles.Shuffle();
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
        }

        private void AddTile(Color color, int value, Tile.TileAction action)
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

        private class TilesInitData
        {
            public TilesInitData(int amount, Color color, int value, Tile.TileAction tileAction)
            {
                Amount = amount;
                Color = color;
                Value = value;
                TileAction = tileAction;
            }

            public int Amount { get; }

            public Color Color { get; }

            public int Value { get; }

            public Tile.TileAction TileAction { get; }
        }

        private class MeeplesInitData
        {
            public MeeplesInitData(int amount, Meeple.MeepleType type)
            {
                Amount = amount;
                Type = type;
            }

            public int Amount { get; }

            public Meeple.MeepleType Type { get; }
        }

        #region InitData

        private List<TilesInitData> _tilesInitData = new List<TilesInitData>
        {
            new TilesInitData(color: Color.Red, value: 8, tileAction: Tile.TileAction.Oasis, amount: 6),
            new TilesInitData(color: Color.Red, value: 6, tileAction: Tile.TileAction.SmallMarket, amount: 8),
            new TilesInitData(color: Color.Red, value: 4, tileAction: Tile.TileAction.LargeMarket, amount: 4),
            new TilesInitData(color: Color.Blue, value: 6, tileAction: Tile.TileAction.SacredPlaces, amount: 4),
            new TilesInitData(color: Color.Blue, value: 5, tileAction: Tile.TileAction.Village, amount: 5),
            new TilesInitData(color: Color.Blue, value: 15, tileAction: Tile.TileAction.SacredPlaces, amount: 1),
            new TilesInitData(color: Color.Blue, value: 5, tileAction: Tile.TileAction.Village, amount: 5),
            new TilesInitData(color: Color.Blue, value: 15, tileAction: Tile.TileAction.SacredPlaces, amount: 1),
            new TilesInitData(color: Color.Blue, value: 12, tileAction: Tile.TileAction.SacredPlaces, amount: 1),
            new TilesInitData(color: Color.Blue, value: 10, tileAction: Tile.TileAction.SacredPlaces, amount: 1),
        };

        private List<MeeplesInitData> _meeplesInitData = new List<MeeplesInitData>
        {
            new MeeplesInitData(16, Meeple.MeepleType.Viziers),
            new MeeplesInitData(20, Meeple.MeepleType.Elders),
            new MeeplesInitData(18, Meeple.MeepleType.Assassins),
            new MeeplesInitData(18, Meeple.MeepleType.Builders),
            new MeeplesInitData(18, Meeple.MeepleType.Merchants),
        };

        #endregion
    }
}