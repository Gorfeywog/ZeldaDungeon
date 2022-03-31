using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    class HUDMap
    {
        private static readonly int HUD_ROOM_LENGTH = 7;
        private static readonly int HUD_ROOM_HEIGHT = 3;
        public HUDMap() { }
        public void Draw(SpriteBatch spriteBatch, Point gridTopLeft, MapRoomState[,] mapGrid)
        {
            for (int i = 0; i < mapGrid.GetLength(0), i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    int scaledLen = HUD_ROOM_LENGTH * SpriteUtil.SCALE_FACTOR;
                    int scaledHeight = HUD_ROOM_HEIGHT * SpriteUtil.SCALE_FACTOR;
                    Point dest = gridTopLeft + new Point(i * scaledLen, j * scaledHeight);
                    Point size = new Point(scaledLen, scaledHeight);
                    Rectangle destRect = new Rectangle(dest, size);
                    // choose a sprite to draw based off mapGrid[i, j]
                }
            }
        }
    }
}
