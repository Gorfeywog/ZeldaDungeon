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
		public Point CurrentPoint { get; private set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private Direction orientation;
		private static int maxFrame = 60; // chosen arbitrarily
		private int currentFrame;
		private int speed = 5;
		private Game1 g;

		public ArrowProjectile(Point position, Direction dir, Game1 g)
		{
			ArrowSprite = ItemSpriteFactory.Instance.CreateArrow(dir);
			CurrentPoint = position;
			orientation = dir;
			currentFrame = 0;
			this.g = g;
		}

		public void Move()
		{
			CurrentPoint = EntityUtils.Offset(CurrentPoint, orientation, speed);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ArrowSprite.Draw(spriteBatch, CurrentPoint);
		}

		public void Update()
		{
			currentFrame++;
			this.Move();
			ArrowSprite.Update();
		}
		public void DespawnEffect() => g.RegisterProjectile(new HitEffect(CurrentPoint));
	}
}