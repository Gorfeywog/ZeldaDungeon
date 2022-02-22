﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    // this should not be a projectile, but it's not really an enemy, item, or block either, let alone a link. add another interface? implement IEntiy directly?
    public class HitEffect : IProjectile
    {
        private static int effectTime = 56;
        private int timer = effectTime; // counts down
        private ISprite sprite = EnemySpriteFactory.Instance.CreateHitEffectSprite();
        public Point CurrentPoint { get; private set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        public HitEffect(Point position)
        {
            CurrentPoint = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void DespawnEffect() { }
        public void Update()
        {
            sprite.Update();
            timer--;
        }
    }
}