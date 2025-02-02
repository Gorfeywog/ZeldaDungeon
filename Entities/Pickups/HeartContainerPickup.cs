﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Pickups
{
    public class HeartContainerPickup : IPickup
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateHeartContainer();
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        public bool HoldsUp { get => true; }
        public HeartContainerPickup(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.HeartContainerWidth;
			int height = (int)SpriteUtil.SpriteSize.HeartContainerLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void PickUp(ILink player)
        {
            player.UseHeartContainer();
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
