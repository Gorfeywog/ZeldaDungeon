using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    public class Boomerang : IProjectile
    {
        public ISprite BoomerangSprite { get; set; }
        public Point CurrentPoint { get; set; }
        private Point InitPoint;
        private Direction dir;
        private int velocity;
        private Random rand;
        private int currentFrame;


        public Boomerang(Point position, Direction dir)
        {
            BoomerangSprite = EnemySpriteFactory.Instance.CreateBoomerangSprite();
            InitPoint = position;
            CurrentPoint = position;
            this.dir = dir;
            velocity = 8;
            rand = new Random();
            currentFrame = 0;
        }

        public void Move() // this should probably be made less jank
        {
            CurrentPoint = EntityUtils.Offset(CurrentPoint, dir, velocity);
        }

        public bool ReadyToDespawn
        {
            get => currentFrame > 0 && CurrentPoint == InitPoint;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BoomerangSprite.Draw(spriteBatch, CurrentPoint);
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame % 8 == 0)
            {
                velocity--;
            }
            Move();

            BoomerangSprite.Update();
        }

        public void DespawnEffect() { }


    }
}