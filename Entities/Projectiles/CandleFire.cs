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
        private static int duration = 300;
        private int timer = duration; // counts down
        private ISprite sprite = BlockSpriteFactory.Instance.CreateFireBlock(); // it is probably bad that projectile sprites all over the place
        private Game1 g;
        private Direction d;
        private int speed = 2;
        public Point CurrentPoint { get; private set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public CandleFire(Point position, Direction d)
        {
            CurrentPoint = position;
            this.d = d;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void DespawnEffect() { }
        public void Update()
        {
            CurrentPoint = EntityUtils.Offset(CurrentPoint, d, speed);
            sprite.Update();
            timer--;
        }
    }
}
