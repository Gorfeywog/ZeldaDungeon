﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class WallMaster : IEnemy
	{
		public ISprite WallMasterSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.High; }
		public DrawLayer Layer { get => DrawLayer.High; }
		private EntityList roomEntities;
		private int currentFrame;

		private Direction currDirection;

		public WallMaster(Point position)
		{
			WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
			currDirection = Direction.SE;
			int width = (int)SpriteUtil.SpriteSize.WallMasterX;
			int height = (int)SpriteUtil.SpriteSize.WallMasterY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			Collision = new CollisionHandler(roomEntities, this);

		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
		}

		public void Move()
		{
			//One in eight chance to change directions
			int changeDirChance = 8;
			if (SpriteUtil.Rand.Next(changeDirChance) == 0)
			{
				switch (SpriteUtil.Rand.Next(4))
				{
					case 0:
						currDirection = Direction.SE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
						break;

					case 1:
						currDirection = Direction.NE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNE();
						break;

					case 2:
						currDirection = Direction.NW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNW();
						break;

					case 3:
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

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			WallMasterSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			WallMasterSprite.Update();
			Collision.ChangeRooms(roomEntities);
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