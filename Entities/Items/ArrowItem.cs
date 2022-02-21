using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Items
{
	public class ArrowItem : IItem
	{
        private ISprite sprite = ItemSpriteFactory.Instance.CreateArrow(Direction.Up);
        private Game1 g;
        public Point CurrentPoint { get; set; }
        public ArrowItem(Point position, Game1 g)
        {
            CurrentPoint = position;
            this.g=g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void Update() => sprite.Update();

        private static int offset = 32;
        public void UseOn(ILink player)
        {
            Point loc = EntityUtils.Offset(player.Center, player.Direction, offset);
            IProjectile proj = new ArrowProjectile(loc, player.Direction, g);
            g.RegisterProjectile(proj);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

