using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon
{
    public class BowItem : IItem
    {
        private ISprite bow = createBow(); // TODO: Check with Luke that this is correct.
        private Rectangle destRect = new Rectangle(new Point(int 700, int 300)); // this might need to change. I based it off of paint, but it should be a little left of the middle.
        public Point CurrentPoint
        {
            get
            {
                return currentPoint;
            }
            set
            {
                currentPoint = value;

            }
        }
        public SpriteBatch Sprites
        {
            get
            {
                return sprites;
            }
            set
            {
                Sprites = value;
            }
        }
    public void Draw()
		{
			Nullable<Rectangle> sourceRect = new Rectangle(CurrentPoint, new System.Drawing.Size(int 32, int 32));
			bow.Draw(Sprites, destRect, sourceRect, Color.White);
		}
		public void UpdateSprite(SpriteBatch spriteBatch, Point topLeft)
		{
			CurrentPoint = topLeft;
            Sprites = spriteBatch;
		}
	}
}

