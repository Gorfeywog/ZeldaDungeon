using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.ItemSprites;

namespace ZeldaDungeon.Sprites
{
    public class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;
        private static readonly int gridX = 32; // how wide each sprite is
        private static readonly int gridY = 32;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        
        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            itemSpriteSheet = content.Load<Texture2D>("itemsprites");
        }

        public ISprite CreateArrow()
        {
            return new StaticItemSprite(itemSpriteSheet, 5, 16, GridToPoint(0, 2));
        }

        public ISprite CreateBomb()
        {
            return new StaticItemSprite(itemSpriteSheet, 8, 14, GridToPoint(1, 2));
        }

        public ISprite CreateBow()
        {
            return new StaticItemSprite(itemSpriteSheet, 8, 16, GridToPoint(3, 2));
        }

        public ISprite CreateClock()
        {
            return new StaticItemSprite(itemSpriteSheet, 11, 16, GridToPoint(4, 2));
        }

        public ISprite CreateCompass()
        {
            return new StaticItemSprite(itemSpriteSheet, 11, 12, GridToPoint(0, 3));
        }

        public ISprite CreateFairy()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new AnimatedItemSprite(itemSpriteSheet, 8, 16, topLefts);
        }

        public ISprite CreateHeartContainer()
        {
            return new StaticItemSprite(itemSpriteSheet, 13, 13, GridToPoint(1, 3));
        }

        public ISprite CreateHeart()
        {
            Point[] topLefts = { GridToPoint(2, 0), GridToPoint(3, 0) };
            return new AnimatedItemSprite(itemSpriteSheet, 7, 8, topLefts);
        }

        public ISprite CreateKey()
        {
            return new StaticItemSprite(itemSpriteSheet, 8, 16, GridToPoint(2, 3));
        }

        public ISprite CreateMap()
        {
            return new StaticItemSprite(itemSpriteSheet, 8, 16, GridToPoint(3, 3));
        }

        public ISprite CreateRupy()
        {
            Point[] topLefts = { GridToPoint(0, 1), GridToPoint(1, 1) };
            return new AnimatedItemSprite(itemSpriteSheet, 8, 16, topLefts);
        }

        public ISprite CreateTriforcePiece()
        {
            Point[] topLefts = { GridToPoint(2, 1), GridToPoint(3, 1) };
            return new AnimatedItemSprite(itemSpriteSheet, 10, 10, topLefts);
        }

        public ISprite CreateWoodenBoomerang()
        {
            return new StaticItemSprite(itemSpriteSheet, 5, 8, GridToPoint(1, 2));
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }

        
    }
}
