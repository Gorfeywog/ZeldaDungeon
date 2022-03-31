using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    public class MapManager
    {
        public static readonly int MAP_GRID_LENGTH = 8;
        private Point topLeftOffsetHud;
        private Point topLeftOffsetPause;
        private Game1 g;
        private MapRoomState[,] mapGrid;
        public MapManager(Game1 g, Point topleftOffsetHud, Point topleftOffsetPause)
        {
            this.g = g;
            this.topLeftOffsetHud = topleftOffsetHud;
            this.topLeftOffsetPause = topleftOffsetPause;
            mapGrid = new MapRoomState[MAP_GRID_LENGTH, MAP_GRID_LENGTH];
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos, Point pausePos)
        {
            
        }

        public void UpdateGrid(bool hasMap, bool hasCompass)
        {
            for (int i = 0; i < MAP_GRID_LENGTH; i++)
            {
                for (int j = 0; j < MAP_GRID_LENGTH; j++)
                {
                    int index = g.GridToRoomIndex(i, j);
                    if (index == -1 || g.Rooms[index].Type == RoomType.Ladder)
                    {
                        mapGrid[i, j] = new MapRoomState(false, false, false);
                    }
                    else
                    {
                        bool current = index == g.CurrentRoomIndex;
                        bool hasTri = g.Rooms[index].HasTriforce && hasCompass;
                        mapGrid[i, j] = new MapRoomState(current, hasTri, hasMap);
                    }
                }
            }
        }
        
    }
}
