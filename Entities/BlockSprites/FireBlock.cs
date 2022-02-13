using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZeldaDungeon.Entities.BlockSprites
{
	public class FireBlock : IBlock
	{
		private int i = 0;
		private Rectangle destRect = new Rectangle(new Point(700, 300), new Size(32, 32)); // this might need to change. I based it off of paint, but it should be a little left of the middle.
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

		ISprite fire = createFire();

		public void Draw()
		{
			Nullable<Rectangle> sourceRect = new Rectangle(CurrentPoint, new Size(32, 32));
			fire.Draw(Sprites, destRect, sourceRect, Color.White);
		}
		public void UpdateSprite(SpriteBatch spriteBatch, Point topLeft)
		{
			Sprites = spriteBatch;
			i++;
			if (i % 4 == 0)
			{
				CurrentPoint = topLeft;
			}
			else
			{
				CurrentPoint = new Point(topLeft.X + 32, topLeft.Y);
			}
		}
	}
}

