using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Link;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class StaticSword : IProjectile
	{
		private ISprite SwordSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public bool ReadyToDespawn => currentFrame > maxFrame;
		public DrawLayer Layer { get => DrawLayer.Normal; }
		private Direction orientation;
		private static int maxFrame = LinkStateMachine.itemUseDelay;
		private int currentFrame;
		private static readonly int speed = 2 * SpriteUtil.SCALE_FACTOR;
		private Game1 g;

		public StaticSword(Point position, Direction dir, Game1 g)
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
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			SwordSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			SwordSprite.Update();
		}

		public void OnHit(IEntity target)
        {
			if (target is IEnemy en)
            {
				en.TakeDamage();
            }
        }
		public void DespawnEffect() { }
	}
}