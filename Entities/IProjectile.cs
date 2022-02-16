using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IProjectile : IDrawable
    {
        public bool CanHurtPlayer{ get; }
        public bool CanHurtEnemies { get; }
        // anything projectiles should do goes here, I guess?
    }
}
