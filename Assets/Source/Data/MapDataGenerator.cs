using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using Source.Utils;

namespace Source.Data
{
    public static class MapDataGenerator
    {
        public static MapData Generate(int size, float bluePercent)
        {
            MapData data = new MapData();
            var tilesCount = size * size;
            data.MapSize = size;
            data.Tiles = new List<TileData>(tilesCount);

            var colors = new List<ETileColor>(tilesCount);

            int blueCount = (int)(tilesCount * bluePercent);
            colors.AddRange(Enumerable.Repeat(ETileColor.Blue, blueCount).ToList());

            var yellowCount = (int)((tilesCount - blueCount) / 2f);
            colors.AddRange(Enumerable.Repeat(ETileColor.Yellow, yellowCount).ToList());

            var greenCount = tilesCount - blueCount - yellowCount;
            colors.AddRange(Enumerable.Repeat(ETileColor.Green, greenCount).ToList());

            colors.Shuffle();

            var n = 0;
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var tileData = new TileData();
                    tileData.ID = (x, y);
                    tileData.Color = colors[n];
                    data.Tiles.Add(tileData);
                    n++;
                }
            }

            return data;
        }
    }
}