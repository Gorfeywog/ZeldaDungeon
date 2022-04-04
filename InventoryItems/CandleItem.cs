using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    public class CandleItem : IItem
	{
        public bool Consumable { get => false; }
        private Game1 g;
        private bool isRed; // red ones can be used more than once per room
        public CandleItem(Game1 g, bool isRed)
        {
            this.g = g;
            this.isRed = isRed;
        }
        private static int offset = SpriteUtil.SCALE_FACTOR;
        public void UseOn(ILink player)
        {
            SoundManager.Instance.PlaySound("FlamesShot");
            // uses player.CurrentLoc.Location rather than player.Center since is about the size of Link
            Point loc = EntityUtils.Offset(player.CurrentLoc.Location, player.Direction, offset);
            IProjectile proj = new CandleFire(loc, player.Direction);
            g.CurrentRoom.RegisterEntity(proj);
        }

        public bool CanUseOn(ILink player) => true; 
        public bool Equals(IItem other)
        {
            if (other is CandleItem otherCandle)
            {
                return this.g == otherCandle.g && (this.isRed == otherCandle.isRed);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return "candle".GetHashCode() ^ g.GetHashCode() ^ isRed.GetHashCode();
        }
    }
}

