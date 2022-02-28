using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Items
{
	public class Candle : IItem
	{
        private ISprite sprite;
        private Game1 g;
        private bool isRed; // red ones can be used more than once per room
        public Rectangle CurrentLoc { get; set; }
        public Candle(Point position, Game1 g, bool isRed)
        {
            CurrentLoc = new Rectangle(position, new Point(8, 16));
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

