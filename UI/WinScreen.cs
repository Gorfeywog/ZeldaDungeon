using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    public class WinScreen
    {
        private SpriteFont font;
        private const int OFFSET = 65;
        private bool tower;
        public WinScreen(SpriteFont font, bool beatTower)
        {
            this.font = font;
            this.tower = beatTower;
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            int scaledRoomHeight = SpriteUtil.SCALE_FACTOR * SpriteUtil.ROOM_HEIGHT;
            int scaledRoomWidth = SpriteUtil.SCALE_FACTOR * SpriteUtil.ROOM_WIDTH;
            Vector2 topLeftVec = topLeft.ToVector2();
            Vector2 center = topLeftVec + new Vector2(scaledRoomWidth / 2.0f, scaledRoomHeight / 2.0f);
            Vector2 centerOffset = new Vector2(OFFSET * SpriteUtil.SCALE_FACTOR, 0);
            center = center - centerOffset;
            var winMessage = tower ? "Congratulations! You defeated the power tower!\n\n     PRESS R TO RESTART OR Q TO QUIT"
                : "Congratulations! You recovered the triforce!\n\n     PRESS R TO RESTART OR Q TO QUIT";
            spriteBatch.DrawString(font, winMessage, center, Color.Gold);
        }
    }
}
