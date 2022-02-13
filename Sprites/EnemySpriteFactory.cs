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
            return new KeeseSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateAquamentusSprite()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new AquamentusSpriteLeft(enemySpriteSheet, topLefts);
        }

        public ISprite CreateGelSprite()
        {
            Point[] topLefts = { GridToPoint(1, 0), GridToPoint(1, 1) };
            return new GelSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateRopeSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(0, 5), GridToPoint(1, 5) };
            return new RopeSpriteLeft(enemySpriteSheet, topLefts);
        }

        public ISprite CreateRopeSpriteRight()
        {
            Point[] topLefts = { GridToPoint(2, 5), GridToPoint(3, 5) };
            return new RopeSpriteRight(enemySpriteSheet, topLefts);
        }

        public ISprite CreateStalfosSprite()
        {
            Point[] topLefts = { GridToPoint(0, 6), GridToPoint(1, 6) };
            return new StalfosSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateTrapSprite()
        {
            Point topLeft = GridToPoint(0, 7);
            return new TrapSprite(enemySpriteSheet, topLeft);
        }

        public ISprite CreateBlueGoriyaSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(4, 2), GridToPoint(5, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateBlueGoriyaSpriteRight()
        {
            Point[] topLefts = { GridToPoint(6, 2), GridToPoint(7, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateBlueGoriyaSpriteUp()
        {
            Point[] topLefts = { GridToPoint(0, 2), GridToPoint(1, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateBlueGoriyaSpriteDown()
        {
            Point[] topLefts = { GridToPoint(2, 2), GridToPoint(3, 2) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteLeft()
        {
            Point[] topLefts = { GridToPoint(4, 3), GridToPoint(5, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteRight()
        {
            Point[] topLefts = { GridToPoint(6, 3), GridToPoint(7, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteUp()
        {
            Point[] topLefts = { GridToPoint(0, 3), GridToPoint(1, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }

        public ISprite CreateRedGoriyaSpriteDown()
        {
            Point[] topLefts = { GridToPoint(2, 3), GridToPoint(3, 3) };
            return new GoriyaSprite(enemySpriteSheet, topLefts);
        }
    }
}
