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

		public Aquamentus()
		{
			AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
			posX = 0;
			posY = 0;
			rand = new Random();
			movingLeft = true;
		}

		public void Move(SpriteBatch spriteBatch)
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

			AquamentusSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Attack()
		{
			//TODO Need to add fireball projectiles to spriteSheet
		}

		public void TakeDamage()
		{

		}




	}
}