using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Sprites
{
    public class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;

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

        public ISprite CreateBomb()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 2, 8, 14));
        }

        public ISprite CreateBow()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 2, 8, 16));
        }

        public ISprite CreateClock()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 2, 11, 16));
        }

        public ISprite CreateCompass()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 3, 11, 12));
        }

        public ISprite CreateFairy()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, 8, 16), SpriteUtil.GridToRectangle(1, 0, 8, 16) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateHeartContainer()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 3, 13, 13));
        }

        public ISprite CreateHeart()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 0, 7, 8), SpriteUtil.GridToRectangle(3, 0, 7, 8) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateKey()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 3, 8, 16));
        }

        public ISprite CreateMap()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 3, 8, 16));
        }

        public ISprite CreateRupy()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 1, 8, 16), SpriteUtil.GridToRectangle(1, 1, 8, 16) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateTriforcePiece()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 1, 10, 10), SpriteUtil.GridToRectangle(3, 1, 10, 10) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateWoodenBoomerang()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 2, 5, 8));
        }
        public ISprite CreateArrow(Direction dir)
        {
            return dir switch
            {
                Direction.Down => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 5, 5, 16)),
                Direction.Left => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 5, 16, 5)),
                Direction.Right => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 5, 16, 5)),
                Direction.Up => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 5, 5, 16)),
                _ => throw new ArgumentException()
            };
        }
        public ISprite CreateMagicArrow(Direction dir)
        {
            return dir switch
            {
                Direction.Down => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 6, 5, 16)),
                Direction.Left => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 6, 16, 5)),
                Direction.Right => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 6, 16, 5)),
                Direction.Up => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 6, 5, 16)),
                _ => throw new ArgumentException()
            };
        }
        public ISprite CreateSword(Direction dir)
        {
            return dir switch
            {
                Direction.Down => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 4, 8, 16)),
                Direction.Left => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 4, 16, 8)),
                Direction.Right => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 4, 16, 8)),
                Direction.Up => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 4, 8, 16)),
                _ => throw new ArgumentException()
            };
        }
        public ISprite CreateCandle(bool isRed)
        {
            if (isRed)
            {
                return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 7, 8, 16));
            }
            else
            {
                return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 7, 8, 16));
            }
        }
        
    }
}
