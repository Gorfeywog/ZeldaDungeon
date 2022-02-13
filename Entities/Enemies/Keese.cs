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

		public Keese(Point position)
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			posX = position.X;
			posY = position.Y;
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

		public void Update()
		{
			KeeseSprite.Update();
		}




	}
}