using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Sprites
{
    // this stores all sprites that are oversized and in their own files, or otherwise don't belong in a sheet.
    // so far this is only the room walls.
    public class SpecialSpriteFactory
    {
        private Texture2D wallsSprite;

        private static SpecialSpriteFactory instance = new SpecialSpriteFactory();

        
        public static SpecialSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SpecialSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            wallsSprite = content.Load<Texture2D>("roomwalls");
        }

        public ISprite CreateWalls()
        {
            return new StaticSprite(wallsSprite, new Rectangle(0, 0, SpriteUtil.ROOM_WIDTH, SpriteUtil.ROOM_HEIGHT));
        }
    }
}
