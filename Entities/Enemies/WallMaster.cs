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

		private Direction currDirection;

		public WallMaster(Point position)
		{
			WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
			currDirection = Direction.SE;
			posX = position.X;
			posY = position.Y;
			currentFrame = 0;

			rand = new Random();

		}

		public void Move()
		{
			//One in eight chance to change directions
			if (rand.Next(8) == 0)
			{
				switch (rand.Next(4))
				{
					case 0:
						currDirection = Direction.SE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSE();
						break;

					case 1:
						currDirection = Direction.NE;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNE();
						break;

					case 2:
						currDirection = Direction.NW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteNW();
						break;

					case 3:
						currDirection = Direction.SW;
						WallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSpriteSW();
						break;

					default:
						break;
				}
			}

			//Determines which way to move
			switch (currDirection)
			{
				case Direction.SE:
					posX += 8;
					posY += 8;
					break;

				case Direction.NE:
					posX += 8;
					posY -= 8;
					break;

				case Direction.NW:
					posX -= 8;
					posY -= 8;
					break;

				case Direction.SW:
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
				Move();
			}
		}




	}
}