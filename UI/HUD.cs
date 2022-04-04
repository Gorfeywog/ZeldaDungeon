using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    public class HUD
    {
        private static readonly Point HUDSize = new Point(SpriteUtil.HUD_WIDTH * SpriteUtil.SCALE_FACTOR, SpriteUtil.HUD_HEIGHT * SpriteUtil.SCALE_FACTOR);
        private ISprite HUDSprite;
        private MapManager mapManager;
        private Game1 g;
        public HUD(Game1 g)
        {
            this.g = g;
            HUDSprite = SpecialSpriteFactory.Instance.CreateHUD();
            mapManager = new MapManager(g);
        }

        public void Draw(SpriteBatch spriteBatch, Point HUDPos)
        {
            Rectangle destRectangle = new Rectangle(HUDPos, HUDSize);
            HUDSprite.Draw(spriteBatch, destRectangle);
            mapManager.Draw(spriteBatch, HUDPos, new Point());
        }

        public void Update()
        {
            HUDSprite.Update();
            bool hasMap = g.Player.HasItem(new MapItem());
            bool hasComp = g.Player.HasItem(new CompassItem());
            mapManager.Update(hasMap, hasComp);
        }
    }
}
