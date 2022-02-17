using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;

namespace ZeldaDungeon.Entities.Enemies
{
	public class RedGoriya : IEnemy
	{
		public ISprite RedGoriyaSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;
		private int currentFrame;
		private bool isAttacking;
		List<Boomerang> boomerangs;


		private enum GoriyaDirection { Left, Right, Up, Down };
		private GoriyaDirection currDirection;

		public RedGoriya(Point position)
		{
			RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
			posX = position.X;
			posY = position.Y;
			currDirection = GoriyaDirection.Left;
			currentFrame = 0;
			isAttacking = false;
			boomerangs = new List<Boomerang>();


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
						currDirection = GoriyaDirection.Left;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteLeft();
						break;

					case 1:
						currDirection = GoriyaDirection.Right;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteRight();
						break;

					case 2:
						currDirection = GoriyaDirection.Up;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteUp();
						break;

					case 3:
						currDirection = GoriyaDirection.Down;
						RedGoriyaSprite = EnemySpriteFactory.Instance.CreateRedGoriyaSpriteDown();
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
			Boomerang boomerang;
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
			boomerangs.Add(boomerang);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			RedGoriyaSprite.Draw(spriteBatch, new Point(posX, posY));
			foreach (Boomerang boomerang in boomerangs)
			{
				boomerang.Draw(spriteBatch);
			}
		}

		public void Update()
		{
			currentFrame++;
			RedGoriyaSprite.Update();
			foreach (Boomerang boomerang in boomerangs)
			{
				boomerang.Update();
				isAttacking = boomerang.IsFlying();

			}
			if (currentFrame % 8 == 0 && !isAttacking)
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