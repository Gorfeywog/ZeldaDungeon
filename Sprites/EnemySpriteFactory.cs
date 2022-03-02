using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpriteSheet;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();
        
        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpriteSheet = content.Load<Texture2D>("enemysprites");
        }

        public ISprite CreateKeeseSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.KeeseX;
            int height = (int)SpriteUtil.SpriteSize.KeeseY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 4, width, height), SpriteUtil.GridToRectangle(1, 4, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateAquamentusSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.AquamentusX;
            int height = (int)SpriteUtil.SpriteSize.AquamentusY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, width, height), SpriteUtil.GridToRectangle(1, 0, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateFireballSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.FireballX;
            int height = (int)SpriteUtil.SpriteSize.FireballY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 11, width, height), SpriteUtil.GridToRectangle(1, 11, width, height),
                SpriteUtil.GridToRectangle(2, 11, width, height), SpriteUtil.GridToRectangle(3, 11, width, height)};
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateGelSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.GelX;
            int height = (int)SpriteUtil.SpriteSize.GelY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 1, width, height), SpriteUtil.GridToRectangle(1, 1, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteLeft()
        {
            int width = (int)SpriteUtil.SpriteSize.RopeX;
            int height = (int)SpriteUtil.SpriteSize.RopeY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 5, width, height), SpriteUtil.GridToRectangle(1, 5, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteRight()
        {
            int width = (int)SpriteUtil.SpriteSize.RopeX;
            int height = (int)SpriteUtil.SpriteSize.RopeY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 5, width, height), SpriteUtil.GridToRectangle(3, 5, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateStalfosSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.StalfosX;
            int height = (int)SpriteUtil.SpriteSize.StalfosY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 6, width, height), SpriteUtil.GridToRectangle(1, 6, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateTrapSprite()
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(0, 7, (int)SpriteUtil.SpriteSize.TrapX, (int)SpriteUtil.SpriteSize.TrapY));
        }

        public ISprite CreateBlueGoriyaSpriteLeft()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(4, 2, width, height), SpriteUtil.GridToRectangle(5, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteRight()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(6, 2, width, height), SpriteUtil.GridToRectangle(7, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteUp()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 2, width, height), SpriteUtil.GridToRectangle(1, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteDown()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 2, width, height), SpriteUtil.GridToRectangle(3, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteLeft()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(4, 3, width, height), SpriteUtil.GridToRectangle(5, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteRight()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(6, 3, width, height), SpriteUtil.GridToRectangle(7, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteUp()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 3, width, height), SpriteUtil.GridToRectangle(1, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteDown()
        {
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 3, width, height), SpriteUtil.GridToRectangle(3, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticBoomerangSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
            int height = (int)SpriteUtil.SpriteSize.BoomerangY;

            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(0, 10, width, height));
        }
        public ISprite CreateBoomerangSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
            int height = (int)SpriteUtil.SpriteSize.BoomerangY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 10, width, height), 
                SpriteUtil.GridToRectangle(1, 10, width, height), SpriteUtil.GridToRectangle(2, 10, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticMagicBoomerangSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
            int height = (int)SpriteUtil.SpriteSize.BoomerangY;

            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(3, 10, width, height));
        }
        public ISprite CreateMagicBoomerangSprite()
        {
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
            int height = (int)SpriteUtil.SpriteSize.BoomerangY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(3, 10, width, height), 
                SpriteUtil.GridToRectangle(4, 10, 5, 8), SpriteUtil.GridToRectangle(5, 10, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateWallMasterSpriteSW()
        {
            int width = (int)SpriteUtil.SpriteSize.WallMasterX;
            int height = (int)SpriteUtil.SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 8, width, height), SpriteUtil.GridToRectangle(1, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteSE()
        {
            int width = (int)SpriteUtil.SpriteSize.WallMasterX;
            int height = (int)SpriteUtil.SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 8, width, height), SpriteUtil.GridToRectangle(3, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNW()
        {
            int width = (int)SpriteUtil.SpriteSize.WallMasterX;
            int height = (int)SpriteUtil.SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(4, 8, width, height), SpriteUtil.GridToRectangle(5, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNE()
        {
            int width = (int)SpriteUtil.SpriteSize.WallMasterX;
            int height = (int)SpriteUtil.SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(6, 8, width, height), SpriteUtil.GridToRectangle(7, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateCloudSprite()
        {
            int width = (int) SpriteUtil.SpriteSize.CloudX;
            int height = (int) SpriteUtil.SpriteSize.CloudY;

            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 9, width, height), SpriteUtil.GridToRectangle(1, 9, width, height), 
                SpriteUtil.GridToRectangle(2, 9, width, height), SpriteUtil.GridToRectangle(3, 9, width, height), 
                SpriteUtil.GridToRectangle(4, 9, width, height), SpriteUtil.GridToRectangle(5, 9, width, height), 
                SpriteUtil.GridToRectangle(6, 9, width, height), SpriteUtil.GridToRectangle(7, 9, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateHitEffectSprite() // should maybe live on a different spritesheet
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(6, 10, 
               (int) SpriteUtil.SpriteSize.HitEffectX, (int) SpriteUtil.SpriteSize.HitEffectY));
        }
    }

}