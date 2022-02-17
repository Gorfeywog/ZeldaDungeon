using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.EnemySprites;

namespace ZeldaDungeon.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpriteSheet;
        private static readonly int gridX = 32; // how wide each sprite is
        private static readonly int gridY = 32;

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

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }

        public ISprite CreateKeeseSprite()
        {
            Point[] topLefts = { GridToPoint(0, 4), GridToPoint(1, 4) };
            return new AnimatedEnemySprite(enemySpriteSheet, 16, 10, topLefts);
        }

        public ISprite CreateAquamentusSprite()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new AnimatedEnemySprite(enemySpriteSheet, 24, 32, topLefts);
        }

        public ISprite CreateFireballSprite()
        {
            Point[] topLefts = { GridToPoint(0, 11), GridToPoint(1, 11), GridToPoint(2, 11), GridToPoint(3, 11)};
            return new AnimatedEnemySprite(enemySpriteSheet, 8, 10, topLefts);
        }

        public ISprite CreateGelSprite()
        {
            Point[] topLefts = { GridToPoint(0, 1), GridToPoint(1, 1) };
            return new AnimatedEnemySprite(enemySpriteSheet, 8, 9, topLefts);
        }

        public ISprite CreateRopeSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(0, 5), GridToPoint(1, 5) };
            return new AnimatedEnemySprite(enemySpriteSheet, 15, 15, topLefts);
        }

        public ISprite CreateRopeSpriteRight()
        {
            Point[] topLefts = { GridToPoint(2, 5), GridToPoint(3, 5) };
            return new AnimatedEnemySprite(enemySpriteSheet, 15, 15, topLefts);
        }

        public ISprite CreateStalfosSprite()
        {
            Point[] topLefts = { GridToPoint(0, 6), GridToPoint(1, 6) };
            return new AnimatedEnemySprite(enemySpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateTrapSprite()
        {
            Point topLeft = GridToPoint(0, 7);
            return new StaticEnemySprite(enemySpriteSheet, 16, 16, topLeft);
        }

        public ISprite CreateBlueGoriyaSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(4, 2), GridToPoint(5, 2) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateBlueGoriyaSpriteRight()
        {
            Point[] topLefts = { GridToPoint(6, 2), GridToPoint(7, 2) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateBlueGoriyaSpriteUp()
        {
            Point[] topLefts = { GridToPoint(0, 2), GridToPoint(1, 2) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateBlueGoriyaSpriteDown()
        {
            Point[] topLefts = { GridToPoint(2, 2), GridToPoint(3, 2) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(4, 3), GridToPoint(5, 3) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteRight()
        {
            Point[] topLefts = { GridToPoint(6, 3), GridToPoint(7, 3) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteUp()
        {
            Point[] topLefts = { GridToPoint(0, 3), GridToPoint(1, 3) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteDown()
        {
            Point[] topLefts = { GridToPoint(2, 3), GridToPoint(3, 3) };
            return new AnimatedEnemySprite(enemySpriteSheet, 14, 16, topLefts);
        }

        public ISprite CreateBoomerangSprite()
        {
            Point[] topLefts = { GridToPoint(0, 10), GridToPoint(1, 10), GridToPoint(2, 10) };
            return new AnimatedEnemySprite(enemySpriteSheet, 5, 8, topLefts);
        }

        public ISprite CreateWallMasterSpriteSW()
        {
            Point[] topLefts = { GridToPoint(0, 8), GridToPoint(1, 8) };
            return new AnimatedEnemySprite(enemySpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateWallMasterSpriteSE()
        {
            Point[] topLefts = { GridToPoint(2, 8), GridToPoint(3, 8) };
            return new AnimatedEnemySprite(enemySpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateWallMasterSpriteNW()
        {
            Point[] topLefts = { GridToPoint(4, 8), GridToPoint(5, 8) };
            return new AnimatedEnemySprite(enemySpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateWallMasterSpriteNE()
        {
            Point[] topLefts = { GridToPoint(6, 8), GridToPoint(7, 8) };
            return new AnimatedEnemySprite(enemySpriteSheet, 16, 16, topLefts);
        }
    }

}