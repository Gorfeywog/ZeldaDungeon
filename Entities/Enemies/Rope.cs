using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Rope : IEnemy
	{
		public ISprite RopeSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		private Random rand;
		private int currentFrame;

		private Direction currDirection;

		public Rope(Point position)
		{
			RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
			currDirection = Direction.Left;

			int width = (int)SpriteUtil.SpriteSize.RopeX;
			int height = (int)SpriteUtil.SpriteSize.RopeY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;

			rand = new Random();

		}

		public void Move()
		{
			//One in four chance to change directions
			if (rand.Next(4) == 0)
			{
				switch (rand.Next(4))
				{
					case 0:
						currDirection = Direction.Left;
						RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
						break;

					case 1:
						currDirection = Direction.Right;
						RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteRight();
						break;

					case 2:
						currDirection = Direction.Up;
						break;

					case 3:
						currDirection = Direction.Down;
						break;

					default:
						break;
				}
			}

			//Determines which way to move
			switch (currDirection)
			{
				case Direction.Left:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X - 8, CurrentLoc.Y), CurrentLoc.Size);
					break;

				case Direction.Right:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8, CurrentLoc.Y), CurrentLoc.Size);
					break;

				case Direction.Up:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - 8), CurrentLoc.Size);
					break;

				case Direction.Down:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8), CurrentLoc.Size);
					break;

				default:
					break;
			}

		}

		public void Attack()
		{
			//TODO Need to add boomerang to spriteSheet
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			RopeSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			RopeSprite.Update();
			currentFrame++;
			if (currentFrame % 8 == 0)
			{
				Move();
			}
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}