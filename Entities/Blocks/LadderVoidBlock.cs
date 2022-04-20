using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class LadderVoidBlock : IBlock // used to emulate gravity
    {
        // notice: no sprite
        public CollisionHeight Height { get => CollisionHeight.Projectile; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public Rectangle CurrentLoc { get; set; }
        public LadderVoidBlock(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Update() { }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

