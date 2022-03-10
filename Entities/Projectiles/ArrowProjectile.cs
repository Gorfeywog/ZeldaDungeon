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
		public bool ReadyToDespawn { get; private set; }
		private Direction orientation;
		private static int maxFrame = 60; // chosen arbitrarily
		private int currentFrame;
		private static readonly int normalSpeed = 3 * SpriteUtil.SCALE_FACTOR;
		private static readonly int magicSpeed = 5 * SpriteUtil.SCALE_FACTOR;
		private int speed;
		private Game1 g;

		public ArrowProjectile(Point position, Direction dir, bool isMagic, Game1 g)
		{
			ArrowSprite = ItemSpriteFactory.Instance.CreateArrow(dir, isMagic);
			speed = isMagic ? magicSpeed : normalSpeed;
			Point size;
			int width = (int)SpriteUtil.SpriteSize.ArrowWidth * SpriteUtil.SCALE_FACTOR;
			int length = (int)SpriteUtil.SpriteSize.ArrowLength * SpriteUtil.SCALE_FACTOR;
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
			if (currentFrame > maxFrame) { ReadyToDespawn = true; }
			Move();
			ArrowSprite.Update();
		}

		public void OnHit(IEntity target)
        {
			if (target is IEnemy en)
            {
				en.TakeDamage();
				ReadyToDespawn = true;
            }
        }
		public void DespawnEffect() => g.CurrentRoom.RegisterProjectile(new HitEffect(CurrentLoc.Location));
	}
}