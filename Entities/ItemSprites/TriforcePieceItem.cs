using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZeldaDungeon.Entities.ItemSprites
{
	public class TriforcePieceItem : IItem
	{
		
		private ISprite triforce = createTriforcePiece(); // TODO: Check with Luke that this is correct.
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
		public void Draw()
		{
			Nullable<Rectangle> sourceRect = new Rectangle(CurrentPoint, new Size(32, 32));
			triforce.Draw(Sprites, destRect, sourceRect, Color.White);
		}
		public void UpdateSprite(SpriteBatch spriteBatch, Point topLeft)
		{
			Sprites = spriteBatch;
			CurrentPoint = topLeft;
		}	
	}
}

