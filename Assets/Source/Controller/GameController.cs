using Source.Configs;
using Source.Data;
using Source.View;
using UnityEngine;
using Zenject;

namespace Source.Controller
{
    public class GameController : MonoBehaviour
    {
        [Inject] private GameConfig _gameConfig;

        private PlayerData _playerData;
        private MapData _mapData;

        private MapView _mapView;

        private void Start()
        {
            _playerData = new PlayerData();
            _playerData.currentTile = (0, 0);
            _playerData.energyLeft = _gameConfig.startingEnergy;

            _mapData = MapDataGenerator.Generate(_gameConfig.mapSize, 0.1f);
            _mapView = Instantiate(_gameConfig.mapViewPrefab);
            _mapView.Setup(_mapData, OnTileClicked, _gameConfig);
        }

        private void OnTileClicked((int, int) id)
        {
            Debug.Log($"Clicked {id}, pos: {_mapView.GetTilePosition(id)}");
        }
    }
}