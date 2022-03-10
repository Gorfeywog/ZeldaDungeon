using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Pickups
{
    public class BowPickup : IPickup
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBow();

        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        public bool HoldsUp { get => true; }
        public BowPickup(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.BowWidth;
			int height = (int)SpriteUtil.SpriteSize.BowLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void PickUp(ILink player)
        {
            player.AddItem(new BowItem());
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
