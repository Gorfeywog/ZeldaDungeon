using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using System.Collections.Generic;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Enemies
{
	public class Aquamentus : IEnemy
	{
		public ISprite AquamentusSprite { get; set; }
		public Rectangle CurrentLoc { get; set; }
		private CollisionHandler collision;
		private int initX;

		private bool movingLeft;
		private int currentFrame;
		private Game1 g;


		public Aquamentus(Point position, Game1 g)
		{
			AquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
			int width = (int)SpriteUtil.SpriteSize.AquamentusX;
			int height = (int)SpriteUtil.SpriteSize.AquamentusY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
			collision = new CollisionHandler((List<IEntity>)g.CurrentRoom.roomEntities, this);
			initX = position.X;

			movingLeft = true;
			this.g = g;
			currentFrame = 0;
		}

		public void Move()
		{
			//One in four chance per move for aquamentus to change direction
			int changeDirChance = 4;
			int xLimit = 2;
			int locChange = 4 * SpriteUtil.SCALE_FACTOR;
			if (SpriteUtil.Rand.Next(changeDirChance) == 0)
            {
				movingLeft = !movingLeft;
            }

			if (movingLeft)
            {
				if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size)))
                {
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X - locChange, CurrentLoc.Y), CurrentLoc.Size);
				}
				if (CurrentLoc.X < initX - xLimit * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR)
				{
					movingLeft = !movingLeft;
				}
			} else
            {
				if (!collision.WillHitBlock(new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size)))
                {
					CurrentLoc = new Rectangle(new Point(CurrentLoc.X + locChange, CurrentLoc.Y), CurrentLoc.Size);
				}
				if (CurrentLoc.X > initX + xLimit * (int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR)
				{
					movingLeft = !movingLeft;
				}
			}

		}

		public void Attack()
		{
			int fireballChange = SpriteUtil.Rand.Next(3) - 1;
			int fireballVel = -4;
			IProjectile fireballUp = new Fireball(CurrentLoc.Location, fireballVel, 1 + fireballChange);
			IProjectile fireballStraight = new Fireball(CurrentLoc.Location, fireballVel, fireballChange);
			IProjectile fireballDown = new Fireball(CurrentLoc.Location, fireballVel, -1 + fireballChange);
			g.RegisterProjectile(fireballUp);
			g.RegisterProjectile(fireballStraight);
			g.RegisterProjectile(fireballDown);
		}

		public void TakeDamage()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			AquamentusSprite.Draw(spriteBatch, CurrentLoc);
		}

		public void Update()
		{
			currentFrame++;
			AquamentusSprite.Update();
			int moveChance = 8;
			if (currentFrame % moveChance == 0)
            {
				Move();
            }
			int attackChance = 64;
			int randChance = 4;
			if (currentFrame % attackChance == 0 && SpriteUtil.Rand.Next(randChance) == 0)
            {
				Attack();
            }

		}
		public void DespawnEffect() { }
		public bool ReadyToDespawn => false;
	}
}