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
    public class MainMenu
    {
        private SpriteFont font;
        private ISprite mainMenu;

        public MainMenu(SpriteFont font)
        {
            this.font = font;
            mainMenu = SpecialSpriteFactory.Instance.CreateMainMenu();
        }

        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            int menuHeight = SpriteUtil.SCALE_FACTOR * SpriteUtil.MENU_HEIGHT;
            int menuWidth = SpriteUtil.SCALE_FACTOR * SpriteUtil.MENU_WIDTH;
            Point size = new Point(menuWidth, menuHeight);
            Point menuDest = topLeft;
            Rectangle destRect = new Rectangle(menuDest, size);
            mainMenu.Draw(spriteBatch, destRect);
            // TODO: Put in text for power tower and dungeon options
        }


    }
}
