using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Pickups
{
	public class ArrowPickup : IPickup
	{
        private ISprite sprite = ItemSpriteFactory.Instance.CreateArrow(Direction.Up);
        private Game1 g;
        public Rectangle CurrentLoc { get; set; }
        public ArrowPickup(Point position, Game1 g)
        {
            int width = (int)SpriteUtil.SpriteSize.ArrowWidth;
			int height = (int)SpriteUtil.SpriteSize.ArrowLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g=g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();

        private static int offset = 32;
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

