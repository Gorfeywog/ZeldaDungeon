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
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 2, 
               (int) SpriteUtil.SpriteSize.BombWidth, (int)SpriteUtil.SpriteSize.BombLength));
        }

        public ISprite CreateBow()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 2, 
               (int) SpriteUtil.SpriteSize.BowWidth, (int) SpriteUtil.SpriteSize.BowLength));
        }

        public ISprite CreateClock()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 2, 
               (int) SpriteUtil.SpriteSize.ClockWidth , (int)SpriteUtil.SpriteSize.ClockLength ));
        }

        public ISprite CreateCompass()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 3, 
                (int)SpriteUtil.SpriteSize.CompassWidth, (int)SpriteUtil.SpriteSize.CompassLength ));
        }

        public ISprite CreateFairy()
        {
            int width = (int)SpriteUtil.SpriteSize.FairyWidth;
            int length = (int)SpriteUtil.SpriteSize.FairyLength;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, width, length), 
                SpriteUtil.GridToRectangle(1, 0, width, length) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateHeartContainer()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 3, 
                (int)SpriteUtil.SpriteSize.HeartContainerWidth, (int)SpriteUtil.SpriteSize.HeartContainerLength));
        }

        public ISprite CreateHeart()
        {
            int width = (int)SpriteUtil.SpriteSize.HeartWidth;
            int length = (int)SpriteUtil.SpriteSize.HeartLength;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 0, width, length), 
                SpriteUtil.GridToRectangle(3, 0, width, length) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateKey()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 3, 
                (int)SpriteUtil.SpriteSize.KeyWidth, (int)SpriteUtil.SpriteSize.KeyLength));
        }

        public ISprite CreateMap()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 3, 
                (int)SpriteUtil.SpriteSize.MapWidth, (int)SpriteUtil.SpriteSize.MapLength));
        }

        public ISprite CreateRupee()
        {
            int width = (int)SpriteUtil.SpriteSize.RupeeWidth;
            int length = (int)SpriteUtil.SpriteSize.RupeeLength;

            Rectangle sourceRectangle = SpriteUtil.GridToRectangle(0, 1, width, length);
            return new StaticSprite(itemSpriteSheet, sourceRectangle);
        }
        public ISprite Create5Rupees()
        {
            int width = (int)SpriteUtil.SpriteSize.RupeeWidth;
            int length = (int)SpriteUtil.SpriteSize.RupeeLength;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 1, width, length), SpriteUtil.GridToRectangle(1, 1, width, length) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateTriforcePiece()
        {
            int width = (int)SpriteUtil.SpriteSize.TriforceWidth;
            int length = (int)SpriteUtil.SpriteSize.TriforceLength;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 1, width, length), SpriteUtil.GridToRectangle(3, 1, width, length) };
            return new AnimatedSprite(itemSpriteSheet, sourceRectangles);
        }

        public ISprite CreateWoodenBoomerang()
        {
            return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 2, 
                (int)SpriteUtil.SpriteSize.BoomerangX, (int)SpriteUtil.SpriteSize.BoomerangY));
        }
        public ISprite CreateArrow(Direction dir, bool isMagic)
        {
            int width = (int)SpriteUtil.SpriteSize.ArrowWidth;
            int length = (int)SpriteUtil.SpriteSize.ArrowLength;

            if (isMagic)
            {
                return dir switch
                {
                    Direction.Down => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 6, width, length)),
                    Direction.Left => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 6, length, width)),
                    Direction.Right => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 6, length, width)),
                    Direction.Up => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 6, width, length)),
                    _ => throw new ArgumentException()
                };
            }
            else
            {
                return dir switch
                {
                    Direction.Down => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 5, width, length)),
                    Direction.Left => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 5, length, width)),
                    Direction.Right => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 5, length, width)),
                    Direction.Up => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 5, width, length)),
                    _ => throw new ArgumentException()
                };
            }
        }
        public ISprite CreateSword(Direction dir) // TODO - should have a version that cycles colors
        {
            int width = (int)SpriteUtil.SpriteSize.SwordWidth;
            int length = (int)SpriteUtil.SpriteSize.SwordLength;

            return dir switch
            {
                Direction.Down => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 4, width, length)),
                Direction.Left => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 4, length, width)),
                Direction.Right => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(2, 4, length, width)),
                Direction.Up => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(3, 4, width, length)),
                _ => throw new ArgumentException()
            };
        }
        public ISprite CreateSwordProj(Direction dir) // TODO - should cycle colors
        {
            int width = (int)SpriteUtil.SpriteSize.SwordProjWidth;
            int height = (int)SpriteUtil.SpriteSize.SwordProjHeight;

            return dir switch
            {
                Direction.NE => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(4, 4, width, height)),
                Direction.NW => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(5, 4, height, width)),
                Direction.SE => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(6, 4, height, width)),
                Direction.SW => new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(7, 4, width, height)),
                _ => throw new ArgumentException()
            };
        }
        public ISprite CreateCandle(bool isRed)
        {
            int width = (int)SpriteUtil.SpriteSize.CandleWidth;
            int length = (int)SpriteUtil.SpriteSize.CandleLength;
            if (isRed)
            {
                return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(0, 7, width, length));
            }
            else
            {
                return new StaticSprite(itemSpriteSheet, SpriteUtil.GridToRectangle(1, 7, width, length));
            }
        }
        
    }
}
