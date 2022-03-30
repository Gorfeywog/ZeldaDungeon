using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Rope : IEnemy
	{
		public ISprite RopeSprite { get; set; }
		public bool ReadyToDespawn { get; set; }

		public Rectangle CurrentLoc { get; set; }

		private int currentFrame;
		private CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public DrawLayer Layer { get => DrawLayer.Normal; }
		private EntityList roomEntities;
		private Direction currDirection;

		public Rope(Point position, Room r)
		{
			RopeSprite = EnemySpriteFactory.Instance.CreateRopeSpriteLeft();
			currDirection = Direction.Left;

			int width = (int)SpriteUtil.SpriteSize.RopeX;
			int height = (int)SpriteUtil.SpriteSize.RopeY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			currentFrame = 0;
			Collision = new CollisionHandler(r, this);
		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
		}

		public void Move()
		{
			//One in four chance to change directions
			int changeDirChance = 4;
			if (SpriteUtil.Rand.Next(changeDirChance) == 0)
			{
				switch (SpriteUtil.Rand.Next(changeDirChance))
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
			int locChange = 4 * SpriteUtil.SCALE_FACTOR;
			switch (currDirection)
			{
				//TODO: implement collision checking.
				case Direction.Left:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size);
					break;

				case Direction.Right:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
					break;

				case Direction.Up:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - locChange), CurrentLoc.Size);
					break;

				case Direction.Down:
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
					break;

				default:
					break;
			}

		}

		public void Attack()
		{

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
			int moveChance = 8;
			if (currentFrame % moveChance == 0)
			{
				Move();
			}

			Collision.Update();
		}
		public void DespawnEffect() { }
	}
}