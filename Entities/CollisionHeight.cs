using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    // entities pass over blocks of lower collision levels, but collide with ones of the same or higher collision level
    // e.g. floor blocks are level floor and pushable blocks are level normal, so link (level normal) walks over floors but not pushable blocks
    public enum CollisionHeight
    {
        Floor = 0,
        Normal = 10,
        High = 20,
        Ghost = 30
    }
}
