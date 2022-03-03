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
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 4, 16, 10), SpriteUtil.GridToRectangle(1, 4, 16, 10) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateAquamentusSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, 24, 32), SpriteUtil.GridToRectangle(1, 0, 24, 32) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateFireballSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 11, 8, 10), SpriteUtil.GridToRectangle(1, 11, 8, 10),
                SpriteUtil.GridToRectangle(2, 11, 8, 10), SpriteUtil.GridToRectangle(3, 11, 8, 10)};
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateGelSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 1, 8, 9), SpriteUtil.GridToRectangle(1, 1, 8, 9) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteLeft()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 5, 15, 15), SpriteUtil.GridToRectangle(1, 5, 15, 15) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteRight()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 5, 15, 15), SpriteUtil.GridToRectangle(3, 5, 15, 15) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateStalfosSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 6, 16, 16), SpriteUtil.GridToRectangle(1, 6, 16, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateTrapSprite()
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(0, 7, 16, 16));
        }

        public ISprite CreateOldManSprite()
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(1, 7, 16, 16));
        }

        public ISprite CreateBlueGoriyaSpriteLeft()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(4, 2, 14, 16), SpriteUtil.GridToRectangle(5, 2, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteRight()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(6, 2, 14 ,16), SpriteUtil.GridToRectangle(7, 2, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteUp()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 2, 14, 16), SpriteUtil.GridToRectangle(1, 2, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateBlueGoriyaSpriteDown()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 2, 14, 16), SpriteUtil.GridToRectangle(3, 2, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteLeft()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(4, 3, 14, 16), SpriteUtil.GridToRectangle(5, 3, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteRight()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(6, 3, 14, 16), SpriteUtil.GridToRectangle(7, 3, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteUp()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 3, 14, 16), SpriteUtil.GridToRectangle(1, 3, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRedGoriyaSpriteDown()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 3, 14, 16), SpriteUtil.GridToRectangle(3, 3, 14, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticBoomerangSprite()
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(0, 10, 5, 8));
        }
        public ISprite CreateBoomerangSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 10, 5, 8), 
                SpriteUtil.GridToRectangle(1, 10, 5, 8), SpriteUtil.GridToRectangle(2, 10, 5, 8) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticMagicBoomerangSprite()
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(3, 10, 5, 8));
        }
        public ISprite CreateMagicBoomerangSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(3, 10, 5, 8), 
                SpriteUtil.GridToRectangle(4, 10, 5, 8), SpriteUtil.GridToRectangle(5, 10, 5, 8) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateWallMasterSpriteSW()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 8, 16, 16), SpriteUtil.GridToRectangle(1, 8, 16, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteSE()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(2, 8, 16, 16), SpriteUtil.GridToRectangle(3, 8, 16, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNW()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(4, 8, 16, 16), SpriteUtil.GridToRectangle(5, 8, 16, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNE()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(6, 8, 16, 16), SpriteUtil.GridToRectangle(7, 8, 16, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateCloudSprite()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 9, 16, 16), SpriteUtil.GridToRectangle(1, 9, 16, 16), 
                SpriteUtil.GridToRectangle(2, 9, 16, 16), SpriteUtil.GridToRectangle(3, 9, 16, 16), SpriteUtil.GridToRectangle(4, 9, 16, 16),
                SpriteUtil.GridToRectangle(5, 9, 16, 16), SpriteUtil.GridToRectangle(6, 9, 16, 16), SpriteUtil.GridToRectangle(7, 9, 16, 16) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateHitEffectSprite() // should maybe live on a different spritesheet
        {
            return new StaticSprite(enemySpriteSheet, SpriteUtil.GridToRectangle(6, 10, 8, 8));
        }
    }

}