using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Items
{
	public class MagicArrowItem : IItem
	{
        private ISprite sprite = ItemSpriteFactory.Instance.CreateMagicArrow(Direction.Up);
        private Game1 g;
        public Point CurrentPoint { get; set; }
        public MagicArrowItem(Point position, Game1 g)
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
            Point loc = EntityUtils.Offset(player.Position, player.Direction, offset);
            IProjectile proj = new MagicArrowProjectile(loc, player.Direction);
            g.RegisterProjectile(proj);
        }
    }
}

