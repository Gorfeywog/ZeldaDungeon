using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Rooms
{
    public enum DoorState
    {
        None,Open,Locked,Closed,Hole // TODO - add a state for "no hole, but can be blown up"
    }
}
