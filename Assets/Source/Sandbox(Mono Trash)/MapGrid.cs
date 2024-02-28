using System;
using Source.Configs;
using UnityEngine;
using Zenject;

public class MapGrid : MonoBehaviour
{
    [Inject] GameConfig _gameConfig;

    public void UpdateLayout()
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

            if (y < _gameConfig.mapSize - 1)
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
                Debug.Log("Too many tiles in map grid");
                return;
            }
        }
    }
}