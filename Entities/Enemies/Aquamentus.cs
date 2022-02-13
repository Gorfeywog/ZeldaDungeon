using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Aquamentus : IEnemy
	{
		public ISprite AquamentusSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;
		private bool movingLeft;

		public Aquamentus(Point position)
		{
			AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
			posX = position.X;
			posY = position.Y;
			rand = new Random();
			movingLeft = true;
		}

		public void Move()
		{
			//One in four chance per move for aquamentus to change direction
			if (rand.Next(4) == 0)
            {
				movingLeft = !movingLeft;
            }

			if (movingLeft)
            {
				posX -= 4;
			} else
            {
				posX += 4;
            }

		}

		public void Attack()
		{
			//TODO Need to add fireball projectiles to spriteSheet
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			AquamentusSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void UpdateSprite()
		{
			AquamentusSprite.Update();
		}




	}
}