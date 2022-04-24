﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class ThrownSword : IProjectile
	{
		private ISprite SwordSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public bool ReadyToDespawn { get; private set; }

		private CollisionHandler collision;
		public DrawLayer Layer { get => DrawLayer.Normal; }
		private Direction orientation;
		private static int maxFrame = 20;
		private int currentFrame;
		private static readonly int speed = 2 * SpriteUtil.SCALE_FACTOR;
		private Game1 g;

		public ThrownSword(Point position, Direction dir, Game1 g)
		{
			SwordSprite = ItemSpriteFactory.Instance.CreateSword(dir);
			Point size;
			int width = (int)SpriteUtil.SpriteSize.SwordWidth * SpriteUtil.SCALE_FACTOR;
			int length = (int)SpriteUtil.SpriteSize.SwordLength * SpriteUtil.SCALE_FACTOR;
			if (dir == Direction.Left || dir == Direction.Right)
			{
				size = new Point(length, width);
			}
			else
			{
				size = new Point(width, length);
			}
			CurrentLoc = new Rectangle(position, size);
			orientation = dir;
			currentFrame = 0;
			this.g = g;
			collision = new CollisionHandler(g.CurrentRoom, this);
		}

		public void Move()
		{
			CurrentLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, orientation, speed), CurrentLoc.Size);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			SwordSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			if (!ReadyToDespawn) ReadyToDespawn = collision.WillHitBlock(CurrentLoc);
			Move();
			SwordSprite.Update();
		}

		public void OnHit(IEntity target, Direction direction)
        {
			if (target is IEnemy en)
            {
				en.TakeDamage(orientation);
				ReadyToDespawn = true;
            }
        }
		private static Direction[] projDirections = { Direction.NW, Direction.NE, Direction.SW, Direction.SE };
		public void DespawnEffect()
        {
			foreach (var d in projDirections)
            {
				g.CurrentRoom.RegisterEntity(new SwordProj(CurrentLoc.Location, d));
            }
			g.Player.SwordIsThrown = false;
        }
	}
}