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
		public Rectangle CurrentLoc { get; set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private static int maxFrame = 400; // chosen arbitrarily
		private int xChange;
		private int yChange;
		private Random rand;
		private int currentFrame;


		public Fireball(Point position, int xChange, int yChange)
		{
			FireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite(); // note that it lives on the enemies sheet
			CurrentLoc = new Rectangle(position, new Point(8, 10));
			this.xChange = xChange;
			this.yChange = yChange;
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			int newX = CurrentLoc.X + xChange;
			int newY = CurrentLoc.Y + yChange;
			CurrentLoc = new Rectangle(new Point(newX, newY), CurrentLoc.Size);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			FireballSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			Move();
			FireballSprite.Update();
		}
		public void DespawnEffect() { } // don't have anything special to 



	}
}