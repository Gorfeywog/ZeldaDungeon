using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Stalfos : IEnemy
    {
        public ISprite StalfosSprite { get; set; }
        public bool ReadyToDespawn { get; private set; }
        public bool isFriendly { get => false; }

        public Rectangle CurrentLoc { get; set; }

        private int currentFrame;
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public EntityList roomEntities;
        private int CurrentHealth;
        private int damageCountdown = 0;
        private int Knockbacks = 0;
        private Room r;
        private Direction direction;

        public Stalfos(Point position, Room r)
        {
            this.r = r;
            StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
            int width = (int)SpriteUtil.SpriteSize.StalfosX;
            int height = (int)SpriteUtil.SpriteSize.StalfosY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            //CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
            CurrentHealth = 12;
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
            ReadyToDespawn = false;
        }

        public void Move()
        {
            if (Knockbacks > 0)
            {
                Knockback(direction);
                Knockbacks--;
            }
            else
            {
                if (!WillMove) { return; }
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


        }

        public void Attack() { }
        public void TakeDamage(Direction direction)
        {
            if (damageCountdown == 0)
            {
                this.direction = direction;
                Knockbacks = 3;
                CurrentHealth--;
                SoundManager.Instance.PlaySound("EnemyZapped");
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }
            if (CurrentHealth == 0) ReadyToDespawn = true;
            StalfosSprite.Damaged = true;
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
                        newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
                        break;
                    case Direction.Up:
                        newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - locChange), CurrentLoc.Size);
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
            StalfosSprite.Draw(spriteBatch, CurrentLoc);
        }

        private static readonly int MOVE_TIMER = 8;
        private bool WillMove => currentFrame % MOVE_TIMER == 0;
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
            Collision.Update();
        }
        public void DespawnEffect()
        {
            int rupeeRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            int heartRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            int arrowRoll = SpriteUtil.Rand.Next(SpriteUtil.GENERIC_RUPEE_ROLL_CAP);
            if (heartRoll > SpriteUtil.HEART_THRESHOLD)
            {
                r.RegisterEntity(new HeartPickup(CurrentLoc.Location));
            }
            if (arrowRoll > SpriteUtil.MAGIC_ARROW_THRESHOLD)
            {
                r.RegisterEntity(new ArrowPickup(CurrentLoc.Location, r.G, true));
            } else
            {
                r.RegisterEntity(new ArrowPickup(CurrentLoc.Location, r.G, false));
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