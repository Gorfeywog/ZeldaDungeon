using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Stalfos : IEnemy
	{
		public ISprite StalfosSprite { get; set; }
		public bool ReadyToDespawn { get; set; }

		public Rectangle CurrentLoc { get; set; }

		private int currentFrame;
		private CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public DrawLayer Layer { get => DrawLayer.Normal; }
		public EntityList roomEntities;
		private int CurrentHealth;
		private bool Damaged;
		private static readonly int damageDelay = 80;
		private int damageCountdown = 0;

		public Stalfos(Point position, Room r)
		{
			StalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
			int width = (int)SpriteUtil.SpriteSize.StalfosX;
			int height = (int)SpriteUtil.SpriteSize.StalfosY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
			Damaged = false;
			currentFrame = 0;
			Collision = new CollisionHandler(r, this);
			ReadyToDespawn = false;
		}

		public void Move()
		{
			int dirChance = 3;
			int locChange = (4 * SpriteUtil.Rand.Next(3) - 4) * SpriteUtil.SCALE_FACTOR;
			Rectangle newPos;
			if (SpriteUtil.Rand.Next(dirChance) == 0)
            {
				newPos = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size); ;
			} else
			{
				newPos = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
			}
			if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;

		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{
			CurrentHealth--;
			if (CurrentHealth == 0) DespawnEffect();
			Damaged = true;
			StalfosSprite.damaged = true;
			damageCountdown = damageDelay;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			StalfosSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			if (Damaged)
			{
				damageCountdown--;
				if (damageCountdown == 0)
				{
					Damaged = false;
				}
			}
			StalfosSprite.Update();
			currentFrame++;
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