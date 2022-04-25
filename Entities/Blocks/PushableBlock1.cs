using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class PushableBlock1 : IBlock
    {
        private ISprite sprite = BlockSpriteFactory.Instance.CreatePushableBlock1();
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public CollisionHandler Collision { get; private set; }
        private static readonly int MAX_HOLD_TIME = 60;
        private bool CanBeMoved { get => amountMoved < 2 * (int)SpriteUtil.SpriteSize.GenericBlockY && lengthHeld >= MAX_HOLD_TIME; }
        private int amountMoved;
        private int lengthHeld;

        public DrawLayer Layer { get => DrawLayer.Normal; }
        public Rectangle CurrentLoc { get; set; }
        private Rectangle newLoc;
        public PushableBlock1(Point position, Room r)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            amountMoved = 0;
        }
        public void Move(Direction direction)
        {
            newLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, direction, SpriteUtil.SCALE_FACTOR), CurrentLoc.Size);
            CurrentLoc = newLoc;
        }

        public void InitMovement(Direction direction)
        {
            lengthHeld++;
            if (CanBeMoved)
            {
                Move(direction);
                amountMoved++;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update()
        {
            sprite.Update();
        }

        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

