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
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBomb(); // TODO: Check with Luke that this is correct.
        private static int width = 16;
        private static int height = 16;
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
        private static int offset = 16; // how far to place from Link
        public void UseOn(ILink player)
        {
            int x = player.Position.X;
            int y = player.Position.Y;
            switch (player.Direction)
            {
                case Direction.Up:
                    y -= offset;
                    break;
                case Direction.Down:
                    y += offset;
                    break;
                case Direction.Left:
                    x -= offset;
                    break;
                case Direction.Right:
                    x += offset;
                    break;
            }
            IProjectile proj = new BombProjectile(new Point(x, y), g);
            g.RegisterProjectile(proj);
        }
    }
}
