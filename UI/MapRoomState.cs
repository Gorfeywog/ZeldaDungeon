﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    class MapRoomState
    {
        public bool HasLink { get; private set; }
        public bool HasTriforce { get; private set; }
        public bool RoomKnown { get; private set; }
        public MapRoomState(bool hasLink, bool hasTriforce, bool roomKnown)
        {
            HasLink = hasLink;
            HasTriforce = hasTriforce;
            RoomKnown = roomKnown;
        }
    }
}
