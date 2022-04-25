using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private int initX;
        private Direction currDirection;
        private int currentFrame;
        private Room r;
        private int damageCountdown = 0;
        private int stunCountdown = 0;
        int verticalDirection = 1;
        int horizontalDirection = 1;


        public GameNWatch(Point position, Room r)
        {
            GameNWatchSprite = EnemySpriteFactory.Instance.CreateMrGameNWatchSprite();
            int width = (int)SpriteUtil.SpriteSize.BowserX;
            int height = (int)SpriteUtil.SpriteSize.BowserY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            initX = position.X;
            currentHealth = SpriteUtil.MEDIUM_MAX_HEALTH;
            this.r = r;
            currentFrame = 0;
        }

        private static readonly int CHANGE_DIR_CHANCE = 4;
        private static readonly int X_LIMIT = 8;
        private static readonly int SPEED = 8;
        public void Move()
        {
            if (!WillMove)
            {
                return;
            }
            //One in four chance to change directions
            if (SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE) == 0)
            {
                currDirection = SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE) switch
                {
                    0 => Direction.Left,
                    1 => Direction.Right,
                    2 => Direction.Up,
                    3 => Direction.Down,
                    _ => currDirection,
                };
            }

            //Determines which way to move
            int locChange = SPEED * SpriteUtil.SCALE_FACTOR;
            // Point newPos = EntityUtils.Offset(CurrentLoc.Location, currDirection, locChange);

            Rectangle newPos = new Rectangle(CurrentLoc.X + (horizontalDirection * locChange), CurrentLoc.Y + (verticalDirection * locChange), CurrentLoc.Width, CurrentLoc.Height);
            if (Collision.WillHitBlock(newPos))
            {
                Direction collisionDirection = Collision.DetectDirection(newPos);
                if (collisionDirection == Direction.Down || collisionDirection == Direction.Up) verticalDirection *= -1;
                else if (collisionDirection == Direction.Left || collisionDirection == Direction.Right) horizontalDirection *= -1;
                newPos = new Rectangle(CurrentLoc.X + (horizontalDirection * locChange), CurrentLoc.Y + (verticalDirection * locChange), CurrentLoc.Width, CurrentLoc.Height);
                Debug.Write(collisionDirection);
            }
            if (!Collision.WillHitBlock(newPos))
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
        private static readonly int MOVE_TIMER = 8;
        private static readonly int ATTACK_TIMER = 64;
        private static readonly int ATTACK_CHANCE = 4;
        private bool WillMove => currentFrame % MOVE_TIMER == 0 && stunCountdown == 0;
        private bool WillAttack => currentFrame % ATTACK_TIMER == 0 && SpriteUtil.Rand.Next(ATTACK_CHANCE) == 0 && stunCountdown == 0;
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