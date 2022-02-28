using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Sprites
{
    public class BlockSpriteFactory
    {
        private Texture2D blockSpriteSheet;

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
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(4, 1, 16, 16));
        }
        public ISprite CreateBlueSandBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(1, 1, 16, 16));
        }
        public ISprite CreateBlueUnwalkableGapBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(3, 2, 16, 16));
        }
        public ISprite CreateLadderBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(0, 2, 16, 16));
        }
        public ISprite CreatePushableBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(0, 1, 16, 16));
        }
        public ISprite CreateStairsBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(2, 1, 16, 16));
        }
        public ISprite CreateStatue1Block()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(1, 2, 16, 16));
        }
        public ISprite CreateStatue2Block()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(2, 2, 16, 16));
        }
        public ISprite CreateWhiteBrickBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(3, 1, 16, 16));
        }
        public ISprite CreateFireBlock()
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, 16, 16), SpriteUtil.GridToRectangle(1, 0, 16, 16) };
            return new AnimatedSprite(blockSpriteSheet, sourceRectangles);
        }


    }
}
