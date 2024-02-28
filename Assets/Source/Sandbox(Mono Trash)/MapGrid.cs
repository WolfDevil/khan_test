using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private float tileSize;
    [SerializeField] private float offset;

    private void FixedUpdate()
    {
        UpdateLayout();
    }

    private void UpdateLayout()
    {
        int x = 0;
        int y = 0;

        var width = tileSize * (mapSize-1) + offset * (mapSize - 1);
        var widthHalf = width / 2;
        var step = width / (mapSize-1);


        foreach (Transform child in transform)
        {
            child.position = new Vector3(x * step - widthHalf, y * step * -1 + widthHalf);

            if (y < mapSize - 1)
            {
                if (x < mapSize - 1)
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
                break;
            }
        }
    }
}