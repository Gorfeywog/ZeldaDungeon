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
			//TODO Fix old man being drawn off center in a less concrete manner
			CurrentLoc = new Rectangle(new Point(position.X - (8 * SpriteUtil.SCALE_FACTOR), position.Y), 
				new Point((int)SpriteUtil.SpriteSize.OldManX * SpriteUtil.SCALE_FACTOR,
				(int)SpriteUtil.SpriteSize.OldManY * SpriteUtil.SCALE_FACTOR));
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