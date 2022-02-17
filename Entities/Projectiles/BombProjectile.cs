using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    public class BombProjectile : IProjectile
    {
        private static int fuseTime = 300; // chosen with no methodology
        private static int smokeTime = 56; // 7*8 for 7 stages of cloud, 8 frames per?
        private bool isCloud = false;
        private int timer = fuseTime; // counts down
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBomb(); // should projectiles be on their own spritesheet (and thus sprite factory)?
        private Game1 g;
        public Point CurrentPoint { get; private set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public BombProjectile(Point position, Game1 g)
        {
            CurrentPoint = position;
            this.g = g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void DespawnEffect()
        {
            // spawn a smoke cloud? damage things in a radius? whatever bombs do.
        }
        public void Update()
        {
            timer--;
            if (timer == 0 && !isCloud) // this is kinda hacky but should work for now
            {
                isCloud = true;
                timer = smokeTime;
                sprite = EnemySpriteFactory.Instance.CreateCloudSprite();
            }
        }
    }
}
