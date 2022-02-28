using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class ArrowProjectile : IProjectile
	{
		private ISprite ArrowSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private Direction orientation;
		private static int maxFrame = 60; // chosen arbitrarily
		private int currentFrame;
		private int speed = 5;
		private Game1 g;

		public ArrowProjectile(Point position, Direction dir, Game1 g)
		{
			ArrowSprite = ItemSpriteFactory.Instance.CreateArrow(dir);
			Point size;
			if (dir == Direction.Left || dir == Direction.Right)
            {
				size = new Point(16, 5);
            } 
			else
            {
				size = new Point(5, 16);
			}
			CurrentLoc = new Rectangle(position, size);
			orientation = dir;
			currentFrame = 0;
			this.g = g;
		}

		public void Move()
		{
			CurrentLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, orientation, speed), CurrentLoc.Size);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ArrowSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			Move();
			ArrowSprite.Update();
		}
		public void DespawnEffect() => g.RegisterProjectile(new HitEffect(CurrentLoc.Location));
	}
}