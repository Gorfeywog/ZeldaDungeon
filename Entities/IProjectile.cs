using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IProjectile : IEntity
    {
        public void OnHit(IEntity target);
    }
}
