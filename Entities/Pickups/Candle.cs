﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.Entities.Pickups
{
	public class Candle : IPickup
	{
        private ISprite sprite;
        private Game1 g;
        private bool isRed; 
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        public bool HoldsUp { get => true; }
        public Candle(Point position, Game1 g, bool isRed)
        {
            int width = (int)SpriteUtil.SpriteSize.CandleWidth;
			int height = (int)SpriteUtil.SpriteSize.CandleLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g = g;
            this.isRed = isRed;
            sprite = ItemSpriteFactory.Instance.CreateCandle(isRed);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void PickUp(ILink player)
        {
            player.AddItem(new CandleItem(g, isRed));
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

