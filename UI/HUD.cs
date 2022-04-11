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
    public class HUD
    {
        private static readonly Point HUDSize = new Point(SpriteUtil.HUD_WIDTH * SpriteUtil.SCALE_FACTOR, SpriteUtil.HUD_HEIGHT * SpriteUtil.SCALE_FACTOR);
        private ISprite HUDSprite;
        private MapManager mapManager;
        private HealthManager healthManager;
        private RupeeManager rupeeManager;
        private KeyManager keyManager;
        private BombManager bombManager;
        private ItemManager itemManager;
        private Game1 g;
        public HUD(Game1 g)
        {
            this.g = g;
            HUDSprite = SpecialSpriteFactory.Instance.CreateHUD();
            mapManager = new MapManager(g);
            healthManager = new HealthManager(g);
            rupeeManager = new RupeeManager(g);
            keyManager = new KeyManager(g);
            bombManager = new BombManager(g);
            itemManager = new ItemManager(g);
        }

        public void Draw(SpriteBatch spriteBatch, Point HUDPos)
        {
            Rectangle destRectangle = new Rectangle(HUDPos, HUDSize);
            HUDSprite.Draw(spriteBatch, destRectangle);
            mapManager.Draw(spriteBatch, HUDPos, new Point());
            healthManager.Draw(spriteBatch, HUDPos);
            rupeeManager.Draw(spriteBatch, HUDPos);
            keyManager.Draw(spriteBatch, HUDPos);
            bombManager.Draw(spriteBatch, HUDPos);
            itemManager.Draw(spriteBatch, HUDPos, new Point());
            
        }

        public void Update()
        {
            HUDSprite.Update();
            bool hasMap = g.Player.HasItem(new MapItem());
            bool hasComp = g.Player.HasItem(new CompassItem());
            mapManager.Update(hasMap, hasComp);
            healthManager.Update();
            rupeeManager.Update();
            keyManager.Update();
            bombManager.Update();
            itemManager.Update();
        }
    }
}
