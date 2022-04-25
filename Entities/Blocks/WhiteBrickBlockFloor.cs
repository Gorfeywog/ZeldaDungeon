using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class WhiteBrickBlockFloor : IBlock
    {

        private ISprite sprite = BlockSpriteFactory.Instance.CreateWhiteBrickBlock();
        public CollisionHeight Height { get => CollisionHeight.Floor; }
        public DrawLayer Layer { get => DrawLayer.Floor; }
        public Rectangle CurrentLoc { get; set; }
        public WhiteBrickBlockFloor(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;

    }
}


