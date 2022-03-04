using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Pickups
{
    public class BombPickup : IPickup
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBomb();
        private Game1 g;
        public Rectangle CurrentLoc { get; set; }
        public bool HoldsUp { get => false; }
        public BombPickup(Point position, Game1 g)
        {
            int width = (int)SpriteUtil.SpriteSize.BombWidth;            
            int height = (int)SpriteUtil.SpriteSize.BombLength;            
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g = g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        private static int offset = 32; // how far to place from Link
        public void PickUp(ILink link)
        {
            link.AddItem(new BombItem(g));
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

