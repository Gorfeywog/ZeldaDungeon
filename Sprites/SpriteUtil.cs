using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    class SpriteUtil
    {
        public static readonly int SCALE_FACTOR = 4; // Scale factor of destination rectangles 
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
        public static readonly int BOWSER_MAX_HEALTH = 20;
        public static readonly int SMALL_MAX_HEALTH = 2;
        public static readonly int MEDIUM_MAX_HEALTH = 5;
        public static readonly int LARGE_MAX_HEALTH = 7;

        // chances of drops
        public static readonly int GENERIC_RUPEE_ROLL_CAP = 11;
        public static readonly int GENERIC_RUPEE_THRESHOLD = 6;
        public static readonly int GENERIC_5_RUPEE_THRESHOLD = 9;
        public static readonly int MAGIC_ARROW_THRESHOLD = 2;
        public static readonly int BOMB_THRESHOLD = 7;
        public static readonly int HEART_THRESHOLD = 8;

        //Default position of link
        public static readonly int LINK_DEFAULT_SPAWN = 32;

        //lengths of states
        public static readonly int LINK_PICKUP_TIME = 69;
        //Room sizes
        public static readonly int ROOM_WIDTH = 256;
        public static readonly int ROOM_HEIGHT = 176;


        public static readonly int DAMAGE_DELAY = 80;
        public static readonly int BOOM_STUN_LENGTH = 160;

        //HUD sizes
        public static readonly int HUD_WIDTH = 256;
        public static readonly int HUD_HEIGHT = 56;

        // Main Menu sizes
        public static readonly int MENU_WIDTH = 256;
        public static readonly int MENU_HEIGHT = 232;
        //Inventory sizes
        public static readonly int INVENTORY_WIDTH = 256;
        public static readonly int INVENTORY_HEIGHT = 88;

        // Offset between each item in pause menu's inventory
        public static readonly int PAUSE_ITEM_OFFSET_X = 72;
        public static readonly int PAUSE_ITEM_OFFSET_Y = 48;
        public static readonly int PAUSE_ITEM_Y_GAP = 30;

        // static positions for items in inventory
        public static readonly int ACTIVE_ITEM_X = 61;
        public static readonly int ACTIVE_ITEM_Y = 45;
        public static readonly int MAP_POS_X = 82;
        public static readonly int MAP_POS_Y = -60;
        public static readonly int COMPASS_POS_X = 82;
        public static readonly int COMPASS_POS_Y = -100;
        


        //Map sizes
        public static readonly int MAP_WIDTH = 256;
        public static readonly int MAP_HEIGHT = 88;

        //Global random number generator for use in all classes
        public static readonly Random Rand = new Random();

        //Length is correspondent to height, it is named length because items can
        //occasionally be used while in different directions, so X and Y would be confusing for these items
        public enum SpriteSize
        {
            KoopaX = 15,
            KoopaY = 23,
            GameNWatchX = 21,
            GameNWatchY = 21,
            RickX = 0,
            RickY = 0,
            BowserX = 32,
            BowserY = 32,
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
            RupeeWidth = 8,
            RupeeLength = 16,
            TriforceWidth = 10,
            TriforceLength = 10,
            ArrowWidth = 5,
            ArrowLength = 16,
            SwordWidth = 8,
            SwordLength = 16,
            SwordProjWidth = 8,
            SwordProjHeight = 10,
            CandleWidth = 8,
            CandleLength = 16,
            HUDNumberWidth = 8,
            HUDNumberHeight = 8,
            HUDHeartHeight = 8,
            HUDHeartWidth = 8,
            BigMapRoomHeight = 8,
            BigMapRoomWidth = 8,
            SmallMapRoomHeight = 4,
            SmallMapRoomWidth = 8,
            SmallMapIndicatorHeight = 3,
            SmallMapIndicatorWidth = 5,
            SmallMapIndicOffsetX = 2,
            SmallMapIndicOffsetY = 0,
            EnemyDeathX = 15,
            EnemyDeathY = 16,
            HUDItemWidth = 8,
            SelectWidth = 8,
            SelectLength = 16

        } 

        private static readonly int gridX = 40;
        private static readonly int gridY = 40;
        public static Rectangle GridToRectangle(int x, int y, int width, int height) // convert grid position to position in pixels
        {
            return new Rectangle(gridX * x, gridY * y, width, height);
        }

    }
}
