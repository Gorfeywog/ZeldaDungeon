using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Trap : IEnemy
	{
		public ISprite TrapSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		public bool Moving { get; set; }
		private int speed;

		private EntityList roomEntities;
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public Trap(Point position, Game1 game)
		{
			TrapSprite = EnemySpriteFactory.Instance.CreateTrapSprite();
			int width = (int)SpriteUtil.SpriteSize.TrapX;
			int height = (int)SpriteUtil.SpriteSize.TrapY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			Collision = new CollisionHandler(roomEntities, this);
			Moving = false;
			speed = 1;// * SpriteUtil.SCALE_FACTOR;

		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
			Collision.ChangeRooms(roomEntities);
		}

		public void Move() { }
		public void Move(Direction direction)
		{
			Rectangle newPos;
			bool canMove = true;
			while (canMove){
				newPos = direction switch
                {
                    Direction.Up => new Rectangle(CurrentLoc.X, CurrentLoc.Y - 1, CurrentLoc.Width, CurrentLoc.Height),
                    Direction.Down => new Rectangle(CurrentLoc.X, CurrentLoc.Y + 1, CurrentLoc.Width, CurrentLoc.Height),
                    Direction.Left => new Rectangle(CurrentLoc.X - 1, CurrentLoc.Y, CurrentLoc.Width, CurrentLoc.Height),
                    Direction.Right => new Rectangle(CurrentLoc.X + 1, CurrentLoc.Y, CurrentLoc.Width, CurrentLoc.Height)
                };
				if (!Collision.WillHitBlock(newPos)) CurrentLoc = newPos;
				else canMove = false;
            }
/*			Point newPt;
			if (Moving) 
            {
				newPt = EntityUtils.Offset(CurrentLoc.Location, Collision.DetectDirection(Game.Player), speed);
				if (!Collision.WillHitBlock(new Rectangle(newPt, CurrentLoc.Size))) {
					CurrentLoc = new Rectangle(newPt, CurrentLoc.Size);
				} else
                {
					Moving = false;
                }
				

			}*/
		}

		public void Attack()
		{

		}

		public void TakeDamage()
		{

		}
		
		public void Draw(SpriteBatch spriteBatch)
        {
			TrapSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
        {
			TrapSprite.Update();
			Collision.Update();
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}