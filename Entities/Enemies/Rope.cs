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
        public ISprite RopeSprite { get; set; }
        public bool ReadyToDespawn { get; private set; }
        public bool isFriendly { get => false; }

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
            currentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
            int width = (int)SpriteUtil.SpriteSize.RopeX;
            int height = (int)SpriteUtil.SpriteSize.RopeY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
        }

        private bool WillMove => currentFrame % MOVE_TIMER == 0;
        public void Move()
        {
            if (!WillMove) { return; }
            //One in four chance to change directions
            int changeDirChance = 4;
            if (SpriteUtil.Rand.Next(changeDirChance) == 0)
            {
                switch (SpriteUtil.Rand.Next(changeDirChance))
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

            //Determines which way to move
            int locChange = 4 * SpriteUtil.SCALE_FACTOR;
            Point newPos = EntityUtils.Offset(CurrentLoc.Location, currDirection, locChange);
            if (!Collision.WillHitBlock(new Rectangle(newPos, CurrentLoc.Size)))
            {
                CurrentLoc = new Rectangle(newPos, CurrentLoc.Size);
            }

        }

        public void Attack() { }

        public void TakeDamage(Direction direction)
        {
            if (damageCountdown == 0)
            {
                Knockback(direction);
                currentHealth--;
                SoundManager.Instance.PlaySound("EnemyZapped");
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }

            if (currentHealth == 0) ReadyToDespawn = true;
            RopeSprite.Damaged = true;
        }

        public void Knockback(Direction direction)
        {
            Rectangle newPos;
            int locChange = SpriteUtil.KNOCKBACK_SPEED * SpriteUtil.SCALE_FACTOR;
            for (int i = 0; i < 2; i++)
            {
                switch (direction)
                {
                    case Direction.Down:
                        newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - locChange), CurrentLoc.Size);
                        break;
                    case Direction.Up:
                        newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
                        break;
                    case Direction.Left:
                        newPos = new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size);
                        break;
                    case Direction.Right:
                        newPos = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
                        break;
                    default:
                        newPos = CurrentLoc;
                        break;
                }
                if (!Collision.WillHitBlock(newPos))
                {
                    CurrentLoc = newPos;
                }
            }
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