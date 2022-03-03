using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Pickups
{
	public class Candle : IPickup
	{
        private ISprite sprite;
        private Game1 g;
        private bool isRed; // red ones can be used more than once per room
        public Rectangle CurrentLoc { get; set; }
        public Candle(Point position, Game1 g, bool isRed)
        {
            int width = (int)SpriteUtil.SpriteSize.CandleWidth;
			int height = (int)SpriteUtil.SpriteSize.CandleLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g = g;
            this.isRed = isRed;
            sprite = ItemSpriteFactory.Instance.CreateCandle(isRed);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();

        private static int offset = 32;
        public void UseOn(ILink player)
        {
            // uses player.CurrentLoc.Location rather than player.Center since is about the size of Link
            Point loc = EntityUtils.Offset(player.CurrentLoc.Location, player.Direction, offset);
            IProjectile proj = new CandleFire(loc, player.Direction);
            g.RegisterProjectile(proj);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

