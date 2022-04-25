using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.MiscEffects
{
    
    public class SmokeCloud : IEntity
    {
        private static int smokeTime = 56; 
        private int timer = smokeTime; 
        private ISprite sprite = EnemySpriteFactory.Instance.CreateCloudSprite();
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
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
    }
}
