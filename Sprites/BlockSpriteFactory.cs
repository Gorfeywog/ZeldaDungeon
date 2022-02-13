using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.BlockSprites;

namespace ZeldaDungeon.Sprites
{
    public class BlockSpriteFactory
    {
        private Texture2D blockSpriteSheet;
        private static readonly int gridX = 32; // how wide each sprite is
        private static readonly int gridY = 32;

        private static BlockSpriteFactory instance = new BlockSpriteFactory();

        
        public static BlockSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private BlockSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            blockSpriteSheet = content.Load<Texture2D>("blocksprites");
        }

        public ISprite CreateBlueFloorBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(4, 1));
        }
        public ISprite CreateBlueSandBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(1, 1));
        }
        public ISprite CreateBlueUnwalkableGapBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(3, 2));
        }
        public ISprite CreateLadderBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(1, 2));
        }
        public ISprite CreatePushableBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(0, 1));
        }
        public ISprite CreateStairsBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(2, 1));
        }
        public ISprite CreateStatue1Block()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(1, 2));
        }
        public ISprite CreateStatue2Block()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(2, 2));
        }
        public ISprite CreateWhiteBrickBlock()
        {
            return new StaticBlockSprite(blockSpriteSheet, 16, 16, GridToPoint(3, 1));
        }
        public ISprite CreateFireBlock()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(0, 1) };
            return new AnimatedBlockSprite(blockSpriteSheet, 16, 16, topLefts);
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}
