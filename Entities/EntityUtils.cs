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
                default: 
                    throw new ArgumentException();
            }
            return new Point(newX, newY);
        }
        public static Direction OppositeOf(Direction dir)
        {
            return dir switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                Direction.NW => Direction.SE,
                Direction.SE => Direction.NW,
                Direction.NE => Direction.SW,
                Direction.SW => Direction.NE,
                _ => throw new ArgumentException()
            };
        }
        public static Point Interpolate(Point oldPoint, Point newPoint, int current, int total)
        {
            Point difference = newPoint - oldPoint;
            Point scaledDifference = new Point((difference.X * current)/total, (difference.Y * current)/total);
            return oldPoint + scaledDifference;
        }
    }
}
