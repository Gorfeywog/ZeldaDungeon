using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Stalfos : IEnemy
	{
		public ISprite StalfosSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		private Random rand;
		private int currentFrame;

		public Stalfos(Point position)
		{
			StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
			CurrentLoc = new Rectangle(position, new Point(16, 16));
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			if (rand.Next(2) == 0)
            {
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8 * rand.Next(3) - 8, CurrentLoc.Y), CurrentLoc.Size); ;
			} else
			{
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8 * rand.Next(3) - 8), CurrentLoc.Size);
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
			StalfosSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			StalfosSprite.Update();
			currentFrame++;
			if (currentFrame % 8 == 0)
			{
				Move();
			}
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}