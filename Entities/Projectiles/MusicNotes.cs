using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
	public class MusicNotes : IProjectile
	{
		public ISprite MusicNoteSprite { get; private set; }
		public Rectangle CurrentLoc { get; set; }
		public DrawLayer Layer { get => DrawLayer.Normal; }
		public bool ReadyToDespawn { get; private set; }
		private static int maxFrame = 400;
		private int xChange;
		private int yChange;
		private Random rand;
		private int currentFrame;


		public MusicNotes(Point position, int xChange, int yChange)
		{
			MusicNoteSprite = EnemySpriteFactory.Instance.CreateMusicNoteSprite(); // note that it lives on the enemies sheet
			int width = (int)SpriteUtil.SpriteSize.MusicX / 2;
			int height = (int)SpriteUtil.SpriteSize.MusicY / 2;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			this.xChange = xChange;
			this.yChange = yChange;
			rand = new Random();
			currentFrame = 0;
		}

		public void Move()
		{
			int newX = CurrentLoc.X + xChange;
			int newY = CurrentLoc.Y + yChange;
			CurrentLoc = new Rectangle(new Point(newX, newY), CurrentLoc.Size);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			MusicNoteSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			if (currentFrame > maxFrame)
			{
				ReadyToDespawn = true;
			}
			Move();
			MusicNoteSprite.Update();
		}
		public void DespawnEffect() { }

		public void OnHit(IEntity target)
		{
			if (target is ILink link)
			{
				link.TakeDamage();
				ReadyToDespawn = true;
			}
		}

	}
}