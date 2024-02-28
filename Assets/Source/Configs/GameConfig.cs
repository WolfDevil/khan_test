using Source.Enums;
using Source.View;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Map Settings")] public MapView mapViewPrefab;
        public int mapSize;
        public float tileSize;
        public float tileOffset;

        [Header("Tile Settings")] public TileView tilePrefab;
        public Color greenColor;
        public int greenCost;

        public Color yellowColor;
        public int yellowCost;

        public Color blueColor;
        public int blueCost;

        [Header("Player Settings")] public PlayerView playerViewPrefab;
        public int startingEnergy;


        public int GetTilePrice(ETileColor color)
        {
            return color switch
            {
                ETileColor.Green => greenCost,
                ETileColor.Yellow => yellowCost,
                ETileColor.Blue => blueCost,
                _ => 0
            };
        }
    }
}