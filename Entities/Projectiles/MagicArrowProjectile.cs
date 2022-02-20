using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class MagicArrowProjectile : IProjectile
	{
		private ISprite MagicArrowSprite { get; set; }
		public Point CurrentPoint { get; private set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private Direction orientation;
		private static int maxFrame = 400; // chosen arbitrarily; should probably be reduced
		private int currentFrame;
		private int speed = 8; // note that this is faster than the regular arrow!


		public MagicArrowProjectile(Point position, Direction dir)
		{
			MagicArrowSprite = ItemSpriteFactory.Instance.CreateMagicArrow(dir);
			CurrentPoint = position;
			orientation = dir;
			currentFrame = 0;
		}

		public void Move()
		{
			CurrentPoint = EntityUtils.Offset(CurrentPoint, orientation, speed);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			MagicArrowSprite.Draw(spriteBatch, CurrentPoint);
		}

		public void Update()
		{
			currentFrame++;
			this.Move();
			MagicArrowSprite.Update();
		}
		public void DespawnEffect() { }
	}
}