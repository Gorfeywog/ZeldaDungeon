using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.MiscEffects
{
    
    public class EnemyDeath : IEntity
    {
        private static int effectTime = 30;
        private int timer = effectTime; // counts down
        private ISprite sprite = EnemySpriteFactory.Instance.CreateEnemyDeathSprite();
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public EnemyDeath(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.EnemyDeathX;
            int height = (int)SpriteUtil.SpriteSize.EnemyDeathY;
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
