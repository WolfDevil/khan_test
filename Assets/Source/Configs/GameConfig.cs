using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int mapSize;
        public float tileSize;
        public float tileOffset;
    }
}