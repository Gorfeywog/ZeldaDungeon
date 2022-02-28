using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Gel : IEnemy
	{
		public ISprite GelSprite { get; set; }
		private Random rand;
		private int currentFrame;

		public Rectangle CurrentLoc { get; set; }

		public Gel(Point position)
		{
			GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
			CurrentLoc = new Rectangle(position, new Point(8, 9));
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			int movingNum = rand.Next(5);
			if (movingNum < 2)
			{
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8 * rand.Next(3) - 8, CurrentLoc.Y), CurrentLoc.Size);
			}
			else if (movingNum > 2)
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
			GelSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			GelSprite.Update();
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