using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class SwordProj : IProjectile
	{
		private ISprite Sprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public bool ReadyToDespawn { get; private set; }
		public DrawLayer Layer { get => DrawLayer.Normal; }
		private static int maxFrame = 12;
		private int currentFrame;
		private static readonly int speed = 2 * SpriteUtil.SCALE_FACTOR;
		private Direction dir;


		public SwordProj(Point position, Direction dir)
		{
			this.dir = dir;
			Sprite = ItemSpriteFactory.Instance.CreateSwordProj(dir);
			int width = (int)SpriteUtil.SpriteSize.SwordProjWidth;
			int height = (int)SpriteUtil.SpriteSize.SwordProjHeight;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
		}

		public void Move()
		{
			Point newPoint = EntityUtils.Offset(CurrentLoc.Location, dir, speed);
			CurrentLoc = new Rectangle(newPoint, CurrentLoc.Size);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Sprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			if (currentFrame > maxFrame)
            {
				ReadyToDespawn = true;
            }
			Move();
			Sprite.Update();
		}
		public void DespawnEffect() { }

		public void OnHit(IEntity target, Direction direction)
		{
			if (target is IEnemy en && !en.isFriendly)
			{
				en.TakeDamage();
			}
		}

	}
}