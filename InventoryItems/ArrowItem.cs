﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    public class ArrowItem : IItem
	{
        private Game1 g;
        private bool isMagic;
        public bool Consumable { get => true; }
        public ArrowItem(Game1 g, bool isMagic)
        {
            this.g = g;
            this.isMagic = isMagic;
        }
        private static int offset = 32;
        public void UseOn(ILink player)
        {
            Point loc = EntityUtils.Offset(player.Center, player.Direction, offset);
            IProjectile proj;
            if (isMagic)
            {
                proj = new MagicArrowProjectile(loc, player.Direction, g);
            }
            else
            {
                proj = new ArrowProjectile(loc, player.Direction, g);
            }
            g.RegisterProjectile(proj);
        }
        public bool CanUseOn(ILink player) => player.HasItem(new BowItem());
        public bool Equals(IItem other)
        {
            if (other is ArrowItem otherArrow)
            {
                return this.g == otherArrow.g && (this.isMagic == otherArrow.isMagic);
            }
            else
            {
                return false;
            }
        }
    }
}

