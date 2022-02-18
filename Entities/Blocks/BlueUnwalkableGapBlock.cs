using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class BlueUnwalkableGapBlock : IBlock
    {
        private ISprite sprite = BlockSpriteFactory.Instance.CreateBlueUnwalkableGapBlock();
        private static int width = 16;
        private static int height = 16;
        public Point CurrentPoint { get; set; }
        public BlueUnwalkableGapBlock(Point position)
        {
            CurrentPoint = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void Update() => sprite.Update();
    }
}

