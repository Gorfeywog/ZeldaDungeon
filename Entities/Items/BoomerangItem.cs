﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Items
{
	public class BoomerangItem : IItem
	{
        private ISprite sprite;
        private bool isMagic;
        private Game1 g;
        public Rectangle CurrentLoc { get; set; }
        public BoomerangItem(Point position, Game1 g, bool isMagic)
        {
            int width = (int)SpriteUtil.SpriteSize.BoomerangX;
			int height = (int)SpriteUtil.SpriteSize.BoomerangY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g = g;
            this.isMagic = isMagic;
            if (isMagic)
            {
                sprite = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            }
            else
            {
                sprite = EnemySpriteFactory.Instance.CreateStaticBoomerangSprite();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();

        private static int offset = 32;
        public void UseOn(ILink player)
        {
        //    Point loc = EntityUtils.Offset(player.Center, player.Direction, offset);
            IProjectile proj = new Boomerang(player.Center, player.Direction, isMagic);
            g.RegisterProjectile(proj);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

