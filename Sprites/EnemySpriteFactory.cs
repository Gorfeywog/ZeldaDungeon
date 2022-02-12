using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.EnemySprites;

namespace ZeldaDungeon.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpriteSheet;
        private static readonly int gridX = 32; // how wide each sprite is
        private static readonly int gridY = 32;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();

        
        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpriteSheet = content.Load<Texture2D>("enemysprites");
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}
