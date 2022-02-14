using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Gel : IEnemy
	{
		public ISprite GelSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;
		private int currentFrame;

		public Gel(Point position)
		{
			GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
			posX = position.X;
			posY = position.Y;
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			int movingNum = rand.Next(5);
			if (movingNum < 2)
			{
				posX += 8 * rand.Next(3) - 8;
			}
			else if (movingNum > 2)
			{
				posY += 8 * rand.Next(3) - 8;
			}

		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			GelSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			GelSprite.Update();
			currentFrame++;
			if (currentFrame % 8 == 0)
			{
				this.Move();
			}
		}




	}
}