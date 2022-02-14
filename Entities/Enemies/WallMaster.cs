using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
	public class WallMaster : IEnemy
	{
		public ISprite WallMasterSprite { get; set; }
		private int posX;
		private int posY;
		private Random rand;
		private int currentFrame;


		private enum WallMasterDirection { SE, NE, NW, SW };
		private WallMasterDirection currDirection;

		public WallMaster(Point position)
		{
			WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
			currDirection = WallMasterDirection.SE;
			posX = position.X;
			posY = position.Y;
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
						currDirection = WallMasterDirection.SE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
						break;

					case 1:
						currDirection = WallMasterDirection.NE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNE();
						break;

					case 2:
						currDirection = WallMasterDirection.NW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNW();
						break;

					case 3:
						currDirection = WallMasterDirection.SW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSW();
						break;

					default:
						break;
				}
			}

			//Determines which way to move
			switch (currDirection)
			{
				case WallMasterDirection.SE:
					posX += 8;
					posY += 8;
					break;

				case WallMasterDirection.NE:
					posX += 8;
					posY -= 8;
					break;

				case WallMasterDirection.NW:
					posX -= 8;
					posY -= 8;
					break;

				case WallMasterDirection.SW:
					posX -= 8;
					posY += 8;
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
			WallMasterSprite.Draw(spriteBatch, new Point(posX, posY));
		}

		public void Update()
		{
			WallMasterSprite.Update();
			currentFrame++;
			if (currentFrame % 8 == 0)
			{
				this.Move();
			}
		}




	}
}