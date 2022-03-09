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
		private Game1 Game;

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
			speed = 1 * SpriteUtil.SCALE_FACTOR;
			Game = game;

		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
			Collision.ChangeRooms(roomEntities);
		}

		public void Move()
		{

			Point newPt;
			if (Moving) 
            {
				newPt = EntityUtils.Offset(CurrentLoc.Location, Collision.DetectDirection(Game.Player), speed);
				if (!Collision.WillHitBlock(new Rectangle(newPt, CurrentLoc.Size))) {
					CurrentLoc = new Rectangle(newPt, CurrentLoc.Size);
				} else
                {
					Moving = false;
                }
				

			}

			Collision.Update();
			//No movement so collision handling not necessary
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