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

		public Gel(Point position)
		{
			GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
			posX = position.X;
			posY = position.Y;
			rand = new Random();
		}

		public void Move()
		{
			if (rand.Next(2) == 0)
			{
				posX += 4 * rand.Next(3) - 4;
			}
			else
			{
				posY += 4 * rand.Next(3) - 4;
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
		}




	}
}