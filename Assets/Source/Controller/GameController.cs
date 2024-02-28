using System;
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
        private PlayerView _playerView;

        private void Start()
        {
            _mapData = MapDataGenerator.Generate(_gameConfig.mapSize, 0.1f);
            _mapView = Instantiate(_gameConfig.mapViewPrefab);
            _mapView.Setup(_mapData, OnTileClicked, _gameConfig);

            _playerData = new PlayerData();
            _playerData.currentTile = (0, 0);
            _playerData.energyLeft = _gameConfig.startingEnergy;

            _playerView = Instantiate(_gameConfig.playerViewPrefab);
            var pos = _mapView.GetTilePosition(_playerData.currentTile);
            pos.z = -1;
            _playerView.MoveTo(pos);
        }

        private void OnTileClicked((int, int) id)
        {
            var pos = _mapView.GetTilePosition(id);
            pos.z = -1;
            var dirCheck = CheckDirection(_playerData.currentTile, id);
            var cost = -1;
            if (dirCheck)
            {
                cost = GetCost(_playerData.currentTile, id);
                if (cost >= 0 && _playerData.energyLeft >= cost)
                {
                    _playerData.currentTile = id;
                    _playerData.energyLeft -= cost;
                    _playerView.MoveTo(pos);
                }
            }

            Debug.Log(
                $"Clicked {id}, pos: {pos}, dirCheck: {dirCheck}, cost: {cost}, energyLeft: {_playerData.energyLeft}");
        }

        private bool CheckDirection((int, int) playerTile, (int, int) clickedTile)
        {
            return !(
                playerTile.Equals(clickedTile) ||
                (playerTile.Item1 != clickedTile.Item1 && playerTile.Item2 != clickedTile.Item2)
            );
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