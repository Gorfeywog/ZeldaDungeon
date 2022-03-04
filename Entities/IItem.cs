using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IItem : IEntity {
        public void UseOn(ILink player);
    }
}
