using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Goriya : IEnemy
    {
        public bool ReadyToDespawn { get; private set; }
        public bool isFriendly { get => false; }

        public ISprite GoriyaSprite { get; set; }
        public Rectangle CurrentLoc { get; set; }
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }

        private int currentFrame;
        private bool IsAttacking { get => (boomerang != null) && !boomerang.ReadyToDespawn; }
        private Room r;
        private IProjectile boomerang;
        private bool isRed;

        private Direction currDirection;
        private static readonly int damageDelay = 80;
        private int damageCountdown = 0;
        private int CurrentHealth;

        public Goriya(Point position, Room r, bool isRed)
        {
            this.isRed = isRed;
            if (isRed)
            {
                GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
            }
            else
            {
                GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteLeft();
            }
            int width = (int)SpriteUtil.SpriteSize.GoriyaX;
            int height = (int)SpriteUtil.SpriteSize.GoriyaY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.r = r;
            currDirection = Direction.Left;
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
            CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
        }
        private static readonly int CHANGE_DIR_CHANCE = 4;
        public void Move()
        {
            if (!WillMove)
            {
                return;
            }
            //One in four chance to change directions
            if (SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE) == 0)
            {
                switch (SpriteUtil.Rand.Next(CHANGE_DIR_CHANCE))
                {
                    case 0:
                        currDirection = Direction.Left;
                        if (isRed)
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
                        }
                        else
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteLeft();
                        }
                        break;

                    case 1:
                        currDirection = Direction.Right;
                        if (isRed)
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteRight();
                        }
                        else
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteRight();
                        }
                        break;

                    case 2:
                        currDirection = Direction.Up;
                        if (isRed)
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteUp();
                        }
                        else
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteUp();
                        }
                        break;

                    case 3:
                        currDirection = Direction.Down;
                        if (isRed)
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteDown();
                        }
                        else
                        {
                            GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteDown();
                        }
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

        public void Attack()
        {
            if (!WillAttack)
            {
                return;
            }
            boomerang = new BoomerangProjectile(this, currDirection, false, r.G);
            r.RegisterEntity(boomerang);
        }

        public void TakeDamage(Direction direction)
        {
            if (damageCountdown == 0)
            {
                Knockback(direction);
                CurrentHealth--;
                SoundManager.Instance.PlaySound("EnemyZapped");
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }
            if (CurrentHealth == 0) ReadyToDespawn = true;
            GoriyaSprite.Damaged = true;
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
            GoriyaSprite.Draw(spriteBatch, CurrentLoc);
        }
        private static readonly int MOVE_TIMER = 8;
        private bool WillMove => currentFrame % MOVE_TIMER == 0 && !IsAttacking;
        private static readonly int ATTACK_TIMER = 128;
        private static readonly int ATTACK_CHANCE = 2;
        private bool WillAttack => currentFrame % ATTACK_TIMER == 0 && SpriteUtil.Rand.Next(ATTACK_CHANCE) == 0;
        public void Update()
        {
            if (GoriyaSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    GoriyaSprite.Damaged = false;
                }
            }
            currentFrame++;
            GoriyaSprite.Update();
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