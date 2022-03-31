using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Rooms;
using System.Diagnostics;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Goriya : IEnemy
	{
		public bool ReadyToDespawn { get; private set; }

		public ISprite GoriyaSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		private CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public DrawLayer Layer { get => DrawLayer.Normal; }

		private int currentFrame;
		private bool IsAttacking { get => (boomerang != null) && !boomerang.ReadyToDespawn; }
		private Room r;
		private IProjectile boomerang;
		private bool isRed;

		private Direction currDirection;
		private static readonly int damageDelay = 80;
		private int damageCountdown = 0;
		private int CurrentHealth;

		public Goriya(Point position, Room r, bool isRed)
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
			this.r = r;
			currDirection = Direction.Left;
			currentFrame = 0;
			Collision = new CollisionHandler(r, this);
			CurrentHealth = SpriteUtil.GENERIC_MAX_HEALTH;
		}

		public void Move()
		{
			//One in four chance to change directions
			int changeDirChance = 4;
			if (SpriteUtil.Rand.Next(changeDirChance) == 0) {
				switch (SpriteUtil.Rand.Next(changeDirChance)) 
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
			boomerang = new BoomerangProjectile(this, currDirection, false, r.G);
			r.RegisterEntity(boomerang);
		}

		public void TakeDamage()
		{
			if (damageCountdown == 0)
            {
				CurrentHealth--;
				damageCountdown = SpriteUtil.DAMAGE_DELAY;
			}
			if (CurrentHealth == 0) ReadyToDespawn = true;
			GoriyaSprite.Damaged = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			GoriyaSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			if (GoriyaSprite.Damaged)
			{
				damageCountdown--;
				if (damageCountdown == 0)
				{
					GoriyaSprite.Damaged = false;
				}
			}
			currentFrame++;
			GoriyaSprite.Update();
			int moveChance = 8;
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
	}
}