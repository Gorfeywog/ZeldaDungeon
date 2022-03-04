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
		private Random rand;
		private int currentFrame;
		public CollisionHandler collision { get; set; }
		public EntityList roomEntities;

		public Stalfos(Point position)
		{
			StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
			int width = (int)SpriteUtil.SpriteSize.StalfosX;
			int height = (int)SpriteUtil.SpriteSize.StalfosY;
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
			if (rand.Next(2) == 0)
            {
				if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X + 8 * DistanceToMove - 8, CurrentLoc.Y), CurrentLoc.Size)))
                {
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8 * DistanceToMove - 8, CurrentLoc.Y), CurrentLoc.Size); ;
				}
			} else
			{
				if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8 * DistanceToMove - 8), CurrentLoc.Size)))
                {
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8 * DistanceToMove - 8), CurrentLoc.Size);
				}
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
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}