using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    
    public class HitEffect : IProjectile
    {
        private static int effectTime = 56;
        private int timer = effectTime; // counts down
        private ISprite sprite = EnemySpriteFactory.Instance.CreateHitEffectSprite();
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public HitEffect(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.HitEffectX;
            int height = (int)SpriteUtil.SpriteSize.HitEffectY;
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
