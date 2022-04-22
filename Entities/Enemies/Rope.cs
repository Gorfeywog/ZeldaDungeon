using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Rope : IEnemy
    {
        private ISprite RopeSprite { get; set; }
        public bool ReadyToDespawn { get => currentHealth <= 0; }
        public bool IsFriendly { get => false; }

        public Rectangle CurrentLoc { get; set; }

        private int currentFrame;
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        private int currentHealth;
        private int damageCountdown = 0;
        private Direction currDirection;
        private Room r;

        public Rope(Point position, Room r)
        {
            this.r = r;
            RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
            currDirection = Direction.Left;
            currentHealth = SpriteUtil.MEDIUM_MAX_HEALTH;
            int width = (int)SpriteUtil.SpriteSize.RopeX;
            int height = (int)SpriteUtil.SpriteSize.RopeY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
        }

        private bool WillMove => currentFrame % MOVE_TIMER == 0;
        private static readonly int CHANGE_DIR_CHANCE = 4;
        public void Move()
        {
            if (!WillMove) { return; }
            if (SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE) == 0)
            {
                switch (SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE))
                {
                    case 0:
                        currDirection = Direction.Left;
                        RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
                        break;

                    case 1:
                        currDirection = Direction.Right;
                        RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteRight();
                        break;

                    case 2:
                        currDirection = Direction.Up;
                        break;

                    case 3:
                        currDirection = Direction.Down;
                        break;

                    default:
                        break;
                }
            }

            // Determines which way to move
            int locChange = 4 * SpriteUtil.SCALE_FACTOR;
            Point newPos = EntityUtils.Offset(CurrentLoc.Location, currDirection, locChange);
            if (!Collision.WillHitBlock(new Rectangle(newPos, CurrentLoc.Size)))
            {
                CurrentLoc = new Rectangle(newPos, CurrentLoc.Size);
            }

        }

        public void Attack() { }

        public void TakeDamage(DamageLevel level)
        {
            if (damageCountdown == 0)
            {
                currentHealth -= (int)level;
                SoundManager.Instance.PlaySound("EnemyZapped");
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }

            RopeSprite.Damaged = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            RopeSprite.Draw(spriteBatch, CurrentLoc);
        }

        private static int MOVE_TIMER = 8;
        public void Update()
        {
            if (RopeSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    RopeSprite.Damaged = false;
                }
            }
            RopeSprite.Update();
            currentFrame++;
            Collision.Update();
        }
        public void DespawnEffect()
        {
            int rupeeRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            int heartRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            if (heartRoll > SpriteUtil.HEART_THRESHOLD)
            {
                r.RegisterEntity(new HeartPickup(CurrentLoc.Location));
            }
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