using System;
using Source.Configs;
using Source.Data;
using Source.View;
using Zenject;

namespace Source.Controller
{
    public class GameController : IInitializable
    {
        private readonly GameConfig _gameConfig;
        private readonly MapView _mapView;
        private readonly PlayerView _playerView;

        private PlayerData _playerData;
        private MapData _mapData;


        public GameController(GameConfig gameConfig, MapView mapView, PlayerView playerView)
        {
            _gameConfig = gameConfig;
            _mapView = mapView;
            _playerView = playerView;
        }


        public void Initialize()
        {
            _mapData = MapDataGenerator.Generate(_gameConfig.mapSize, 0.1f);
            _mapView.Setup(_mapData, OnTileClicked, _gameConfig);

            _playerData = new PlayerData();
            _playerData.currentTile = (0, 0);
            _playerData.energyLeft = _gameConfig.startingEnergy;

            var pos = _mapView.GetTilePosition(_playerData.currentTile);
            pos.z = -1;
            _playerView.MoveTo(pos);
        }


        private void OnTileClicked((int, int) id)
        {
            var pos = _mapView.GetTilePosition(id);
            pos.z = -1;
            var dirCheck = _mapData.CheckDirection(_playerData.currentTile, id);
            if (!dirCheck) return;

            var cost = GetCost(_playerData.currentTile, id);
            if (cost < 0 || _playerData.energyLeft < cost) return;

            _playerData.currentTile = id;
            _playerData.energyLeft -= cost;
            _playerView.MoveTo(pos);
        }

        private int GetCost((int, int) playerTile, (int, int) clickedTile)
        {
            var cost = 0;
            if (playerTile.Item1 == clickedTile.Item1)
            {
                var diff = clickedTile.Item2 - playerTile.Item2;
                var dir = diff < 0 ? -1 : 1;
                for (var i = 1; i <= MathF.Abs(diff); i++) Add((playerTile.Item1, playerTile.Item2 + i * dir));
            }
            else if (playerTile.Item2 == clickedTile.Item2)
            {
                var diff = clickedTile.Item1 - playerTile.Item1;
                var dir = diff < 0 ? -1 : 1;
                for (var i = 1; i <= MathF.Abs(diff); i++) Add((playerTile.Item1 + i * dir, playerTile.Item2));
            }

            return cost;

            void Add((int, int) currentTile)
            {
                if (cost < 0) return;

                var price = _gameConfig.GetTilePrice(_mapData.GetTileFromID(currentTile).Color);
                if (price < 0) cost = -1;
                else cost += price;
            }
        }
    }
}