using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    class BlockSpriteFactory
    {
        private static BlockSpriteFactory instance = new BlockSpriteFactory();
        private static BlockSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private BlockSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {

        }
    }
}
