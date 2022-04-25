using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.InventoryItems
{
    public class BombItem : IItem
    {
        private Game1 g;
        public bool Consumable { get => true; }
        public BombItem(Game1 g)
        {

            this.g = g;
        }

        private static int offset = 16 * SpriteUtil.SCALE_FACTOR; 
        public void UseOn(ILink player)
        {

            Point loc = EntityUtils.Offset(player.CurrentLoc.Location, player.Direction, offset);
            IProjectile proj = new BombProjectile(loc, g);
            SoundManager.Instance.PlaySound("BombAppearanceAndSelfDestruction");
            g.CurrentRoom.RegisterEntity(proj);
        }

        public bool CanUseOn(ILink player) => true;
        public bool Equals(IItem other)
        {
            if (other is BombItem otherBomb)
            {
                return this.g == otherBomb.g;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return "bomb".GetHashCode() ^ g.GetHashCode();
        }
        public bool Selectable => true;
    }
}

