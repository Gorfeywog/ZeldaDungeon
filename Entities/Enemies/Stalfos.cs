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
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public EntityList roomEntities;

		public Stalfos(Point position)
		{
			StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
			int width = (int)SpriteUtil.SpriteSize.StalfosX;
			int height = (int)SpriteUtil.SpriteSize.StalfosY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));

			currentFrame = 0;
			Collision = new CollisionHandler(roomEntities, this);
		}
		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
			Collision.ChangeRooms(roomEntities);
		}

		public void Move()
		{
			int DistanceToMove = SpriteUtil.Rand.Next(3);
			Rectangle newPos;
			if (SpriteUtil.Rand.Next(2) == 0)
            {
				newPos = new Rectangle(new Point(CurrentLoc.X + 8 * DistanceToMove - 8, CurrentLoc.Y), CurrentLoc.Size);
				if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;

			} else
			{
				newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8 * DistanceToMove - 8), CurrentLoc.Size);
				if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;
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
			if (currentFrame % 8 == 0)
			{
				Move();
			}

			Collision.Update();
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}