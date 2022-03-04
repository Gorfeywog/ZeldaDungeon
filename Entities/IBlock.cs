using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IBlock : IEntity
    {
        public CollisionHeight Height { get; }
    }
}s
