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
		private int posX;
		private int posY;
		private Random rand;
		private int currentFrame;
		private bool IsAttacking { get => (boomerang != null) && !boomerang.ReadyToDespawn; }
		private Game1 g;
		private IProjectile boomerang;
		private bool isRed;
		private enum GoriyaDirection { Left, Right, Up, Down };
		private GoriyaDirection currDirection;

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
			posX = position.X;
			posY = position.Y;
			this.g = g;
			currDirection = GoriyaDirection.Left;
			currentFrame = 0;
			rand = new Random();
		}

		public void Move()
		{
			//One in four chance to change directions
			if (rand.Next(4) == 0) {
				switch (rand.Next(4))
				{
					case 0:
						currDirection = GoriyaDirection.Left;
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
						currDirection = GoriyaDirection.Right;
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
						currDirection = GoriyaDirection.Up;
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
						currDirection = GoriyaDirection.Down;
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
				case GoriyaDirection.Left:
					posX -= 8;
					break;

				case GoriyaDirection.Right:
					posX += 8;
					break;

				case GoriyaDirection.Up:
					posY -= 8;
					break;

				case GoriyaDirection.Down:
					posY += 8;
					break;

				default:
					break;
			}
		}

		public void Attack()
		{
			switch (currDirection)
			{
				case GoriyaDirection.Left:
					boomerang = new Boomerang(new Point(posX, posY), -24, 0);
					break;

				case GoriyaDirection.Right:
					boomerang = new Boomerang(new Point(posX, posY), 24, 0);
					break;

				case GoriyaDirection.Up:
					boomerang = new Boomerang(new Point(posX, posY), 0, -24);
					break;

				case GoriyaDirection.Down:
					boomerang = new Boomerang(new Point(posX, posY), 0, 24);
					break;

				default:
					boomerang = new Boomerang(new Point(posX, posY), -24, 0);
					break;
			}
			g.RegisterProjectile(boomerang);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			GoriyaSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			currentFrame++;
			GoriyaSprite.Update();
			if (currentFrame % 8 == 0 && !IsAttacking)
			{
				this.Move();
			}
			if (currentFrame % 64 == 0 && rand.Next(4) == 0)
			{
				this.Attack();
			}

		}
	}
}