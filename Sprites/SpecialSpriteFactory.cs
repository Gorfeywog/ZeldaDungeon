using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Sprites
{
    // this stores all sprites that are oversized and in their own files, or otherwise don't belong in a sheet.
    // this includes the walls and the UI sprites.
    public class SpecialSpriteFactory
    {
        private Texture2D wallsSprite;
        private Texture2D hudSprite;
        private Texture2D inventorySprite;
        private Texture2D mapSprite;
        private Texture2D menuSprite;

        private static SpecialSpriteFactory instance = new SpecialSpriteFactory();

        
        public static SpecialSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SpecialSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            wallsSprite = content.Load<Texture2D>("roomwalls");
            hudSprite = content.Load<Texture2D>("HUD_UI");
            inventorySprite = content.Load<Texture2D>("Inventory_UI");
            mapSprite = content.Load<Texture2D>("Map_UI");
            menuSprite = content.Load<Texture2D>("menu_screen");
        }

        public ISprite CreateWalls()
        {
            return new StaticSprite(wallsSprite, new Rectangle(0, 0, SpriteUtil.ROOM_WIDTH, SpriteUtil.ROOM_HEIGHT));
        }

        public ISprite CreateHUD()
        {
            return new StaticSprite(hudSprite, new Rectangle(0, 0, SpriteUtil.HUD_WIDTH, SpriteUtil.HUD_HEIGHT));
        }

        public ISprite CreateInventory()
        {
            return new StaticSprite(inventorySprite, new Rectangle(0, 0, SpriteUtil.INVENTORY_WIDTH, SpriteUtil.INVENTORY_HEIGHT));
        }

        public ISprite CreateMap()
        {
            return new StaticSprite(mapSprite, new Rectangle(0, 0, SpriteUtil.MAP_WIDTH, SpriteUtil.MAP_HEIGHT));
        }

        public ISprite CreateMainMenu()
        {
            return new StaticSprite(menuSprite, new Rectangle(0, 0, SpriteUtil.MENU_WIDTH, SpriteUtil.MENU_HEIGHT));
        }
    }
}
