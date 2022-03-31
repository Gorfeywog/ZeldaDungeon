using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities.Enemies
{
	public class ItemHolder : IEnemy
	{
		public bool ReadyToDespawn { get => Underlying.ReadyToDespawn; }

		public IEnemy Underlying { get; private set; }
		public Rectangle CurrentLoc { get => Underlying.CurrentLoc; set => Underlying.CurrentLoc = value; }
		public CollisionHeight Height => Underlying.Height;
		public DrawLayer Layer => Underlying.Layer;
		private IPickup heldItem { get; set; }
		private Room r;
		public ItemHolder(IEnemy underlying, IPickup held, Room r)
		{
			Underlying = underlying;
			heldItem = held;
			this.r = r;
		}

		public void Move() => Underlying.Move();

		public void Attack() => Underlying.Attack();

		public void TakeDamage() => Underlying.TakeDamage();
		
		public void Draw(SpriteBatch spriteBatch)
        {
			Underlying.Draw(spriteBatch);
			heldItem.Draw(spriteBatch);
		}

		public void Update()
        {
			Underlying.Update();
			heldItem.Update();
			Point size = heldItem.CurrentLoc.Size;
			Point loc = Underlying.CurrentLoc.Location;
			heldItem.CurrentLoc = new Rectangle(loc, size);
		}
		public void DespawnEffect()
		{
			Underlying.DespawnEffect();
			r.RegisterEntity(heldItem);
		}
	}
}