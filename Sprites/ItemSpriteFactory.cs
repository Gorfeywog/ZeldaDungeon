using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.ItemSprites;

namespace ZeldaDungeon.Sprites
{
    public class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;
        private static readonly int gridX = 32; // how wide each sprite is
        private static readonly int gridY = 32;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        
        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            itemSpriteSheet = content.Load<Texture2D>("itemsprites");
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}
