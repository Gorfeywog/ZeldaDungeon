using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    public class CandleFire : IProjectile
    {
        private static int duration = 60;
        private int timer = duration; // counts down
        private ISprite sprite = BlockSpriteFactory.Instance.CreateFireBlock(); 
        private Game1 g;
        private Direction d;
        private int speed = 2 * SpriteUtil.SCALE_FACTOR;
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public CandleFire(Point position, Direction d)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.d = d;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void DespawnEffect() { }
        public void Update()
        {
            if (timer > duration / 3) // it stops moving after travelling a while
            {
                CurrentLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, d, speed), CurrentLoc.Size);
            }
            sprite.Update();
            timer--;
        }
        public void OnHit(IEntity target)
        {
            if (target is IEnemy en)
            {
                en.TakeDamage();
            }
            else if (target is ILink link)
            {
                link.TakeDamage();
            }
        }
    }
}
