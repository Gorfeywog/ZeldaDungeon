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
        private ISprite inventorySprite;
        private ISprite mapSprite;
        private PauseMapManager mapManager;
        private ItemMenuManager itemManager;
        private Game1 g;
        public PauseMenu(Game1 g)
        {
            this.g = g;
            inventorySprite = SpecialSpriteFactory.Instance.CreateInventory();
            mapSprite = SpecialSpriteFactory.Instance.CreateMap();
            mapManager = new PauseMapManager(g);
            itemManager = new ItemMenuManager(g);
        }

        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle inventoryRec = new Rectangle(topLeft, InventorySize);
            Point mapPos = topLeft + new Point(0, SpriteUtil.MAP_HEIGHT * SpriteUtil.SCALE_FACTOR);
            Rectangle mapRec = new Rectangle(mapPos, MapSize);
            mapSprite.Draw(spriteBatch, mapRec);
            inventorySprite.Draw(spriteBatch, inventoryRec);
            itemManager.Draw(spriteBatch, topLeft);
            mapManager.Draw(spriteBatch, mapPos); // TODO - REMOVE EVIL PLACEHOLDER. SPLIT CLASS UP.

        }
        public void Update()
        {
            inventorySprite.Update();
            mapSprite.Update();
            bool hasMap = g.Player.HasItem(new MapItem());
            mapManager.Update(hasMap);
        }
    }
}
