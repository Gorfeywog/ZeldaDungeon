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
        private static int duration = 150;
        private int timer = duration; // counts down
        private ISprite sprite = BlockSpriteFactory.Instance.CreateFireBlock(); // it is probably bad that projectile sprites all over the place
        private Game1 g;
        private Direction d;
        private int speed = 2;
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public CandleFire(Point position, Direction d)
        {
            CurrentLoc = new Rectangle(position, new Point(16,16));
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
    }
}
