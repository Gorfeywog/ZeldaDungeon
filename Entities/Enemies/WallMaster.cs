using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class WallMaster : IEnemy
    {
        private ISprite WallMasterSprite { get; set; }
        public bool ReadyToDespawn { get => currentHealth <= 0; }
        public bool IsFriendly { get => false; }

        public Rectangle CurrentLoc { get; set; }
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Ghost; }
        public DrawLayer Layer { get => DrawLayer.High; }
        private int currentFrame;
        private int currentHealth;
        private Room r;
        private int damageCountdown = 0;
        private int stunCountdown = 0;
        private Direction currDirection;
        public WallMaster(Point position, Room r)
        {
            this.r = r;
            WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
            currDirection = Direction.SE;
            int width = (int)SpriteUtil.SpriteSize.WallMasterX;
            int height = (int)SpriteUtil.SpriteSize.WallMasterY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
            currentHealth = SpriteUtil.LARGE_MAX_HEALTH;

        }

        public void Move()
        {
            if (!WillMove) { return; }
            int changeDirChance = 60;
            if (SpriteUtil.Rand.Next(8) == 0 || currentFrame % changeDirChance == 0)
            {
                switch (currDirection)
                {
                    case Direction.NE:
                        currDirection = Direction.SE;
                        WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
                        break;

                    case Direction.NW:
                        currDirection = Direction.NE;
                        WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNE();
                        break;

                    case Direction.SW:
                        currDirection = Direction.NW;
                        WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNW();
                        break;

                    case Direction.SE:
                        currDirection = Direction.SW;
                        WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSW();
                        break;

                    default:
                        break;
                }
            }

            int speed = 4 * SpriteUtil.SCALE_FACTOR;
            Point newPos = EntityUtils.Offset(CurrentLoc.Location, currDirection, speed);
            if (!Collision.WillHitBlock(new Rectangle(newPos, CurrentLoc.Size))) CurrentLoc = new Rectangle(newPos, CurrentLoc.Size);

        }

        public void Attack() { }

        public void TakeDamage(DamageLevel level)
        {
            if (damageCountdown == 0)
            {
                if (level == DamageLevel.Boomerang) 
                {
                    stunCountdown = SpriteUtil.BOOM_STUN_LENGTH;
                }
                else
                {
                    currentHealth -= (int)level;
                }
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
                SoundManager.Instance.PlaySound("EnemyZapped");
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }
            WallMasterSprite.Damaged = true;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            WallMasterSprite.Draw(spriteBatch, CurrentLoc);
        }

        private static readonly int MOVE_TIMER = 8;
        private bool WillMove => currentFrame % MOVE_TIMER == 0 && stunCountdown == 0;
        public void Update()
        {
            if (stunCountdown > 0) { stunCountdown--; }
            if (WallMasterSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    WallMasterSprite.Damaged = false;
                }
            }
            WallMasterSprite.Update();
            currentFrame++;
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