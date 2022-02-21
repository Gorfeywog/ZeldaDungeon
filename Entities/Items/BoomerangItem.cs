using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Entities.Projectiles;

namespace ZeldaDungeon.Entities.Items
{
	public class BoomerangItem : IItem
	{
        private ISprite sprite;
        private bool isMagic;
        private Game1 g;
        public Point CurrentPoint { get; set; }
        public BoomerangItem(Point position, Game1 g, bool isMagic)
        {
            CurrentPoint = position;
            this.g = g;
            this.isMagic = isMagic;
            if (isMagic)
            {
                sprite = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            }
            else
            {
                sprite = EnemySpriteFactory.Instance.CreateStaticBoomerangSprite();
            }
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
            // TODO - make this work
            g.RegisterProjectile(proj);
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

