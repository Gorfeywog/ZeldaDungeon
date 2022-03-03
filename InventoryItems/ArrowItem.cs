using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Items
{
	public class ArrowItem : IItem
	{
        private Game1 g;
        public bool Consumable { get => true; }
        public ArrowItem(Game1 g)
        {
            this.g=g;
        }
        private static int offset = 32;
        public void UseOn(ILink player)
        {
            Point loc = EntityUtils.Offset(player.Center, player.Direction, offset);
            IProjectile proj = new ArrowProjectile(loc, player.Direction, g);
            g.RegisterProjectile(proj);
        }
        public bool Equals(IItem other)
        {
            if (other is ArrowItem otherArrow)
            {
                return this.g == otherArrow.g;
            }
            else
            {
                return false;
            }
        }
    }
}

