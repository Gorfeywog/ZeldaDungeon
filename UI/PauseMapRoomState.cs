using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    class PauseMapRoomState
    {
        public bool RoomKnown { get; private set; }
        private ICollection<Direction> doorDirections; 
        public PauseMapRoomState(bool roomKnown, ICollection<Direction> doorDirections)
        {
            RoomKnown = roomKnown;
            this.doorDirections = new HashSet<Direction>(doorDirections);
        }
        public bool HasDoor(Direction d) => doorDirections.Contains(d);
        public override string ToString()
        {
            string dirString = "";
            foreach (var d in doorDirections) { dirString += d; }
            return "room known: " + RoomKnown + "\ndir: " + dirString;
        }
    }
}
