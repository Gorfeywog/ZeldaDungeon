using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Keese : IEnemy
	{
		public ISprite KeeseSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;

		public Keese()
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			posX = 0;
			posY = 0;
			rand = new Random();
		}

		public void Move()
		{
			posX += 4 * rand.Next(3) - 4;
			posY += 4 * rand.Next(3) - 4;

		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			KeeseSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void UpdateSprite()
		{

		}




	}
}