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
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(4, 1, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateBlueSandBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(1, 1, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateBlueUnwalkableGapBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(3, 2, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateLadderBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(0, 2, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreatePushableBlock1()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(0, 1, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreatePushableBlock2()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(0, 1,
                (int)SpriteUtil.SpriteSize.GenericBlockX, (int)SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateStairsBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(2, 1, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateStatue1Block()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(1, 2, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateStatue2Block()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(2, 2, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateWhiteBrickBlock()
        {
            return new StaticSprite(blockSpriteSheet, SpriteUtil.GridToRectangle(3, 1, 
                (int) SpriteUtil.SpriteSize.GenericBlockX, (int) SpriteUtil.SpriteSize.GenericBlockY));
        }
        public ISprite CreateFireBlock()
        {
            int width = (int) SpriteUtil.SpriteSize.GenericBlockX;
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, width, height), 
                SpriteUtil.GridToRectangle(1, 0, width, height) };
            return new AnimatedSprite(blockSpriteSheet, sourceRectangles);
        }


    }
}
