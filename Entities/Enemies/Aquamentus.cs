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
		public Rectangle CurrentLoc { get; set; }

		private Random rand;
		private bool movingLeft;
		private int currentFrame;
		private Game1 g;


		public Aquamentus(Point position, Game1 g)
		{
			AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
			int width = (int)SpriteUtil.SpriteSize.AquamentusX;
			int height = (int)SpriteUtil.SpriteSize.AquamentusY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));

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
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X - 8, CurrentLoc.Y), CurrentLoc.Size);
			} else
            {
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8, CurrentLoc.Y), CurrentLoc.Size);
			}

		}

		public void Attack()
		{
			int fireballChange = rand.Next(3) - 1;
			int fireballVel = -4;
			IProjectile fireballUp = new Fireball(CurrentLoc.Location, fireballVel, 1 + fireballChange);
			IProjectile fireballStraight = new Fireball(CurrentLoc.Location, fireballVel, fireballChange);
			IProjectile fireballDown = new Fireball(CurrentLoc.Location, fireballVel, -1 + fireballChange);
			g.RegisterProjectile(fireballUp);
			g.RegisterProjectile(fireballStraight);
			g.RegisterProjectile(fireballDown);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			AquamentusSprite.Draw(spriteBatch, CurrentLoc);
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