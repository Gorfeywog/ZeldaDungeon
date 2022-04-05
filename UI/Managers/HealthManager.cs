using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    class HealthManager
    {
        public static readonly int HEART_GRID_LENGTH = 8;
        public static readonly int HUD_HEART_OFFSET_X = 176;
        public static readonly int HUD_HEART_OFFSET_Y = 32;
        private Game1 g;
        private HUDHealth hudHealth;
        public HealthManager(Game1 g)
        {
            this.g = g;
            hudHealth = new HUDHealth();
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos)
        {
            Point hudHeartTopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_HEART_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_HEART_OFFSET_Y);
            hudHealth.Draw(spriteBatch, hudHeartTopLeft);
        }

        public void Update()
        {
            int hp = g.Player.CurrentHealth;
            int maxHp = g.Player.MaxHealth;
            int full = hp / 2;
            int half = hp % 2;
            int empty = (maxHp / 2) - (full + half);
            hudHealth.Update(full, half, empty);
        }
    }
}
