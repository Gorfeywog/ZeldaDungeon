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

		private Random rand;
		private int currentFrame;

		public Keese(Point position)
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			int width = (int)SpriteUtil.SpriteSize.KeeseX;
			int height = (int)SpriteUtil.SpriteSize.KeeseY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			rand = new Random();
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
			int DistanceToMove = rand.Next(3);
			if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X + (8 * DistanceToMove) - 8, CurrentLoc.Y + (8 * DistanceToMove) - 8), CurrentLoc.Size)))
            {
				CurrentLoc = new Rectangle(new Point(CurrentLoc.X + (8 * DistanceToMove) - 8, CurrentLoc.Y + (8 * DistanceToMove) - 8), CurrentLoc.Size);
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