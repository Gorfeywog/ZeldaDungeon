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
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public EntityList roomEntities;
		public OldMan(Point position)
		{
			OldManSprite = EnemySpriteFactory.Instance.CreateOldManSprite();
			CurrentLoc = new Rectangle(new Point(position.X - (8 * SpriteUtil.SCALE_FACTOR), position.Y), 
				new Point((int)SpriteUtil.SpriteSize.OldManX * SpriteUtil.SCALE_FACTOR,
				(int)SpriteUtil.SpriteSize.OldManY * SpriteUtil.SCALE_FACTOR));
			Collision = new CollisionHandler(roomEntities, this);
		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
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