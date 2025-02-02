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
	public class ArrowPickup : IPickup
	{
        private ISprite sprite;
        private Game1 g;
        private bool isMagic;
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        public bool HoldsUp { get => false; }
        public ArrowPickup(Point position, Game1 g, bool isMagic)
        {
            int width = (int)SpriteUtil.SpriteSize.ArrowWidth;
			int height = (int)SpriteUtil.SpriteSize.ArrowLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g = g;
            this.isMagic = isMagic;
            sprite = ItemSpriteFactory.Instance.CreateArrow(Direction.Up, isMagic);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void PickUp(ILink link) 
        {
            link.AddItem(new ArrowItem(g, isMagic));
        }
        public void Update() => sprite.Update();

        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

