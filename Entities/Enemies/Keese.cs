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

		public CollisionHandler collision { get; set; }
		public EntityList roomEntities;
		public Rectangle CurrentLoc {get; set;}

		private int currentFrame;

		public Keese(Point position)
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			int width = (int)SpriteUtil.SpriteSize.KeeseX;
			int height = (int)SpriteUtil.SpriteSize.KeeseY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			collision = new CollisionHandler(roomEntities, this);
		}

		public void UpdateList(EntityList roomEntities)
        {
            this.roomEntities = roomEntities;
			collision.ChangeRooms(roomEntities);
		}

		public void Move()
		{
			int DistanceToMoveX = SpriteUtil.Rand.Next(3);
			int DistanceToMoveY = SpriteUtil.Rand.Next(3);
			Rectangle newPos = new Rectangle(new Point(CurrentLoc.X + 8 * DistanceToMoveX - 8, CurrentLoc.Y + 8 * DistanceToMoveY - 8), CurrentLoc.Size);
			if (!collision.WillHitBlock(newPos))
            {
				CurrentLoc = newPos;
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