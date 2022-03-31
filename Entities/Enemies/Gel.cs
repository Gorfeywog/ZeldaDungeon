using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Gel : IEnemy
	{
		public ISprite GelSprite { get; set; }
		public bool ReadyToDespawn { get; private set; }
		private int currentFrame;
		private CollisionHandler Collision { get; set; }
		private int currentHealth;
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public DrawLayer Layer { get => DrawLayer.Normal; }
		public EntityList roomEntities;
		public Rectangle CurrentLoc { get; set; }
		private int damageCountdown = 0;

		public Gel(Point position, Room r)
		{
			GelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
			int width = (int)SpriteUtil.SpriteSize.GelX;
			int height = (int)SpriteUtil.SpriteSize.GelY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			ReadyToDespawn = false;
			currentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
			Collision = new CollisionHandler(r, this);
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
			if (damageCountdown == 0)
            {
				currentHealth--;
				damageCountdown = SpriteUtil.DAMAGE_DELAY;
			}
			if (currentHealth == 0) ReadyToDespawn = true;
			GelSprite.Damaged = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			GelSprite.Draw(spriteBatch, CurrentLoc);
		}

		private static readonly int moveChance = 8;
		public void Update()
		{
			if (GelSprite.Damaged)
			{
				damageCountdown--;
				if (damageCountdown == 0)
				{
					GelSprite.Damaged = false;
				}
			}
			GelSprite.Update();
			currentFrame++;
			if (currentFrame % moveChance == 0)
			{
				Move();
			}

			Collision.Update();
		}
		public void DespawnEffect() { }

	}
}