using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class GameNWatch : IEnemy
    {
        public bool ReadyToDespawn { get => currentHealth <= 0; }
        private ISprite GameNWatchSprite { get; set; }
        public Rectangle CurrentLoc { get; set; }
        public bool IsFriendly { get => false; }

        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        private int currentHealth;
        private Direction currDirection;
        private int currentFrame;
        private Room r;
        private int damageCountdown = 0;
        private int stunCountdown = 0;
        Rectangle newPos;


        public GameNWatch(Point position, Room r)
        {
            GameNWatchSprite = EnemySpriteFactory.Instance.CreateMrGameNWatchSprite();
            int width = (int)SpriteUtil.SpriteSize.GameNWatchX;
            int height = (int)SpriteUtil.SpriteSize.GameNWatchY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            currentHealth = SpriteUtil.MEDIUM_MAX_HEALTH;
            this.r = r;
            currentFrame = 0;
            currDirection = Direction.SW;
        }

        private static readonly int SPEED = 1;
        public void Move()
        {
            int locChange = SPEED * SpriteUtil.SCALE_FACTOR;

            switch (currDirection)
            {
                case Direction.SW:
                    newPos = new Rectangle(CurrentLoc.X - locChange, CurrentLoc.Y + locChange, CurrentLoc.Width, CurrentLoc.Height);
                    break;
                case Direction.NW:
                    newPos = new Rectangle(CurrentLoc.X - locChange, CurrentLoc.Y - locChange, CurrentLoc.Width, CurrentLoc.Height);
                    break;
                case Direction.NE:
                    newPos = new Rectangle(CurrentLoc.X + locChange, CurrentLoc.Y - locChange, CurrentLoc.Width, CurrentLoc.Height);
                    break;
                case Direction.SE:
                    newPos = new Rectangle(CurrentLoc.X + locChange, CurrentLoc.Y + locChange, CurrentLoc.Width, CurrentLoc.Height);
                    break;
            }

            if (Collision.WillHitBlock(newPos))
            {
                int LeftWall = r.RoomPos.X + 2 * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR;
                int RightWall = LeftWall + 8 * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR;
                int topWall = r.RoomPos.Y + 2 * (int)SpriteUtil.SpriteSize.GenericBlockY * SpriteUtil.SCALE_FACTOR;
                int BottomWall = topWall + 7 * (int)SpriteUtil.SpriteSize.GenericBlockY * SpriteUtil.SCALE_FACTOR;

                switch (currDirection)
                {
                    case Direction.SW:
                        if (newPos.X <= LeftWall) currDirection = Direction.SE;
                        if (newPos.Y >= BottomWall + 72) currDirection = Direction.NW;
                        break;
                    case Direction.NW:
                        if (newPos.X <= LeftWall) currDirection = Direction.NE;
                        if (newPos.Y <= topWall) currDirection = Direction.SW;
                        break;
                    case Direction.NE:
                        if (newPos.X >= RightWall) currDirection = Direction.NW;
                        if (newPos.Y <= topWall) currDirection = Direction.SE;
                        break;
                    case Direction.SE:
                        if (newPos.X >= RightWall) currDirection = Direction.SW;
                        if (newPos.Y >= BottomWall + 72) currDirection = Direction.NE;
                        break;
                }
            }
            else
            {
                CurrentLoc = newPos;
            }

        }

        public void Attack()
        {

        }

        public void TakeDamage(DamageLevel level)
        {
            if (damageCountdown == 0)
            {
                if (level == DamageLevel.Boomerang) // stun for boomerang hits
                {
                    stunCountdown = SpriteUtil.BOOM_STUN_LENGTH;
                }
                else
                {
                    currentHealth -= (int)level;
                }
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
                SoundManager.Instance.PlaySound("BossZapped");
            }
            GameNWatchSprite.Damaged = true;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameNWatchSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            currentFrame++;
            GameNWatchSprite.Update();
            if (stunCountdown > 0) { stunCountdown--; }
            if (GameNWatchSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    GameNWatchSprite.Damaged = false;
                }
            }
            Collision.Update();

        }

        private static readonly int ATTACK_TIMER = 64;
        private static readonly int ATTACK_CHANCE = 4;
        public void DespawnEffect()
        {
            SoundManager.Instance.PlaySound("BossRoaring");
            int rupeeRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            if (rupeeRoll > SpriteUtil.GENERIC_5_RUPEE_THRESHOLD)
            {
                int count = 5;
                r.RegisterEntity(new RupeePickup(CurrentLoc.Location, count));
            }
            else if (rupeeRoll > SpriteUtil.GENERIC_RUPEE_THRESHOLD)
            {
                r.RegisterEntity(new RupeePickup(CurrentLoc.Location, 1));
            }
            r.RegisterEntity(new EnemyDeath(CurrentLoc.Location));
        }
    }
}