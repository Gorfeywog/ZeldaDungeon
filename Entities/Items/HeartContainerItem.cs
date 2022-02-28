using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Items
{
    public class HeartContainerItem : IItem
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateHeartContainer();
        private static int width = 16;
        private static int height = 16;
        public Rectangle CurrentLoc { get; set; }
        public HeartContainerItem(Point position)
        {
            CurrentLoc = new Rectangle(position, new Point(13, 13));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void UseOn(ILink player)
        {
            // do nothing, for now
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
