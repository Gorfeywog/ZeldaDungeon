﻿using Microsoft.Xna.Framework;
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
        public bool IsRed { get; private set; } 
        public CandleItem(Game1 g, bool isRed)
        {
            this.g = g;
            this.IsRed = isRed;
        }
        private static int offset = 16 * SpriteUtil.SCALE_FACTOR;
        public void UseOn(ILink player)
        {
            SoundManager.Instance.PlaySound("FlamesShot");
            Point loc = EntityUtils.Offset(player.CurrentLoc.Location, player.Direction, offset);
            IProjectile proj = new CandleFire(loc, player.Direction);
            g.CurrentRoom.RegisterEntity(proj);
            if (!IsRed)
            {
                g.CurrentRoom.HaveUsedCandle = true;
            }
        }

        public bool CanUseOn(ILink player) => IsRed || !g.CurrentRoom.HaveUsedCandle; 
        public bool Equals(IItem other)
        {
            if (other is CandleItem otherCandle)
            {
                return this.g == otherCandle.g && (this.IsRed == otherCandle.IsRed);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return "candle".GetHashCode() ^ g.GetHashCode() ^ IsRed.GetHashCode();
        }
        public bool Selectable => true;
    }
}

