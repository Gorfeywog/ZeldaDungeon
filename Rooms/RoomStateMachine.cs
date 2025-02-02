using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Rooms
{
    public class RoomStateMachine
    {
        private int resetTimer = 0;
        private static readonly int clockTime = 600; 

        public RoomState State { get; private set; }
        public RoomStateMachine()
        {
            State = RoomState.Normal;
        }
        public void Update()
        {
            if (resetTimer > 0)
            {
                resetTimer--;
                if (resetTimer == 0)
                {
                    State = RoomState.Normal;
                }
            }
        }
        public void PickUp()
        {
            State = RoomState.PickUp;
            resetTimer = SpriteUtil.LINK_PICKUP_TIME;
        }
        public void UseClock()
        {
            State = RoomState.Clock;
            resetTimer = clockTime;
        }
        public void EnterEffects() { }
        public void ExitEffects()
        {
            if (State == RoomState.Clock)
            {
                State = RoomState.Normal;
            }
        }
    }
    public enum RoomState 
    {
        Normal, PickUp, Clock
    }
}
