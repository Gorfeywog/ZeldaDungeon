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

		private Random rand;
		private int currentFrame;

		public Keese(Point position)
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			CurrentLoc = new Rectangle(position, new Point(16, 10));
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8 * rand.Next(3) - 8, CurrentLoc.Y + 8 * rand.Next(3) - 8), CurrentLoc.Size);

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
			if (currentFrame % 8 == 0)
			{
				Move();
			}
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;

	}
}