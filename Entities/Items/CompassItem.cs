using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Items
{
    public class CompassItem : IItem
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateCompass();
        private static int width = 16;
        private static int height = 16;
        public Rectangle CurrentLoc { get; set; }
        public CompassItem(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.CompassWidth;
			int height = (int)SpriteUtil.SpriteSize.CompassLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
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
