using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class RickAstley : IEnemy
    {
        public bool ReadyToDespawn { get => currentHealth <= 0; }
        private ISprite RickAstleySprite { get; set; }
        public Rectangle CurrentLoc { get; set; }
        public bool IsFriendly { get => false; }

        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        private int currentHealth;
        private int initX;
        private bool movingLeft;
        private int currentFrame;
        private Room r;
        private int damageCountdown = 0;
        private int stunCountdown = 0;


        public RickAstley(Point position, Room r)
        {
            RickAstleySprite = EnemySpriteFactory.Instance.CreateRickAstleySprite();
            int width = (int)SpriteUtil.SpriteSize.RickX;
            int height = (int)SpriteUtil.SpriteSize.RickY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            initX = position.X;
            currentHealth = SpriteUtil.ASTLEY_MAX_HEALTH;
            movingLeft = true;
            this.r = r;
            currentFrame = 0;
        }
        public void Move()
        {
            // Rick Astley is a singer, not a fighter.

        }

        public void Attack()
        {
            if (!WillAttack)
            {
                return;
            }

            int noteVel = 2 * SpriteUtil.SCALE_FACTOR;
            IProjectile noteN = new MusicNotes(CurrentLoc.Location, 0, -noteVel);
            IProjectile noteS = new MusicNotes(CurrentLoc.Location, 0, noteVel);
            IProjectile noteE = new MusicNotes(CurrentLoc.Location, noteVel, 0);
            IProjectile noteW = new MusicNotes(CurrentLoc.Location, -noteVel,0);
            IProjectile noteSE = new MusicNotes(CurrentLoc.Location, noteVel, noteVel);
            IProjectile noteSW = new MusicNotes(CurrentLoc.Location, -noteVel, noteVel);
            IProjectile noteNE = new MusicNotes(CurrentLoc.Location, noteVel, -noteVel);
            IProjectile noteNW = new MusicNotes(CurrentLoc.Location, -noteVel, -noteVel);
            r.RegisterEntity(noteN);
            r.RegisterEntity(noteS);
            r.RegisterEntity(noteE);
            r.RegisterEntity(noteW);
            r.RegisterEntity(noteSE);
            r.RegisterEntity(noteSW);
            r.RegisterEntity(noteNE);
            r.RegisterEntity(noteNW);
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
                SoundManager.Instance.PlaySound("MinecraftOof");
            }
            RickAstleySprite.Damaged = true;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            RickAstleySprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame % 2 == 0) RickAstleySprite.Update();
            if (stunCountdown > 0) { stunCountdown--; }
            if (RickAstleySprite.Damaged)
            {
                damageCountdown--;
                if (damageCountdown == 0)
                {
                    RickAstleySprite.Damaged = false;
                }
            }
            Collision.Update();

        }
        private static readonly int ATTACK_TIMER = 64;
        private static readonly int ATTACK_CHANCE = 4;
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