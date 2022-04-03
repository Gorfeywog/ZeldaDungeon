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

        public void Move()
        {
            //One in four chance per move for aquamentus to change direction
            int changeDirChance = 4;
            int xLimit = 2;
            int locChange = 4 * SpriteUtil.SCALE_FACTOR;
            if (SpriteUtil.Rand.Next(changeDirChance) == 0)
            {
                movingLeft = !movingLeft;
            }

            if (movingLeft)
            {
                if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size)))

                {
                    CurrentLoc = new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size);
                }
                if (CurrentLoc.X < initX - xLimit * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR)
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
                if (CurrentLoc.X > initX + xLimit * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR)
                {
                    movingLeft = !movingLeft;
                }
            }

        }

        public void Attack()
        {
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
            int moveChance = 8;
            if (AquamentusSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    AquamentusSprite.Damaged = false;
                }
            }
            if (currentFrame % moveChance == 0)
            {
                Move();
            }
            int attackChance = 64;
            int randChance = 4;
            if (currentFrame % attackChance == 0 && SpriteUtil.Rand.Next(randChance) == 0)
            {
                Attack();
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
            r.RegisterEntity(new EnemyDeath(CurrentLoc.Location));
        }
    }
}