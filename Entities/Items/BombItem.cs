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
        public Point CurrentPoint { get; set; }
        public BombItem(Point position, Game1 g)
        {
            CurrentPoint = position;
            this.g = g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void Update() => sprite.Update();
        private static int offset = 32; // how far to place from Link
        public void UseOn(ILink player)
        {
            Point loc = EntityUtils.Offset(player.Position, player.Direction, offset);
            IProjectile proj = new BombProjectile(loc, g);
            g.RegisterProjectile(proj);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
