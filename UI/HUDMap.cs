using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    class HUDMap
    {
        private ISprite linkIndic;
        private ISprite triforceIndic;
        private ISprite room;
        public HUDMap()
        {
            linkIndic = HUDSpriteFactory.Instance.CreateSmallMapLinkIndicator();
            triforceIndic = HUDSpriteFactory.Instance.CreateSmallMapTriforceIndicator();
            room = HUDSpriteFactory.Instance.CreateSmallMapRoom();
        }
        public void Draw(SpriteBatch spriteBatch, Point gridTopLeft, MapRoomState[,] mapGrid)
        {
            for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    int scaledLen = (int)SpriteUtil.SpriteSize.SmallMapRoomWidth * SpriteUtil.SCALE_FACTOR;
                    int scaledHeight = (int)SpriteUtil.SpriteSize.SmallMapRoomHeight * SpriteUtil.SCALE_FACTOR;
                    Point dest1 = gridTopLeft + new Point(i * scaledLen, j * scaledHeight);
                    Point size1 = new Point(scaledLen, scaledHeight);
                    Rectangle destRect1 = new Rectangle(dest, size);
                    
                }
            }
        }
    }
}
