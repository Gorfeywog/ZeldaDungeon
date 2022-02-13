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
            return new ArrowItemSprite(itemSpriteSheet, GridToPoint(0, 2));
        }

        public ISprite CreatBomb()
        {
            return new BombItemSprite(itemSpriteSheet, GridToPoint(1, 2));
        }

        public ISprite CreateBow()
        {
            return new BowItemSprite(itemSpriteSheet, GridToPoint(3, 2));
        }

        public ISprite CreateClock()
        {
            return new ClockItemSprite(itemSpriteSheet, GridToPoint(4, 2));
        }

        public ISprite CreateCompass()
        {
            return new CompassItemSprite(itemSpriteSheet, GridToPoint(0, 3));
        }

        public ISprite CreateFairy()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new FairyItemSprite(itemSpriteSheet, topLefts);
        }

        public ISprite CreateHeartContainer()
        {
            return new HeartContainerItemSprite(itemSpriteSheet, GridToPoint(1, 3));
        }

        public ISprite CreateHeart()
        {
            Point[] topLefts = { GridToPoint(2, 0), GridToPoint(3, 0) };
            return new HeartItemSprite(itemSpriteSheet, topLefts);
        }

        public ISprite CreateKey()
        {
            return new KeyItemSprite(itemSpriteSheet, GridToPoint(2, 3));
        }

        public ISprite CreateMap()
        {
            return new MapItemSprite(itemSpriteSheet, GridToPoint(3, 3));
        }

        public ISprite CreateRupy()
        {
            Point[] topLefts = { GridToPoint(0, 1), GridToPoint(1, 1) };
            return new RupyItemSprite(itemSpriteSheet, topLefts);
        }

        public ISprite CreateTriforcePiece()
        {
            Point[] topLefts = { GridToPoint(2, 1), GridToPoint(3, 1) };
            return new TriforcePieceItemSprite(itemSpriteSheet, topLefts);
        }

        public ISprite CreateWoodenBoomerang()
        {
            return new WoodenBoomerangItemSprite(itemSpriteSheet, GridToPoint(1, 2));
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }

        
    }
}
