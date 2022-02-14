using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Aquamentus : IEnemy
	{
		public ISprite AquamentusSprite { get; set; }

		private List<IEnemy> fireballs;
		private int posX;
		private int posY;
		private Random rand;
		private bool movingLeft;
		private int currentFrame;



		public Aquamentus(Point position)
		{
			AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
			posX = position.X;
			posY = position.Y;
			rand = new Random();
			movingLeft = true;
			fireballs = new List<IEnemy>();
			currentFrame = 0;
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
				posX -= 8;
			} else
            {
				posX += 8;
            }

		}

		public void Attack()
		{
			int fireballChange = rand.Next(3) - 1;
			IEnemy fireballUp = new Fireball(new Point(posX, posY), -4, 1 + fireballChange);
			IEnemy fireballStraight = new Fireball(new Point(posX, posY), -4, fireballChange);
			IEnemy fireballDown = new Fireball(new Point(posX, posY), -4, -1 + fireballChange);
			fireballs.Add(fireballUp);
			fireballs.Add(fireballStraight);
			fireballs.Add(fireballDown);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			AquamentusSprite.Draw(spriteBatch, new Point(posX, posY));
			foreach (IEnemy fireball in fireballs) {
				fireball.Draw(spriteBatch);
            }

		}

		public void Update()
		{
			currentFrame++;
			AquamentusSprite.Update();
			if (currentFrame % 8 == 0)
            {
				this.Move();
            }
			if (currentFrame % 64 == 0 && rand.Next(4) == 0)
            {
				this.Attack();
            }

			foreach (IEnemy fireball in fireballs)
			{
				fireball.Update();
			}
		}




	}
}