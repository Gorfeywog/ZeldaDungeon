using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Pickups
{
    public class TriforcePiecePickup : IPickup
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateTriforcePiece();
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        public bool HoldsUp { get => true; }
        public TriforcePiecePickup(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.TriforceWidth;
            int height = (int)SpriteUtil.SpriteSize.TriforceLength;
            int offset = 8;
            CurrentLoc = new Rectangle(new Point(position.X + (offset * SpriteUtil.SCALE_FACTOR), position.Y),
                new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void PickUp(ILink player)
        {
            // do nothing, for now
        }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}
