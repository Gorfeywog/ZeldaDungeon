﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    // this should not be a projectile, but it's not really an enemy, item, or block either, let alone a link. add another interface? implement IEntiy directly?
    public class SmokeCloud : IProjectile
    {
        private static int smokeTime = 56; // 7*8 for 7 stages of cloud, 8 frames per?
        private int timer = smokeTime; // counts down
        private ISprite sprite = EnemySpriteFactory.Instance.CreateCloudSprite();
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public SmokeCloud(Point position)
        {
            CurrentLoc = new Rectangle(position, new Point(16, 16));
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
