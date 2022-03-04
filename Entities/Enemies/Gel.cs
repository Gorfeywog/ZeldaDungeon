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
		private int currentFrame;

		public Rectangle CurrentLoc { get; set; }

		public Gel(Point position)
		{
			GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
			int width = (int)SpriteUtil.SpriteSize.GelX;
			int height = (int)SpriteUtil.SpriteSize.GelY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
		}

		public void Move()
		{
			int movingNum = SpriteUtil.Rand.Next(5);
			if (movingNum < 2)
			{
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8 * SpriteUtil.Rand.Next(3) - 8, CurrentLoc.Y), CurrentLoc.Size);
			}
			else if (movingNum > 2)
			{
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8 * SpriteUtil.Rand.Next(3) - 8), CurrentLoc.Size);
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