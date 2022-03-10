using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Trap : IEnemy
	{
		public ISprite TrapSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }

		private Point initPoint;
		private Direction dir;
		private int speed;
		private int cooldown;
		private const int MaxCooldown = 20;
		private Game1 Game;
		private bool attacking;
		private bool moving;
		private bool onCooldown;

		private EntityList roomEntities;
		public CollisionHandler Collision { get; set; }
		public CollisionHeight Height { get => CollisionHeight.Normal; }
		public Trap(Point position, Game1 game)
		{
			TrapSprite = EnemySpriteFactory.Instance.CreateTrapSprite();
			int width = (int)SpriteUtil.SpriteSize.TrapX;
			int height = (int)SpriteUtil.SpriteSize.TrapY;
			initPoint = position;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			Collision = new CollisionHandler(roomEntities, this);
			speed = 2 * SpriteUtil.SCALE_FACTOR;
			Game = game;
			attacking = false;
			moving = false;
			onCooldown = false;
			cooldown = 0;

		}

		public void UpdateList(EntityList roomEntities)
		{
			this.roomEntities = roomEntities;
			Collision.ChangeRooms(roomEntities);
		}

		public void Move()
		{
			Point newPt;
			if (attacking) 
            {
				newPt = EntityUtils.Offset(CurrentLoc.Location, dir, speed);
				if (!Collision.WillHitBlock(new Rectangle(newPt, CurrentLoc.Size))) {
					CurrentLoc = new Rectangle(newPt, CurrentLoc.Size);
				} else
                {
					attacking = false;
                }
				

			} else if (moving)
            {
				newPt = EntityUtils.Offset(CurrentLoc.Location, dir, -SpriteUtil.SCALE_FACTOR);
				if (CurrentLoc.Location != initPoint)
                {
					CurrentLoc = new Rectangle(newPt, CurrentLoc.Size);
				} else
                {
					moving = false;
					onCooldown = true;
                }
            }

		}

		public void Attack()
		{
			if (!attacking && !moving && !onCooldown)
			{
				dir = Collision.DetectDirection(Game.Player);
				attacking = true;
				moving = true;
			}
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
			if (moving)
            {
				Move();
            }
			if (onCooldown)
            {
				cooldown--;
            }
			if (cooldown <= 0)
            {
				cooldown = MaxCooldown;
				onCooldown = false;
            }
			Collision.Update();
		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}