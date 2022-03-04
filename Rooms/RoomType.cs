using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Rooms
{
    // having only 2 types in an enum is kinda weird, but feels more extensible than a bool
    // numbers are directly associated for easier parsing from csv files, not just arbitrarily!
    public enum RoomType
    {
        Normal = 0,
        Ladder = 1 
    }
}
