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
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
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
			this.roomEntities = g.CurrentRoom.roomEntities;
			Collision = new CollisionHandler(roomEntities, this);
		}
		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
			Collision.ChangeRooms(roomEntities);
		}

		public void Move()
		{
			//One in four chance to change directions
			int changeDirChance = 4;
			if (SpriteUtil.Rand.Next(changeDirChance) == 0) {
				switch (SpriteUtil.Rand.Next(changeDirChance)) // consider moving the sprite selection logic to EnemySpriteFactory
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
			int locChange = 4 * SpriteUtil.SCALE_FACTOR;
			switch (currDirection)
			{
				case Direction.Left:
					if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size);
					}
					break;

				case Direction.Right:
					if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
					}
					break;

				case Direction.Up:
					if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - locChange), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y - locChange), CurrentLoc.Size);
					}
					break;

				case Direction.Down:
					if (!Collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size)))
                    {
						CurrentLoc = new Rectangle(new Point(CurrentLoc.X, CurrentLoc.Y + locChange), CurrentLoc.Size);
					}
					break;

				default:
					break;
			}
		}

		public void Attack()
		{
			boomerang = new BoomerangProjectile(this, currDirection, false, g);
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
			int moveChance = 8;
			Collision.ChangeRooms(roomEntities);
			if (currentFrame % moveChance == 0 && !IsAttacking)
			{
				Move();
			}
			int attackChance = 128;
			int randChance = 2;
			if (currentFrame % attackChance == 0 && SpriteUtil.Rand.Next(randChance) == 0)
			{
				Attack();
			}

			Collision.Update();

		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}