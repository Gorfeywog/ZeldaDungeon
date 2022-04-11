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
        private ISprite InventorySprite;
        private ISprite MapSprite;
        private ISprite HUDSprite;
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
            HUDSprite = SpecialSpriteFactory.Instance.CreateHUD();
            mapManager = new MapManager(g);
            healthManager = new HealthManager(g);
            rupeeManager = new RupeeManager(g);
            keyManager = new KeyManager(g);
            bombManager = new BombManager(g);
            itemManager = new ItemMenuManager(g);
        }

        public void Draw(SpriteBatch spriteBatch, Point HUDPos, Point MapPos, Point InventoryPos)
        {
            Rectangle inventoryRec = new Rectangle(InventoryPos, InventorySize);
            Rectangle mapRec = new Rectangle(MapPos, MapSize);
            Rectangle destRectangle = new Rectangle(HUDPos, HUDSize);
            HUDSprite.Draw(spriteBatch, destRectangle);
            MapSprite.Draw(spriteBatch, mapRec);
            InventorySprite.Draw(spriteBatch, inventoryRec);
            mapManager.Draw(spriteBatch, HUDPos, MapPos);
            healthManager.Draw(spriteBatch, HUDPos);
            rupeeManager.Draw(spriteBatch, HUDPos);
            keyManager.Draw(spriteBatch, HUDPos);
            bombManager.Draw(spriteBatch, HUDPos);
            itemManager.Draw(spriteBatch, InventoryPos);
        }
        public void Update()
        {
            InventorySprite.Update();
            MapSprite.Update();
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
