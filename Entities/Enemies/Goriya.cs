using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Goriya : IEnemy
	{
		public ISprite GoriyaSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public CollisionHandler collision { get; set; }
		public EntityList roomEntities;

		private int currentFrame;
		private bool IsAttacking { get => (boomerang != null) && !boomerang.ReadyToDespawn; }
		private Game1 g;
		private IProjectile boomerang;
		private bool isRed;

		private Direction currDirection;

		public Goriya(Point position, Game1 g, bool isRed)
		{
			this.isRed = isRed;
			if (isRed)
            {
				GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
			}
			else
            {
				GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteLeft();
            }
			int width = (int)SpriteUtil.SpriteSize.GoriyaX;
			int height = (int)SpriteUtil.SpriteSize.GoriyaY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			this.g = g;
			currDirection = Direction.Left;
			currentFrame = 0;
			this.roomEntities = g.CurrentRoom.roomEntitiesEL;
			collision = new CollisionHandler(roomEntities, this);
		}
		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
			collision.ChangeRooms(roomEntities);
		}

		public void Move()
		{
			//One in four chance to change directions
			if (SpriteUtil.Rand.Next(4) == 0) {
				switch (SpriteUtil.Rand.Next(4)) // consider moving the sprite selection logic to EnemySpriteFactory
				{
					case 0:
						currDirection = Direction.Left;
						if (isRed)
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
						}
						else
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteLeft();
						}
						break;

					case 1:
						currDirection = Direction.Right;
						if (isRed)
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteRight();
						}
						else
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteRight();
						}
						break;

					case 2:
						currDirection = Direction.Up;
						if (isRed)
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteUp();
						}
						else
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteUp();
						}
						break;

					case 3:
						currDirection = Direction.Down;
						if (isRed)
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteDown();
						}
						else
						{
							GoriyaSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaSpriteDown();
						}
						break;

					default:
						break;
				}
			}

			//Determines which way to move
			switch (currDirection)
			{
				case Direction.Left:
					if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X - 8, CurrentLoc.Y), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X - 8, CurrentLoc.Y), CurrentLoc.Size);
					}
					break;

				case Direction.Right:
					if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X + 8, CurrentLoc.Y), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X + 8, CurrentLoc.Y), CurrentLoc.Size);
					}
					break;

				case Direction.Up:
					if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - 8), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - 8), CurrentLoc.Size);
					}
					break;

				case Direction.Down:
					if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + 8), CurrentLoc.Size);
					}
					break;

				default:
					break;
			}
		}

		public void Attack()
		{
			boomerang = new Boomerang(CurrentLoc.Location, currDirection, false);
			g.RegisterProjectile(boomerang);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			GoriyaSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			GoriyaSprite.Update();
			collision.ChangeRooms(roomEntities);
			if (currentFrame % 8 == 0 && !IsAttacking)
			{
				Move();
			}
			if (currentFrame % 128 == 0 && SpriteUtil.Rand.Next(2) == 0)
			{
				Attack();
			}

		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}