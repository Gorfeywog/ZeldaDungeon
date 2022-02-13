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

        public ISprite createKeeseSprite()
        {
            Point[] topLefts = { GridToPoint(0, 4), GridToPoint(1, 4) };
            return new KeeseSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createAquamentusSprite()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new AquamentusSpriteLeft(enemySpriteSheet, topLefts);
        }

        public ISprite createGelSprite()
        {
            Point[] topLefts = { GridToPoint(1, 0), GridToPoint(1, 1) };
            return new GelSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createRopeSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(0, 5), GridToPoint(1, 5) };
            return new RopeSpriteLeft(enemySpriteSheet, topLefts);
        }

        public ISprite createRopeSpriteRight()
        {
            Point[] topLefts = { GridToPoint(2, 5), GridToPoint(3, 5) };
            return new RopeSpriteRight(enemySpriteSheet, topLefts);
        }

        public ISprite createStalfosSprite()
        {
            Point[] topLefts = { GridToPoint(0, 6), GridToPoint(1, 6) };
            return new StalfosSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createTrapSprite()
        {
            Point topLeft = GridToPoint(0, 7);
            return new TrapSprite(enemySpriteSheet, topLeft);
        }

        public ISprite createBlueGoriyaSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(4, 2), GridToPoint(5, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createBlueGoriyaSpriteRight()
        {
            Point[] topLefts = { GridToPoint(6, 2), GridToPoint(7, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createBlueGoriyaSpriteUp()
        {
            Point[] topLefts = { GridToPoint(0, 2), GridToPoint(1, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createBlueGoriyaSpriteDown()
        {
            Point[] topLefts = { GridToPoint(2, 2), GridToPoint(3, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createRedGoriyaSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(4, 3), GridToPoint(5, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createRedGoriyaSpriteRight()
        {
            Point[] topLefts = { GridToPoint(6, 3), GridToPoint(7, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createRedGoriyaSpriteUp()
        {
            Point[] topLefts = { GridToPoint(0, 3), GridToPoint(1, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite createRedGoriyaSpriteDown()
        {
            Point[] topLefts = { GridToPoint(2, 3), GridToPoint(3, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }
    }
}
