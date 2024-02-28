using Source.View;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Map Settings")] public int mapSize;
        public float tileSize;
        public float tileOffset;

        [Header("Tile Settings")] public TileView tilePrefab;
        public Color greenColor;
        public Color yellowColor;
        public Color blueColor;
    }
}