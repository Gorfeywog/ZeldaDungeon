using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities
{
    public class MainMenu : IEntity
    {
        private ISprite sprite;
        private SpriteFont menuFont;
        private Vector2 textPos1;

        private const int OFFSETX = 25;
        private const int OFFSETY = 40;

        public Direction Dir { get; private set; }
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get => false; }
        public void DespawnEffect() { }
        public DrawLayer Layer { get => DrawLayer.MainMenu; }
        public MainMenu(Point position, SpriteFont font)
        {
            CurrentLoc = new Rectangle(position, new Point(SpriteUtil.MENU_WIDTH * SpriteUtil.SCALE_FACTOR,
                SpriteUtil.MENU_HEIGHT * SpriteUtil.SCALE_FACTOR));
            this.sprite = SpecialSpriteFactory.Instance.CreateMainMenu();
            menuFont = font;

            int scaledHeight = SpriteUtil.SCALE_FACTOR * SpriteUtil.MENU_HEIGHT;
            int scaledWidth = SpriteUtil.SCALE_FACTOR * SpriteUtil.MENU_HEIGHT;
            Vector2 topLeftVec = position.ToVector2();
            textPos1 = topLeftVec + new Vector2(scaledWidth / 2.0f, scaledHeight / 2.0f) ;
            Vector2 offsetY = new Vector2(0, OFFSETY * SpriteUtil.SCALE_FACTOR);
            Vector2 offsetX = new Vector2(OFFSETX * SpriteUtil.SCALE_FACTOR, 0);
            textPos1 = textPos1 + offsetY - offsetX;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
            spriteBatch.DrawString(menuFont, "  PRESS 1 TO ENTER DUNGEON\nPRESS 2 TO ENTER POWER TOWER", textPos1, Color.Black);
            // TODO: Need to find a way to draw the text on the same layer as the menu sprite.
        }
        public void Update() => sprite.Update();        
    }
}
