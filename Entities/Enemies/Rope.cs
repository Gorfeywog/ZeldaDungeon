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

		private int currentFrame;
		public CollisionHandler collision { get; set; }
		private EntityList roomEntities;
		private Direction currDirection;

		public Rope(Point position)
		{
			RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
			currDirection = Direction.Left;

			int width = (int)SpriteUtil.SpriteSize.RopeX;
			int height = (int)SpriteUtil.SpriteSize.RopeY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			collision = new CollisionHandler(roomEntities, this);
		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
		}

		public void Move()
		{
			//One in four chance to change directions
			if (SpriteUtil.Rand.Next(4) == 0)
			{
				switch (SpriteUtil.Rand.Next(4))
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
				//TODO: implement ccollision checking.
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