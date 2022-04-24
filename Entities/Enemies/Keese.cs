using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Keese : IEnemy
    {
        public ISprite KeeseSprite { get; set; }
        public bool ReadyToDespawn { get; private set; }

        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.High; }
        public DrawLayer Layer { get => DrawLayer.High; }
        public EntityList roomEntities;
        public bool isFriendly { get => false; }

        public Rectangle CurrentLoc { get; set; }
        private int currentHealth;
        private int damageCountdown = 0;
        private Room r;
        private int currentFrame;

        public Keese(Point position, Room r)
        {
            this.r = r;
            KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
            int width = (int)SpriteUtil.SpriteSize.KeeseX;
            int height = (int)SpriteUtil.SpriteSize.KeeseY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
            currentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
            ReadyToDespawn = false;
        }

        private bool WillMove => currentFrame % MOVE_TIMER == 0;
        public void Move()
        {
            if (!WillMove) { return; }
            int locXChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
            int locYChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
            Rectangle newPos = new Rectangle(new Point(CurrentLoc.X + locXChange, CurrentLoc.Y + locYChange), CurrentLoc.Size);
            if (!Collision.WillHitBlock(newPos))
            {
                CurrentLoc = newPos;
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
            KeeseSprite.Damaged = true;
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
            KeeseSprite.Draw(spriteBatch, CurrentLoc);
        }
        private static readonly int MOVE_TIMER = 8;
        public void Update()
        {
            if (KeeseSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    KeeseSprite.Damaged = false;
                }
            }
            currentFrame++;
            KeeseSprite.Update();
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