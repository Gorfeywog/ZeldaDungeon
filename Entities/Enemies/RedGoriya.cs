using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class RedGoriya : IEnemy
	{
		public ISprite RedGoriyaSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;


		private enum GoriyaDirection { Left, Right, Up, Down };
		private GoriyaDirection currDirection;

		public RedGoriya(Point position)
		{
			RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
			currDirection = GoriyaDirection.Left;
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
						currDirection = GoriyaDirection.Left;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
						break;

					case 1:
						currDirection = GoriyaDirection.Right;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteRight();
						break;

					case 2:
						currDirection = GoriyaDirection.Up;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteUp();
						break;

					case 3:
						currDirection = GoriyaDirection.Down;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteDown();
						break;

					default:
						break;
				}
			}

			//Determines which way to move
			switch (currDirection)
			{
				case GoriyaDirection.Left:
					posX -= 4;
					break;

				case GoriyaDirection.Right:
					posX += 4;
					break;

				case GoriyaDirection.Up:
					posY -= 4;
					break;

				case GoriyaDirection.Down:
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
			RedGoriyaSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			RedGoriyaSprite.Update();
		}




	}
}