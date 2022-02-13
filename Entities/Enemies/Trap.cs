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

		public Trap()
		{
			TrapSprite = EnemySpriteFactory.Instance.CreateTrapSprite();
			posX = 0;
			posY = 0;
		}

		public void Move(SpriteBatch spriteBatch)
		{
			TrapSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}




	}
}