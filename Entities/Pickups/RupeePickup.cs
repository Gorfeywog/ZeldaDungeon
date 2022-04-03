using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Pickups
{
    public class RupeePickup : IPickup
    {
        private ISprite sprite;

        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        public bool HoldsUp { get => false; }
        public int Quantity { get; private set; }
        public RupeePickup(Point position, int quantity)
        {
            int width = (int)SpriteUtil.SpriteSize.RupeeWidth;
			int height = (int)SpriteUtil.SpriteSize.RupeeLength;
            Quantity = quantity;
            sprite = (Quantity >= 5) ? ItemSpriteFactory.Instance.Create5Rupees() : ItemSpriteFactory.Instance.CreateRupee();
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void PickUp(ILink player)
        {
            player.AddItem(new RupeeItem(), Quantity);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
