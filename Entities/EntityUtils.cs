using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public static class EntityUtils
    {
        public static Point Offset(Point p, Direction d, int amt)
        {
            return Offset(p, d, amt, amt);
        }
        public static Point Offset(Point p, Direction d, int amtX, int amtY)
        {
            int newX = p.X;
            int newY = p.Y;
            switch (d)
            {
                case Direction.Up:
                    newY -= amtY;
                    break;
                case Direction.Down:
                    newY += amtY;
                    break;
                case Direction.Left:
                    newX -= amtX;
                    break;
                case Direction.Right:
                    newX += amtX;
                    break;
                case Direction.NW:
                    newX -= amtX;
                    newY -= amtY;
                    break;
                case Direction.NE:
                    newX += amtX;
                    newY -= amtY;
                    break;
                case Direction.SW:
                    newX -= amtX;
                    newY += amtY;
                    break;
                case Direction.SE:
                    newX += amtX;
                    newY += amtY;
                    break;
                default: // would be smart to support NW, etc
                    throw new ArgumentException();
            }
            return new Point(newX, newY);
        }
    }
}
