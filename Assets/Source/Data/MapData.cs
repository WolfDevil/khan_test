using System.Collections.Generic;

namespace Source.Data
{
    public struct MapData
    {
        public int MapSize;
        public List<TileData> Tiles;

        public int GetTileIndex((int, int) id)
        {
            return id.Item1 * MapSize + id.Item2;
        }

        public TileData GetTileFromID((int, int) id)
        {
            var index = GetTileIndex(id);
            return Tiles[index];
        }
    }
}