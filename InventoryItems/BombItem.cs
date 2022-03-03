﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Items
{
    public class BombItem : IItem
    {
        private Game1 g;
        public BombItem(Game1 g)
        {

            this.g = g;
        }

        private static int offset = 32; // how far to place from Link
        public void UseOn(ILink player)
        {
            // uses player.Position rather than player.Center since is about the size of Link
            Point loc = EntityUtils.Offset(player.CurrentLoc.Location, player.Direction, offset);
            IProjectile proj = new BombProjectile(loc, g);
            g.RegisterProjectile(proj);
        }
    }
}

