using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    class SpriteUtil
    {
        public static readonly int SCALE_FACTOR = 5; // Scale factor of destination rectangles 
        public static readonly int WAIT_TIME = 8; // how many Updates to wait between cycling frame

        //Door positions
        public static readonly int X_POS_CENTER = 112;
        public static readonly int Y_POS_CENTER = 72;
        public static readonly int X_POS_LEFT = 0;
        public static readonly int X_POS_RIGHT = 224;
        public static readonly int Y_POS_TOP = 0;
        public static readonly int Y_POS_BOTTOM = 144;

        //Default max health values
        public static readonly int LINK_MAX_HEALTH = 6;
        public static readonly int AQUAMENTUS_MAX_HEALTH = 20;
        public static readonly int GENERIC_MAX_HEALTH = 2;

        //Default position of link
        public static readonly int LINK_DEFAULT_SPAWN = 32;

        //lengths of states
        public static readonly int LINK_PICKUP_TIME = 69;
        //Room sizes
        public static readonly int ROOM_WIDTH = 256;
        public static readonly int ROOM_HEIGHT = 176;

        //Global random number generator for use in all classes
        public static readonly Random Rand = new Random();

        //Length is correspondent to height, it is named length because items can
        //occasionally be used while in different directions, so X and Y would be confusing for these items
        public enum SpriteSize
        {
            OldManX = 16,
            OldManY = 16,
            DoorX = 32,
            DoorY = 32,
            LinkX = 16,
            LinkY = 16,
            GenericBlockX = 16,
            GenericBlockY = 16,
            AquamentusX = 24,
            AquamentusY = 32,
            KeeseX = 16,
            KeeseY = 10,
            FireballX = 8,
            FireballY = 10,
            GelX = 8,
            GelY = 9,
            RopeX = 15,
            RopeY = 15,
            StalfosX = 16,
            StalfosY = 16,
            TrapX = 16,
            TrapY = 16,
            GoriyaX = 14,
            GoriyaY = 16,
            BoomerangX = 5,
            BoomerangY = 8,
            WallMasterX = 16,
            WallMasterY = 16,
            CloudX = 16,
            CloudY = 16,
            HitEffectX = 8,
            HitEffectY = 8,
            BombWidth = 8,
            BombLength = 14,
            BowWidth = 8,
            BowLength = 16,
            ClockWidth = 11,
            ClockLength = 16,
            CompassWidth = 11,
            CompassLength = 12,
            FairyWidth = 8,
            FairyLength = 16,
            HeartContainerWidth = 13,
            HeartContainerLength = 13,
            HeartWidth = 7,
            HeartLength = 8,
            KeyWidth = 8,
            KeyLength = 16,
            MapWidth = 8,
            MapLength = 16,
            RupyWidth = 8,
            RupyLength = 16,
            TriforceWidth = 10,
            TriforceLength = 10,
            ArrowWidth = 5,
            ArrowLength = 16,
            SwordWidth = 8,
            SwordLength = 16,
            SwordProjWidth = 8,
            SwordProjHeight = 10,
            CandleWidth = 8,
            CandleLength = 16
        } 

        private static readonly int gridX = 40;
        private static readonly int gridY = 40;
        public static Rectangle GridToRectangle(int x, int y, int width, int height) // convert grid position to position in pixels
        {
            return new Rectangle(gridX * x, gridY * y, width, height);
        }

    }
}
