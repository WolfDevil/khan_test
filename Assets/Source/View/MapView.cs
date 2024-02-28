using Source.Configs;
using Source.Data;
using Source.Enums;
using UnityEngine;
using Zenject;

namespace Source.View
{
    public class MapView : MonoBehaviour
    {
        [Inject] private GameConfig _gameConfig;

        private void Start()
        {
            SpawnTiles();
            UpdateLayout();
        }

        private void SpawnTiles()
        {
            var mapData = MapDataGenerator.Generate(_gameConfig.mapSize, 0.1f);

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

                tile.Setup(color, () => OnTileClicked(tileData.ID));
            }
        }

        private void OnTileClicked((int, int) id)
        {
        }

        private void UpdateLayout()
        {
            var width = _gameConfig.tileSize * (_gameConfig.mapSize - 1) +
                        _gameConfig.tileOffset * (_gameConfig.mapSize - 1);
            var widthHalf = width / 2;
            var step = width / (_gameConfig.mapSize - 1);

            var x = 0;
            var y = 0;

            foreach (Transform child in transform)
            {
                child.position = new Vector3(x * step - widthHalf, y * step * -1 + widthHalf);

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