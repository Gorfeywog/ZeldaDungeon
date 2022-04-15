using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.UI.Managers;

namespace ZeldaDungeon.UI
{
    public class GameOverScreen
    {
        private SpriteFont font;
        private const int OFFSET = 10;
        public GameOverScreen(SpriteFont font)
        {
            this.font = font;
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            int scaledRoomHeight = SpriteUtil.SCALE_FACTOR * SpriteUtil.ROOM_HEIGHT;
            int scaledRoomWidth = SpriteUtil.SCALE_FACTOR * SpriteUtil.ROOM_WIDTH;
            Vector2 topLeftVec = topLeft.ToVector2();
            Vector2 center = topLeftVec + new Vector2(scaledRoomWidth / 2.0f, scaledRoomHeight / 2.0f);
            Vector2 centerOffset = new Vector2(OFFSET * SpriteUtil.SCALE_FACTOR, 0);
            center = center - centerOffset;
            spriteBatch.DrawString(font, "YOU LOSE.\nPRESS R TO RESTART.", center, Color.Red);
        }
    }
}
