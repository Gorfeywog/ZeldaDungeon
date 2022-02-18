using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class ArrowProjectile : IProjectile
	{
		private ISprite ArrowSprite { get; set; }
		public Point CurrentPoint { get; private set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private Direction orientation;
		private static int maxFrame = 400; // chosen arbitrarily
		private Random rand;
		private int currentFrame;
		private int speed = 5;


		public ArrowProjectile(Point position, Direction dir)
		{
			ArrowSprite = ItemSpriteFactory.Instance.CreateArrow(dir); // note that it lives on the enemies sheet
			CurrentPoint = position;
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			int newX = CurrentPoint.X;
			int newY = CurrentPoint.Y;
			switch (orientation)
            {
				case Direction.Up:
					newY += speed;
					break;
				case Direction.Down:
					newY -= speed;
					break;
				case Direction.Left:
					newX -= speed;
					break;
				case Direction.Right:
					newX += speed;
					break;
            }
			CurrentPoint = new Point(newX, newY);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ArrowSprite.Draw(spriteBatch, CurrentPoint);
		}

		public void Update()
		{
			currentFrame++;
			this.Move();
			ArrowSprite.Update();
		}
		public void DespawnEffect() { }
	}
}