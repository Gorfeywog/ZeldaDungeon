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
        public Rectangle CurrentLoc { get; set; }
        private Point InitPoint;
        private Direction dir;
        private int velocity;
        private Random rand;
        private int currentFrame;
        private bool isMagic;
        // consider adding a bool like "isFriendly" later on

        public Boomerang(Point position, Direction dir, bool isMagic)
        {
            var esf = EnemySpriteFactory.Instance;
            BoomerangSprite = isMagic ? esf.CreateMagicBoomerangSprite() : esf.CreateBoomerangSprite();
            InitPoint = position;
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
			int height = (int)SpriteUtil.SpriteSize.BoomerangY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.dir = dir;
            this.isMagic = isMagic;
            velocity = isMagic ? 12 : 8; // magic ones go faster
            rand = new Random();
            currentFrame = 0;
        }

        public void Move() // this should probably be made less jank
        {
            CurrentLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, dir, velocity), CurrentLoc.Size);
        }

        public bool ReadyToDespawn
        {
            get => currentFrame > 0 && CurrentLoc.Location == InitPoint;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BoomerangSprite.Draw(spriteBatch, CurrentLoc);
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