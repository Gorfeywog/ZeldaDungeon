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
    public class BoomerangItem : IItem
	{
        public bool Consumable { get => true; }
        private bool isMagic;
        private Game1 g;
        public BoomerangItem(Game1 g, bool isMagic)
        {

            this.g = g;
            this.isMagic = isMagic;
        }
        private static int offset = 32;
        public void UseOn(ILink player)
        {
            Point loc = EntityUtils.Offset(player.Center, player.Direction, offset);
            IProjectile proj = new Boomerang(player, player.Direction, isMagic, g);
            g.RegisterProjectile(proj);
        }

        public bool CanUseOn(ILink player) => true;
        public bool Equals(IItem other)
        {
            if (other is BoomerangItem otherBoom)
            {
                return this.g == otherBoom.g && (this.isMagic == otherBoom.isMagic);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return "boomerang".GetHashCode() ^ g.GetHashCode() ^ isMagic.GetHashCode();
        }
    }
}

