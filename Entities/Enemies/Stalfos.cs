using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Stalfos : IEnemy
    {
        public ISprite StalfosSprite { get; set; }
        public bool ReadyToDespawn { get; private set; }

        public Rectangle CurrentLoc { get; set; }

        private int currentFrame;
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public EntityList roomEntities;
        private int CurrentHealth;
        private static readonly int damageDelay = 80;
        private int damageCountdown = 0;
        private Room r;

        public Stalfos(Point position, Room r)
        {
            this.r = r;
            StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
            int width = (int)SpriteUtil.SpriteSize.StalfosX;
            int height = (int)SpriteUtil.SpriteSize.StalfosY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
            ReadyToDespawn = false;
        }

        public void Move()
        {
            int dirChance = 3;
            int locChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
            Rectangle newPos;
            if (SpriteUtil.Rand.Next(dirChance) == 0)
            {
                newPos = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size); ;
            }
            else
            {
                newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
            }
            if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;

        }

        public void Attack()
        {

        }

        public void TakeDamage()
        {
            if (damageCountdown == 0)
            {
                CurrentHealth--;
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }
            if (CurrentHealth == 0) ReadyToDespawn = true;
            StalfosSprite.Damaged = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            StalfosSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            if (StalfosSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    StalfosSprite.Damaged = false;
                }
            }
            StalfosSprite.Update();
            currentFrame++;
            int moveChance = 8;
            if (currentFrame % moveChance == 0)
            {
                Move();
            }

            Collision.Update();
        }
        public void DespawnEffect()
        {
            int rupeeRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            if (rupeeRoll > SpriteUtil.GENERIC_5_RUPEE_THRESHOLD)
            {
                r.RegisterEntity(new RupeePickup(CurrentLoc.Location, 5));
            }
            else if (rupeeRoll > SpriteUtil.GENERIC_RUPEE_THRESHOLD)
            {
                r.RegisterEntity(new RupeePickup(CurrentLoc.Location, 1));
            }
        }
    }
}