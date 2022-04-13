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
        private HealthManager healthManager;
        private RupeeManager rupeeManager;
        private KeyManager keyManager;
        private BombManager bombManager;
        private ItemMenuManager itemManager;
        private Game1 g;
        public PauseMenu(Game1 g)
        {
            this.g = g;
            InventorySprite = SpecialSpriteFactory.Instance.CreateInventory();
            MapSprite = SpecialSpriteFactory.Instance.CreateMap();
            mapManager = new MapManager(g);
            healthManager = new HealthManager(g);
            rupeeManager = new RupeeManager(g);
            keyManager = new KeyManager(g);
            bombManager = new BombManager(g);
            itemManager = new ItemMenuManager(g);
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
            InventorySprite.Update();
            MapSprite.Update();
            bool hasMap = g.Player.HasItem(new MapItem());
            bool hasComp = g.Player.HasItem(new CompassItem());
            mapManager.Update(hasMap, hasComp);
        }
    }
}
