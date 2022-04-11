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
    class PauseMenu
    {
        private static readonly Point InventorySize = new Point(SpriteUtil.INVENTORY_WIDTH * SpriteUtil.SCALE_FACTOR, SpriteUtil.INVENTORY_HEIGHT * SpriteUtil.SCALE_FACTOR);
        private static readonly Point MapSize = new Point(SpriteUtil.MAP_WIDTH * SpriteUtil.SCALE_FACTOR, SpriteUtil.MAP_HEIGHT * SpriteUtil.SCALE_FACTOR);
        private static readonly Point HUDSize = new Point(SpriteUtil.HUD_WIDTH * SpriteUtil.SCALE_FACTOR, SpriteUtil.HUD_HEIGHT * SpriteUtil.SCALE_FACTOR);
        private ISprite inventorySPrite;
        private ISprite mapSprite;
        private MapManager mapManager;
        private Game1 g;
        public PauseMenu(Game1 g)
        {
            this.g = g;
            inventorySPrite = SpecialSpriteFactory.Instance.CreateInventory();
            mapSprite = SpecialSpriteFactory.Instance.CreateMap();
            mapManager = new MapManager(g);
        }

        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle inventoryRec = new Rectangle(topLeft, InventorySize);
            Point mapPos = topLeft + new Point(0, SpriteUtil.MAP_HEIGHT * SpriteUtil.SCALE_FACTOR);
            Rectangle mapRec = new Rectangle(mapPos, MapSize);
            mapSprite.Draw(spriteBatch, mapRec);
            inventorySPrite.Draw(spriteBatch, inventoryRec);
            mapManager.Draw(spriteBatch, new Point(), mapPos); // TODO - REMOVE EVIL PLACEHOLDER. SPLIT CLASS UP.

        }
        public void Update()
        {
            inventorySPrite.Update();
            mapSprite.Update();
            bool hasMap = g.Player.HasItem(new MapItem());
            bool hasComp = g.Player.HasItem(new CompassItem());
            mapManager.Update(hasMap, hasComp);
        }
    }
}
