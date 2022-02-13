using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Trap : IEnemy
	{
		public ISprite TrapSprite { get; set; }
		private int posX;
		private int posY;

		public Trap(Point position)
		{
			TrapSprite = EnemySpriteFactory.Instance.CreateTrapSprite();
			posX = position.X;
			posY = position.Y;
		}

		public void Move()
		{

		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}
		
		public void Draw(SpriteBatch spriteBatch)
        {
			TrapSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
        {
			TrapSprite.Update();
        }




	}
}