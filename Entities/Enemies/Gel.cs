using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Gel : IEnemy
    {
        public ISprite GelSprite { get; set; }
        public bool ReadyToDespawn { get; private set; }
        private int currentFrame;
        private CollisionHandler Collision { get; set; }
        private int currentHealth;
        public bool isFriendly { get => false; }

        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public EntityList roomEntities;
        public Rectangle CurrentLoc { get; set; }
        private int damageCountdown = 0;
        private Room r;

        public Gel(Point position, Room r)
        {
            this.r = r;
            GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
            int width = (int)SpriteUtil.SpriteSize.GelX;
            int height = (int)SpriteUtil.SpriteSize.GelY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            currentFrame = 0;
            ReadyToDespawn = false;
            currentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
            Collision = new CollisionHandler(r, this);
        }
        private bool WillMove => currentFrame % MOVE_TIMER == 0;
        public void Move()
        {
            if (!WillMove) return;
            int movingNum = SpriteUtil.Rand.Next(5);
            int locChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
            Rectangle newPos;
            if (movingNum < 2) // 2 in 5 chance to move in x direction
            {
                newPos = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
                if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;
            }
            else if (movingNum > 2) // 2 in 5 chance to move in y direction
            {
                newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
                if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;
            }
            // 1 in 5 chance to not move at all
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
            GelSprite.Damaged = true;
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
            GelSprite.Draw(spriteBatch, CurrentLoc);
        }

        private static readonly int MOVE_TIMER = 8;
        public void Update()
        {
            if (GelSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    GelSprite.Damaged = false;
                }
            }
            GelSprite.Update();
            currentFrame++;
            Collision.Update();
        }
        public void DespawnEffect()
        {
            int rupeeRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            int bombRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            int heartRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            if (bombRoll > SpriteUtil.BOMB_THRESHOLD)
            {
                r.RegisterEntity(new BombPickup(CurrentLoc.Location, r.G));
            }
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