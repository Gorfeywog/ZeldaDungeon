using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    
    public class SmokeCloud : IProjectile
    {
        private static int smokeTime = 56; // 7*8 for 7 stages of cloud
        private int timer = smokeTime; // counts down
        private ISprite sprite = EnemySpriteFactory.Instance.CreateCloudSprite();
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public SmokeCloud(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.CloudX;
			int height = (int)SpriteUtil.SpriteSize.CloudY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void DespawnEffect() { }
        public void Update()
        {
            sprite.Update();
            timer--;
        }
        public void OnHit(IEntity target) { }
    }
}
