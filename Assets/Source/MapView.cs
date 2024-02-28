using System;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private MapGrid mapGrid;

    private void Start()
    {
        mapGrid.UpdateLayout();
    }
}