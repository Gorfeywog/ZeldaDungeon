using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Diagnostics;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Keese : IEnemy
	{
		public ISprite KeeseSprite { get; set; }
		public bool ReadyToDespawn { get; set; }

		private CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.High; }
		public DrawLayer Layer { get => DrawLayer.High; }
		public EntityList roomEntities;
		public Rectangle CurrentLoc {get; set;}
		private int CurrentHealth;
		private int damageCountdown = 0;

		private int currentFrame;

		public Keese(Point position, Room r)
		{
			KeeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
			int width = (int)SpriteUtil.SpriteSize.KeeseX;
			int height = (int)SpriteUtil.SpriteSize.KeeseY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			Collision = new CollisionHandler(r, this);
			CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
			ReadyToDespawn = false;
		}

		public void Move()
		{
			int locXChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
			int locYChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
			Rectangle newPos = new Rectangle(new Point(CurrentLoc.X + locXChange, CurrentLoc.Y + locYChange), CurrentLoc.Size);
			if (!Collision.WillHitBlock(newPos))
            {
				CurrentLoc = newPos;
			}
		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{
			if (damageCountdown == 0)
            {
				CurrentHealth--;
				damageCountdown = SpriteUtil.DAMAGE_DELAY;
			}

			if (CurrentHealth == 0) DespawnEffect();
			KeeseSprite.damaged = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			KeeseSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			if (KeeseSprite.damaged)
			{
				damageCountdown--;
				if (damageCountdown == 0)
				{
					KeeseSprite.damaged = false;
				}
			}
			currentFrame++;
			KeeseSprite.Update();
			int moveChance = 8;
			if (currentFrame % moveChance == 0)
			{
				Move();
			}

			Collision.Update();
		}
		public void DespawnEffect() 
		{
			ReadyToDespawn = true;
		}

	}
}