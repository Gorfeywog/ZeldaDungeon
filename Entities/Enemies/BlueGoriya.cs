using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class BlueGoriya : IEnemy
	{
		public ISprite BlueGoriyaSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;


		private enum GoriyaDirection { Left, Right, Up, Down };
		private GoriyaDirection currDirection;

		public BlueGoriya()
		{
			BlueGoriyaSprite = EnemySpriteFactory.Instance.createBlueGoriyaSpriteLeft();
			currDirection = GoriyaDirection.Left;

			
			rand = new Random();

		}

		public void Move()
		{
			//One in four chance to change directions
			if (rand.Next(4) == 0) {
				switch (rand.Next(4))
				{
					case 0:
						currDirection = GoriyaDirection.Left;
						BlueGoriyaSprite = EnemySpriteFactory.Instance.createBlueGoriyaSpriteLeft();
						break;

					case 1:
						currDirection = GoriyaDirection.Right;
						BlueGoriyaSprite = EnemySpriteFactory.Instance.createBlueGoriyaSpriteRight();
						break;

					case 2:
						currDirection = GoriyaDirection.Up;
						BlueGoriyaSprite = EnemySpriteFactory.Instance.createBlueGoriyaSpriteUp();
						break;

					case 3:
						currDirection = GoriyaDirection.Down;
						BlueGoriyaSprite = EnemySpriteFactory.Instance.createBlueGoriyaSpriteDown();
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
			BlueGoriyaSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void UpdateSprite()
		{

		}




	}
}