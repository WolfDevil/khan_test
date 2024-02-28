using System;
using System.Collections.Generic;
using Source.Configs;
using Source.Data;
using Source.Enums;
using UnityEngine;
using Zenject;

namespace Source.View
{
    public class MapView : MonoBehaviour
    {
        private GameConfig _gameConfig;
        private List<TileView> _tiles;

        public void Setup(MapData mapData, Action<(int, int)> onTileClicked, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _tiles = new List<TileView>(mapData.MapSize * mapData.MapSize);

            foreach (var tileData in mapData.Tiles)
            {
                var tile = Instantiate(_gameConfig.tilePrefab, transform);

                var color = tileData.Color switch
                {
                    ETileColor.Green => _gameConfig.greenColor,
                    ETileColor.Yellow => _gameConfig.yellowColor,
                    ETileColor.Blue => _gameConfig.blueColor,
                    _ => Color.white
                };

                tile.Setup(color, () => onTileClicked?.Invoke(tileData.ID));
                _tiles.Add(tile);
            }

            UpdateLayout();
        }

        public Vector3 GetTilePosition((int, int) id)
        {
            var tileIndex = id.Item1 * _gameConfig.mapSize + id.Item2;
            return _tiles[tileIndex].transform.position;
        }

        private void UpdateLayout()
        {
            var width = _gameConfig.tileSize * (_gameConfig.mapSize - 1) +
                        _gameConfig.tileOffset * (_gameConfig.mapSize - 1);
            var widthHalf = width / 2;
            var step = width / (_gameConfig.mapSize - 1);

            var x = 0;
            var y = 0;

            foreach (var tile in _tiles)
            {
                tile.transform.position = new Vector3(x * step - widthHalf, y * step * -1 + widthHalf);

                if (y < _gameConfig.mapSize)
                {
                    if (x < _gameConfig.mapSize - 1)
                    {
                        x++;
                    }
                    else
                    {
                        x = 0;
                        y++;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}