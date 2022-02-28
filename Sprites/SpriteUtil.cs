using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    class SpriteUtil
    {
        public static readonly int SCALE_FACTOR = 2; // Scale factor of destination rectangles 

        public static readonly int WAIT_TIME = 8; // how many Updates to wait between cycling frame

        private static readonly int gridX = 32;
        private static readonly int gridY = 32;
        public static Rectangle GridToRectangle(int x, int y, int width, int height) // convert grid position to position in pixels
        {
            return new Rectangle(gridX * x, gridY * y, width, height);
        }

    }
}
