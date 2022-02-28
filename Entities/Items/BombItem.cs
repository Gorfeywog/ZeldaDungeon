using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Items
{
    public class BombItem : IItem
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBomb();
        private Game1 g;
        public Rectangle CurrentLoc { get; set; }
        public BombItem(Point position, Game1 g)
        {
            CurrentLoc = new Rectangle(position, new Point(8, 14));
            this.g = g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        private static int offset = 32; // how far to place from Link
        public void UseOn(ILink player)
        {
            // uses player.Position rather than player.Center since is about the size of Link
            Point loc = EntityUtils.Offset(player.CurrentLoc.Location, player.Direction, offset);
            IProjectile proj = new BombProjectile(loc, g);
            g.RegisterProjectile(proj);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
