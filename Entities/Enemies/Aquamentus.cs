using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Aquamentus : IEnemy
	{
		public ISprite AquamentusSprite { get; set; }

		private int posX;
		private int posY;
		private Random rand;
		private bool movingLeft;
		private int currentFrame;
		private Game1 g;


		public Aquamentus(Point position, Game1 g)
		{
			AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
			posX = position.X;
			posY = position.Y;
			rand = new Random();
			movingLeft = true;
			this.g = g;
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
			IProjectile fireballUp = new Fireball(new Point(posX, posY), -4, 1 + fireballChange);
			IProjectile fireballStraight = new Fireball(new Point(posX, posY), -4, fireballChange);
			IProjectile fireballDown = new Fireball(new Point(posX, posY), -4, -1 + fireballChange);
			g.RegisterProjectile(fireballUp);
			g.RegisterProjectile(fireballStraight);
			g.RegisterProjectile(fireballDown);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			AquamentusSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			currentFrame++;
			AquamentusSprite.Update();
			if (currentFrame % 8 == 0)
            {
				Move();
            }
			if (currentFrame % 64 == 0 && rand.Next(4) == 0)
            {
				Attack();
            }

		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}