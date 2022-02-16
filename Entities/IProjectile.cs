using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IProjectile : IDrawable
    {
        public Point CurrentPoint { get; }
        public bool ReadyToDespawn { get; } // there is probably a smarter way to do this.
        public void DespawnEffect();
        // anything projectiles should do goes here, I guess?
    }
}
