using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    // entities pass over blocks of lower collision levels, but collide with ones of the same or higher collision level
    // e.g. floor blocks are level 0 and pushable blocks are level 1, so link (level 1) walks over floors but not pushable blocks
    // whereas a keese (level 2) flies over both of those, but not through blocks of level 2
    public enum CollisionHeight
    {
        Floor = 0,
        Normal = 1,
        High = 2,
        Ghost = 3
    }
}
