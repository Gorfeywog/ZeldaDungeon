using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IProjectile : IEntity
    {
        public Point CurrentPoint { get; }

        // add properties relating to what "team" they're on?
        // as in, whether they hurt Link, hurt enemies, don't do damage?
    }
}
