using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class OldMan : IEnemy
	{
		public ISprite OldManSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }

		public OldMan(Point position)
		{
			OldManSprite = EnemySpriteFactory.Instance.CreateOldManSprite();
			CurrentLoc = new Rectangle(position, new Point(16, 16));
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
			OldManSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
        {
			OldManSprite.Update();
        }
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}