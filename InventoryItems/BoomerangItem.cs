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
    public class BoomerangItem : IItem
	{
        public bool Consumable { get => true; }
        public bool IsMagic { get; private set; }
        private Game1 g;
        public BoomerangItem(Game1 g, bool isMagic)
        {

            this.g = g;
            this.IsMagic = isMagic;
        }
        private static int offset = 8 * SpriteUtil.SCALE_FACTOR;
        public void UseOn(ILink player)
        {
            if (IsMagic)
            {
                SoundManager.Instance.PlaySound("UpgradedMagicalBoomerangThrown");
            } else
            {
                SoundManager.Instance.PlaySound("MagicalBoomerangThrown");
            }
            Point loc = EntityUtils.Offset(player.Center, player.Direction, offset);
            IProjectile proj = new BoomerangProjectile(player, player.Direction, IsMagic, g);
            g.CurrentRoom.RegisterEntity(proj);
        }

        public bool CanUseOn(ILink player) => true;
        public bool Equals(IItem other)
        {
            if (other is BoomerangItem otherBoom)
            {
                return this.g == otherBoom.g && (this.IsMagic == otherBoom.IsMagic);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return "boomerang".GetHashCode() ^ g.GetHashCode() ^ IsMagic.GetHashCode();
        }
        public bool Selectable => true;
    }
}

