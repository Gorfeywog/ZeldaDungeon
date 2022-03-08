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
        private Point TopLeft
        {
            get
            {
                return new Point(CurrentLoc.X, CurrentLoc.Y);
            }
            set
            {
                Point size = CurrentLoc.Size;
                CurrentLoc = new Rectangle(value, size);
            }
        }
        private IEntity thrower;
        private Direction targetDir;
        private int velocity;
        private Random rand;
        private bool isReturning = false; // toggles to true when it reaches the target
        private bool isMagic;
        private int currentFrame;

        public Boomerang(IEntity thrower, Direction dir, bool isMagic)
        {
            targetDir = dir;
            var esf = EnemySpriteFactory.Instance;
            BoomerangSprite = isMagic ? esf.CreateMagicBoomerangSprite() : esf.CreateBoomerangSprite();
            this.thrower = thrower;
            Point pos = new Point(thrower.CurrentLoc.X, thrower.CurrentLoc.Y);
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
			int height = (int)SpriteUtil.SpriteSize.BoomerangY;
			CurrentLoc = new Rectangle(pos, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.isMagic = isMagic;
            velocity = isMagic ? 12 : 8; // magic ones go faster
            rand = new Random();
            currentFrame = 0;
        }

        public void Move() // TODO - check for colliding with walls? 
        {
            Point path;
            if (isReturning)
            {
                Point throwerPos = new Point(thrower.CurrentLoc.X, thrower.CurrentLoc.Y);
                path = throwerPos - TopLeft;
            }
            else
            {
                path = EntityUtils.Offset(new Point(), targetDir, 1);
            }
            var pathVec = path.ToVector2();
            // if will reach the position, have it turn back or cease to exist
            // should maybe move into Update somehow?
            if (pathVec.Length() < velocity && isReturning)
            {
                ReadyToDespawn = true;
            }
            pathVec.Normalize();
            var offset = pathVec * velocity;
            var PointOffset = new Point((int)Math.Round(offset.X), (int)Math.Round(offset.Y));
            TopLeft += PointOffset;
            
        }

        public bool ReadyToDespawn { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            BoomerangSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame % 4 == 0)
            {
                if (isReturning)
                {
                    velocity++;
                }
                else
                {
                    velocity--;
                    if (velocity == 0)
                    {
                        isReturning = true;
                    }
                }
            }
            Move();

            BoomerangSprite.Update();
        }

        public void DespawnEffect() { }


    }
}