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
		public Rectangle CurrentLoc { get; set; }
		public bool ReadyToDespawn { get => currentFrame > maxFrame; }
		private Direction orientation;
		private static int maxFrame = 60;
		private int currentFrame;
		private int speed = 8; // note that this is faster than the regular arrow!
		private Game1 g;

		public MagicArrowProjectile(Point position, Direction dir, Game1 g) // should this and regular arrow be the same class?
		{
			MagicArrowSprite = ItemSpriteFactory.Instance.CreateMagicArrow(dir);
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
			MagicArrowSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			this.Move();
			MagicArrowSprite.Update();
		}
		public void DespawnEffect() => g.RegisterProjectile(new HitEffect(CurrentLoc.Location));
	}
}