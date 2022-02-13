using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Rope : IEnemy
	{
		public ISprite RopeSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;


		private enum RopeDirection { Left, Right, Up, Down };
		private RopeDirection currDirection;

		public Rope(Point position)
		{
			RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
			currDirection = RopeDirection.Left;

			posX = position.X;
			posY = position.Y;

			rand = new Random();

		}

		public void Move()
		{
			//One in four chance to change directions
			if (rand.Next(4) == 0)
			{
				switch (rand.Next(4))
				{
					case 0:
						currDirection = RopeDirection.Left;
						RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
						break;

					case 1:
						currDirection = RopeDirection.Right;
						RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteRight();
						break;

					case 2:
						currDirection = RopeDirection.Up;
						break;

					case 3:
						currDirection = RopeDirection.Down;
						break;

					default:
						break;
				}
			}

			//Determines which way to move
			switch (currDirection)
			{
				case RopeDirection.Left:
					posX -= 4;
					break;

				case RopeDirection.Right:
					posX += 4;
					break;

				case RopeDirection.Up:
					posY -= 4;
					break;

				case RopeDirection.Down:
					posX += 4;
					break;

				default:
					break;
			}

		}

		public void Attack()
		{
			//TODO Need to add boomerang to spriteSheet
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			RopeSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			RopeSprite.Update();
		}




	}
}