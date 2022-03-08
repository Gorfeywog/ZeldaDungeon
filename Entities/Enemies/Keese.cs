using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Keese : IEnemy
	{
		public ISprite KeeseSprite { get; set; }

		public Rectangle CurrentLoc {get; set;}

		private int currentFrame;

		public Keese(Point position)
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			int width = (int)SpriteUtil.SpriteSize.KeeseX;
			int height = (int)SpriteUtil.SpriteSize.KeeseY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
		}

		public void Move()
		{
			int locChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
			CurrentLoc = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y + locChange), CurrentLoc.Size);

		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			KeeseSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			KeeseSprite.Update();
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