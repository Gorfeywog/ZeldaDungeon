using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Pickups
{
    public class HeartPickup : IPickup
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateHeart();
        private static int width = 16;
        private static int height = 16;
        public Rectangle CurrentLoc { get; set; }
        public HeartPickup(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.HeartWidth;
			int height = (int)SpriteUtil.SpriteSize.HeartLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void PickUp(ILink player)
        {
            // heal link, once that's a thing we can do
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
