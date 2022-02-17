using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class Fireball : IProjectile
	{
		public ISprite FireballSprite { get; private set; }
		public Point CurrentPoint { get; private set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private static int maxFrame = 400; // chosen arbitrarily
		private int xChange;
		private int yChange;
		private Random rand;
		private int currentFrame;


		public Fireball(Point position, int xChange, int yChange)
		{
			FireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite(); // note that it lives on the enemies sheet
			CurrentPoint = position;
			this.xChange = xChange;
			this.yChange = yChange;
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			int newX = CurrentPoint.X + xChange;
			int newY = CurrentPoint.Y + yChange;
			CurrentPoint = new Point(newX, newY);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			FireballSprite.Draw(spriteBatch, CurrentPoint);
		}

		public void Update()
		{
			currentFrame++;
			this.Move();
			FireballSprite.Update();
		}
		public void DespawnEffect() { } // don't have anything special to 



	}
}