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

		private int currentFrame;

		public Stalfos(Point position)
		{
			StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
			int width = (int)SpriteUtil.SpriteSize.StalfosX;
			int height = (int)SpriteUtil.SpriteSize.StalfosY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));

			currentFrame = 0;
		}

		public void Move()
		{
			int dirChance = 3;
			int locChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
			if (SpriteUtil.Rand.Next(dirChance) == 0)
            {
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size); ;
			} else
			{
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
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
			int moveChance = 8;
			if (currentFrame % moveChance == 0)
			{
				Move();
			}
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}