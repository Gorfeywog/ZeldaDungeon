using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using static ZeldaDungeon.Sprites.SpriteUtil;

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
            int width = (int)SpriteSize.KeeseX;
            int height = (int)SpriteSize.KeeseY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 4, width, height), GridToRectangle(1, 4, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateAquamentusSprite()
        {
            int width = (int)SpriteSize.AquamentusX;
            int height = (int)SpriteSize.AquamentusY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 0, width, height), GridToRectangle(1, 0, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateFireballSprite()
        {
            int width = (int)SpriteSize.FireballX;
            int height = (int)SpriteSize.FireballY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 11, width, height), GridToRectangle(1, 11, width, height),
                GridToRectangle(2, 11, width, height), GridToRectangle(3, 11, width, height)};
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateGelSprite()
        {
            int width = (int)SpriteSize.GelX;
            int height = (int)SpriteSize.GelY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 1, width, height), GridToRectangle(1, 1, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteLeft()
        {
            int width = (int)SpriteSize.RopeX;
            int height = (int)SpriteSize.RopeY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 5, width, height), GridToRectangle(1, 5, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteRight()
        {
            int width = (int)SpriteSize.RopeX;
            int height = (int)SpriteSize.RopeY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 5, width, height), GridToRectangle(3, 5, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateStalfosSprite()
        {
            int width = (int)SpriteSize.StalfosX;
            int height = (int)SpriteSize.StalfosY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 6, width, height), GridToRectangle(1, 6, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateTrapSprite()
        {
            return new StaticSprite(enemySpriteSheet, GridToRectangle(0, 7, (int)SpriteSize.TrapX, (int)SpriteSize.TrapY));
        }

        public ISprite CreateOldManSprite()
        {
            return new StaticSprite(enemySpriteSheet, GridToRectangle(1, 7, 16, 16));
        }

        public ISprite CreateBlueGoriyaSpriteLeft()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(4, 2, width, height), GridToRectangle(5, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteRight()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(6, 2, width, height), GridToRectangle(7, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteUp()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 2, width, height), GridToRectangle(1, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteDown()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 2, width, height), GridToRectangle(3, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteLeft()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(4, 3, width, height), GridToRectangle(5, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteRight()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(6, 3, width, height), GridToRectangle(7, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteUp()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 3, width, height), GridToRectangle(1, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteDown()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 3, width, height), GridToRectangle(3, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            return new StaticSprite(enemySpriteSheet, GridToRectangle(0, 10, width, height));
        }
        public ISprite CreateBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 10, width, height), 
                GridToRectangle(1, 10, width, height), GridToRectangle(2, 10, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticMagicBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            return new StaticSprite(enemySpriteSheet, GridToRectangle(3, 10, width, height));
        }
        public ISprite CreateMagicBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            Rectangle[] sourceRectangles = { GridToRectangle(3, 10, width, height), 
                GridToRectangle(4, 10, 5, 8), GridToRectangle(5, 10, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateWallMasterSpriteSW()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 8, width, height), GridToRectangle(1, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteSE()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 8, width, height), GridToRectangle(3, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNW()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(4, 8, width, height), GridToRectangle(5, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNE()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(6, 8, width, height), GridToRectangle(7, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateCloudSprite()
        {
            int width = (int) SpriteSize.CloudX;
            int height = (int) SpriteSize.CloudY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 9, width, height), GridToRectangle(1, 9, width, height), 
                GridToRectangle(2, 9, width, height), GridToRectangle(3, 9, width, height), 
                GridToRectangle(4, 9, width, height), GridToRectangle(5, 9, width, height), 
                GridToRectangle(6, 9, width, height), GridToRectangle(7, 9, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateHitEffectSprite() 
        {
            return new StaticSprite(enemySpriteSheet, GridToRectangle(6, 10, 
               (int) SpriteSize.HitEffectX, (int) SpriteSize.HitEffectY));
        }
        
        public ISprite CreateEnemyDeathSprite()
        {
            int width = (int)SpriteSize.EnemyDeathX;
            int height = (int)SpriteSize.EnemyDeathY;
            Rectangle[] sourceRectangles = { GridToRectangle(0, 12, width, height), GridToRectangle(1, 12, width, height),
                GridToRectangle(2, 12, width, height), GridToRectangle(3, 12, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
    }

}