using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Aquamentus : IEnemy
    {
        public bool ReadyToDespawn { get; set; }
        public ISprite AquamentusSprite { get; set; }
        public Rectangle CurrentLoc { get; set; }
        public bool isFriendly { get => false; }

        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        private int currentHealth;
        private int initX;
        private EntityList roomEntities;
        private Random rand;
        private bool movingLeft;
        private int currentFrame;
        private Room r;
        private int damageCountdown = 0;



        public Aquamentus(Point position, Room r)
        {
            AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
            ReadyToDespawn = false;
            int width = (int)SpriteUtil.SpriteSize.AquamentusX;
            int height = (int)SpriteUtil.SpriteSize.AquamentusY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            initX = position.X;
            currentHealth = SpriteUtil.AQUAMENTUS_MAX_HEALTH;
            movingLeft = true;
            this.r = r;
            currentFrame = 0;
        }

        private static readonly int CHANGE_DIR_CHANCE = 4;
        private static readonly int X_LIMIT = 2;
        private static readonly int SPEED = 4;
        public void Move()
        {
            if (!WillMove)
            {
                return;
            }
            //One in four chance per move for aquamentus to change direction
            int locChange = SPEED * SpriteUtil.SCALE_FACTOR;
            if (SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE) == 0)
            {
                movingLeft = !movingLeft;
            }

            if (movingLeft)
            {
                if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size)))

                {
                    CurrentLoc = new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size);
                }
                if (CurrentLoc.X < initX - X_LIMIT * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR)
                {
                    movingLeft = !movingLeft;
                }
            }
            else
            {
                if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size)))
                {
                    CurrentLoc = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
                }
                if (CurrentLoc.X > initX + X_LIMIT * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR)
                {
                    movingLeft = !movingLeft;
                }
            }

        }

        public void Attack()
        {
            if (!WillAttack)
            {
                return;
            }
            int fireballChange = SpriteUtil.Rand.Next(3) - 1;
            int fireballVel = -2 * SpriteUtil.SCALE_FACTOR;
            IProjectile fireballUp = new Fireball(CurrentLoc.Location, fireballVel, (1 + fireballChange) * SpriteUtil.SCALE_FACTOR);
            IProjectile fireballStraight = new Fireball(CurrentLoc.Location, fireballVel, fireballChange * SpriteUtil.SCALE_FACTOR);
            IProjectile fireballDown = new Fireball(CurrentLoc.Location, fireballVel, (-1 + fireballChange) * SpriteUtil.SCALE_FACTOR);
            r.RegisterEntity(fireballUp);
            r.RegisterEntity(fireballStraight);
            r.RegisterEntity(fireballDown);
        }

        public void TakeDamage()
        {
            if (damageCountdown == 0)
            {
				currentHealth--;
				damageCountdown = SpriteUtil.DAMAGE_DELAY;
				SoundManager.Instance.PlaySound("BossZapped");
                SoundManager.Instance.PlaySound("BossRoaring");
            }
            if (currentHealth == 0) ReadyToDespawn = true;
			AquamentusSprite.Damaged = true;

		}

        public void Draw(SpriteBatch spriteBatch)
        {
            AquamentusSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            currentFrame++;
            AquamentusSprite.Update();
            if (AquamentusSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    AquamentusSprite.Damaged = false;
                }
            }
            Collision.Update();

        }
        private static readonly int MOVE_TIMER = 8;
        private static readonly int ATTACK_TIMER = 64;
        private static readonly int ATTACK_CHANCE = 4;
        private bool WillMove => currentFrame % MOVE_TIMER == 0;
        private bool WillAttack => currentFrame % ATTACK_TIMER == 0 && SpriteUtil.Rand.Next(ATTACK_CHANCE) == 0;
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
            r.RegisterEntity(new EnemyDeath(CurrentLoc.Location));
        }
    }
}