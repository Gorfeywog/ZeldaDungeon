using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Goomba : IEnemy
    {
        private ISprite GoombaSprite { get; set; }
        public bool ReadyToDespawn { get => currentHealth <= 0; }
        public bool IsFriendly { get => false; }

        public Rectangle CurrentLoc { get; set; }

        private int currentFrame;
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public EntityList roomEntities;
        private int currentHealth;
        private int damageCountdown = 0;
        private int stunCountdown = 0;
        private Room r;

        public Goomba(Point position, Room r)
        {
            this.r = r;
            GoombaSprite = EnemySpriteFactory.Instance.CreateGoombaSprite();
            int width = (int)SpriteUtil.SpriteSize.StalfosX;
            int height = (int)SpriteUtil.SpriteSize.StalfosY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            currentHealth = SpriteUtil.MEDIUM_MAX_HEALTH;
            currentFrame = 0;
            Collision = new CollisionHandler(r, this);
        }

        public void Move()
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

        public void Attack() { }
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
                SoundManager.Instance.PlaySound("EnemyZapped");
                damageCountdown = SpriteUtil.DAMAGE_DELAY;
            }
            GoombaSprite.Damaged = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GoombaSprite.Draw(spriteBatch, CurrentLoc);
        }

        private static readonly int MOVE_TIMER = 8;
        private bool WillMove => currentFrame % MOVE_TIMER == 0 && stunCountdown == 0;
        public void Update()
        {
            if (stunCountdown > 0) { stunCountdown--; }
            if (GoombaSprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    GoombaSprite.Damaged = false;
                }
            }
            GoombaSprite.Update();
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
            }
            else
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