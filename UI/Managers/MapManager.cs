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
        public static readonly int HUD_MAP_OFFSET_X = 16;
        public static readonly int HUD_MAP_OFFSET_Y = 8;
        public static readonly int PAUSE_MAP_OFFSET_X = 126;
        public static readonly int PAUSE_MAP_OFFSET_Y = 10;
        private Game1 g;
        private MapRoomState[,] mapGrid;
        private HUDMap hudMap;
        private PauseMap pauseMap;
        public MapManager(Game1 g)
        {
            this.g = g;
            mapGrid = new MapRoomState[MAP_GRID_LENGTH, MAP_GRID_LENGTH];
            hudMap = new HUDMap();
            pauseMap = new PauseMap();
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos, Point pausePos)
        {

            if (g.State == GameState.Normal)
            {
                Point hudMapTopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_MAP_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_MAP_OFFSET_Y);
                hudMap.Draw(spriteBatch, hudMapTopLeft, mapGrid);
            }
            else if (g.State == GameState.PauseMenu)
            {
                Point pauseMapTopLeft = pausePos + new Point(PAUSE_MAP_OFFSET_X * SpriteUtil.SCALE_FACTOR, PAUSE_MAP_OFFSET_Y * SpriteUtil.SCALE_FACTOR);
                pauseMap.Draw(spriteBatch, pauseMapTopLeft, mapGrid);
            }
        }

        public void Update(bool hasMap, bool hasCompass)
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
            hudMap.Update();
        }
        
    }
}
