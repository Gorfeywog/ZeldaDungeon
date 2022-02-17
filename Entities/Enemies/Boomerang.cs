using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Boomerang : IProjectile
    {
        public ISprite BoomerangSprite { get; set; }
        public Point CurrentPoint { get => new Point(posX, posY); }
        private int posX;
        private int posY;
        private int initPosX;
        private int initPosY;
        private int xChange;
        private int yChange;
        private Random rand;
        private int currentFrame;


        public Boomerang(Point position, int xChange, int yChange)
        {
            BoomerangSprite = EnemySpriteFactory.Instance.CreateBoomerangSprite();
            initPosX = position.X;
            initPosY = position.Y;
            posX = position.X;
            posY = position.Y;
            this.xChange = xChange;
            this.yChange = yChange;
            rand = new Random();
            currentFrame = 0;
        }

        public void Move()
        {
            if (currentFrame < Math.Abs(xChange * 2))
            {
                if (xChange > 0)
                {
                    posX += xChange - currentFrame;
                }
                else
                {
                    posX += xChange + currentFrame;
                }
            }
            if (currentFrame < Math.Abs(yChange * 2))
            {
                if (yChange > 0)
                {
                    posY += yChange - currentFrame;
                }
                else
                {
                    posY += yChange + currentFrame;
                }
            }

        }

        public bool ReadyToDespawn
        {
            get => posX == initPosX && posY == initPosY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BoomerangSprite.Draw(spriteBatch, CurrentPoint);
        }

        public void Update()
        {
            currentFrame++;
            this.Move();
            BoomerangSprite.Update();
        }

        public void DespawnEffect() { }


    }
}