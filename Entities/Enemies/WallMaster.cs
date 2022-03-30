using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities.Enemies
{
	public class WallMaster : IEnemy
	{
		public ISprite WallMasterSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		private CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Ghost; }
		public DrawLayer Layer { get => DrawLayer.High; }
		private int currentFrame;
		private int CurrentHealth;
		private bool Damaged;
		private static readonly int damageDelay = 80;
		private int damageCountdown = 0;
		private Direction currDirection;

		public WallMaster(Point position, Room r)
		{
			WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
			currDirection = Direction.SE;
			int width = (int)SpriteUtil.SpriteSize.WallMasterX;
			int height = (int)SpriteUtil.SpriteSize.WallMasterY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			Collision = new CollisionHandler(r, this);
			CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
			Damaged = false;

		}

		public void Move()
		{
			//One in eight chance to change directions
			int changeDirChance = 60;
			if (SpriteUtil.Rand.Next(8) == 0 || currentFrame % changeDirChance == 0)
			{
				switch (currDirection)
				{
					case Direction.NE:
						currDirection = Direction.SE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
						break;

					case Direction.NW:
						currDirection = Direction.NE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNE();
						break;

					case Direction.SW:
						currDirection = Direction.NW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNW();
						break;

					case Direction.SE:
						currDirection = Direction.SW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSW();
						break;

					default:
						break;
				}
			}

			int speed = 4 * SpriteUtil.SCALE_FACTOR;
			Point newPos = EntityUtils.Offset(CurrentLoc.Location, currDirection, speed);
			if (!Collision.WillHitBlock(new Rectangle(newPos, CurrentLoc.Size))) CurrentLoc = new Rectangle(newPos, CurrentLoc.Size);

		}

		public void Attack() { }

		public void TakeDamage()
		{
			CurrentHealth--;
			if (CurrentHealth == 0) DespawnEffect();
			Damaged = true;
			WallMasterSprite.damaged = true;
			damageCountdown = damageDelay;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			WallMasterSprite.Draw(spriteBatch, CurrentLoc);
		}

		private static readonly int moveChance = 8;
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
			WallMasterSprite.Update();
			currentFrame++;
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