using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Fireball : IEnemy
	{
		public ISprite FireballSprite { get; set; }
		private int posX;
		private int posY;
		private int xChange;
		private int yChange;
		private Random rand;
		private int currentFrame;


		public Fireball(Point position, int xChange, int yChange)
		{
			FireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite();
			posX = position.X;
			posY = position.Y;
			this.xChange = xChange;
			this.yChange = yChange;
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			posX += xChange;
			posY += yChange;

		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			FireballSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			currentFrame++;

			this.Move();
			FireballSprite.Update();
		}




	}
}