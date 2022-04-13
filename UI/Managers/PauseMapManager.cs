using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    public class PauseMapManager
    {
        public static readonly int MAP_GRID_LENGTH = 8;
        public static readonly int PAUSE_MAP_OFFSET_X = 126;
        public static readonly int PAUSE_MAP_OFFSET_Y = 10;
        private Game1 g;
        private HUDMapRoomState[,] mapGrid;
        private PauseMap pauseMap;
        public PauseMapManager(Game1 g)
        {
            this.g = g;
            mapGrid = new HUDMapRoomState[MAP_GRID_LENGTH, MAP_GRID_LENGTH];
            pauseMap = new PauseMap();
        }
        public void Draw(SpriteBatch spriteBatch, Point pausePos)
        {
            Point pauseMapTopLeft = pausePos + new Point(PAUSE_MAP_OFFSET_X * SpriteUtil.SCALE_FACTOR, PAUSE_MAP_OFFSET_Y * SpriteUtil.SCALE_FACTOR);
            pauseMap.Draw(spriteBatch, pauseMapTopLeft, mapGrid);
        }

        public void Update(bool hasMap, bool hasCompass)
        {
        }
        
    }
}
