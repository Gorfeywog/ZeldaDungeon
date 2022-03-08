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
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public EntityList roomEntities;
		public Rectangle CurrentLoc { get; set; }

		public Gel(Point position)
		{
			GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
			int width = (int)SpriteUtil.SpriteSize.GelX;
			int height = (int)SpriteUtil.SpriteSize.GelY;
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
			int movingNum = SpriteUtil.Rand.Next(5);
			int locChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
      Rectangle newPos;
			if (movingNum < 2) // 2 in 5 chance to move in x direction
			{
				newPos = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
				if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;
			}
			else if (movingNum > 2) // 2 in 5 chance to move in y direction
			{
				newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
				if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;
			}
			// 1 in 5 chance to not move at all
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
			int moveChance = 8;
			if (currentFrame % moveChance == 0)
			{
				Move();
			}

			Collision.Update();
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;

	}
}