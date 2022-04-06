using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class Trap : IEnemy
    {
        public ISprite TrapSprite { get; set; }
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn => false;


        private Point initPoint;
        private Direction dir;
        private int speed;
        private int cooldown;
        private const int MaxCooldown = 20;
        private Room r;
        private Game1 g;
        private bool attacking;
        private bool moving;
        private bool onCooldown;

        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public Trap(Point position, Room r, Game1 g)
        {
            TrapSprite = EnemySpriteFactory.Instance.CreateTrapSprite();
            int width = (int)SpriteUtil.SpriteSize.TrapX;
            int height = (int)SpriteUtil.SpriteSize.TrapY;
            initPoint = position;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            speed = 2 * SpriteUtil.SCALE_FACTOR;
            this.g = g;
            attacking = false;
            moving = false;
            onCooldown = false;
            cooldown = 0;

        }

        public void Move()
        {
            if (!moving) { return; }
            Point newPt;
            if (attacking)
            {
                newPt = EntityUtils.Offset(CurrentLoc.Location, dir, speed);
                if (!Collision.WillHitBlock(new Rectangle(newPt, CurrentLoc.Size)))
                {
                    CurrentLoc = new Rectangle(newPt, CurrentLoc.Size);
                }
                else
                {
                    attacking = false;
                }


            }
            else
            {
                newPt = EntityUtils.Offset(CurrentLoc.Location, dir, -SpriteUtil.SCALE_FACTOR);
                if (CurrentLoc.Location != initPoint)
                {
                    CurrentLoc = new Rectangle(newPt, CurrentLoc.Size);
                }
                else
                {
                    moving = false;
                    onCooldown = true;
                }
            }

        }
        public void Attack() { }
        public void SpecialTrapAttack()
        {
            if (!attacking && !moving && !onCooldown)
            {
                dir = Collision.DetectDirection(g.Player);
                attacking = true;
                moving = true;
            }
        }

        public void TakeDamage()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TrapSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            TrapSprite.Update();
            if (onCooldown)
            {
                cooldown--;
            }
            if (cooldown <= 0)
            {
                cooldown = MaxCooldown;
                onCooldown = false;
            }
            Collision.Update();
        }
        public void DespawnEffect() { }
    }
}