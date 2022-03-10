using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.Entities.Projectiles
{
    public class BoomerangProjectile : IProjectile
    {
        public ISprite BoomerangSprite { get; set; }
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
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
        private int velocity; // properly is a speed
        private Random rand;
        private bool isReturning = false; // toggles to true when it turns around
        private bool isMagic;
        private int currentFrame;
        private Game1 g; // needed to give Link his boomerang back after throwing it away
        private int magicSpeed = 4 * SpriteUtil.SCALE_FACTOR;
        private int normalSpeed = 3 * SpriteUtil.SCALE_FACTOR;
        public BoomerangProjectile(IEntity thrower, Direction dir, bool isMagic, Game1 g)
        {
            this.g = g;
            targetDir = dir;
            var esf = EnemySpriteFactory.Instance;
            BoomerangSprite = isMagic ? esf.CreateMagicBoomerangSprite() : esf.CreateBoomerangSprite();
            this.thrower = thrower;
            Point pos = thrower.CurrentLoc.Center;
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
			int height = (int)SpriteUtil.SpriteSize.BoomerangY;
			CurrentLoc = new Rectangle(pos, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.isMagic = isMagic;
            velocity = isMagic ? magicSpeed : normalSpeed;
            rand = new Random();
            currentFrame = 0;
        }

        public void Move() // TODO - check for colliding with walls? 
        {
            Point path;
            if (isReturning)
            {
                Point throwerPos = thrower.CurrentLoc.Center;
                path = throwerPos - TopLeft;
            }
            else
            {
                path = EntityUtils.Offset(new Point(), targetDir, 1);
            }
            var pathVec = path.ToVector2();
            // if will reach the thrower, it ceases to exist
            // maybe move this into Update somehow?
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
            int moveChance = 4;
            if (currentFrame % moveChance == 0)
            {
                if (isReturning)
                {
                    velocity += SpriteUtil.SCALE_FACTOR / 3;
                }
                else
                {
                    velocity -= SpriteUtil.SCALE_FACTOR / 3;
                    if (velocity == 0)
                    {
                        isReturning = true;
                    }
                }
            }
            Move();

            BoomerangSprite.Update();
        }

        public void DespawnEffect() // give Link his boomerang back
        {
            if (thrower is ILink link)
            {
                link.AddItem(new BoomerangItem(g, isMagic));
            }
        }
        public void OnHit(IEntity target)
        {
            bool friendly = thrower is ILink;
            if (target is IEnemy en && friendly)
            {
                en.TakeDamage();
            }
            else if (target is ILink link && !friendly)
            {
                link.TakeDamage();
            }
        }
    }
}