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
                    var curRoom = mapGrid[i, j];
                    int scaledLen1 = (int)SpriteUtil.SpriteSize.SmallMapRoomWidth * SpriteUtil.SCALE_FACTOR;
                    int scaledHeight1 = (int)SpriteUtil.SpriteSize.SmallMapRoomHeight * SpriteUtil.SCALE_FACTOR;
                    Point dest1 = gridTopLeft + new Point(i * scaledLen1, j * scaledHeight1);
                    Point size1 = new Point(scaledLen1, scaledHeight1);
                    Rectangle destRect1 = new Rectangle(dest1, size1);
                    if (curRoom.RoomKnown)
                    {
                        room.Draw(spriteBatch, destRect1);
                    }
                    int scaledOffsetX = (int)SpriteUtil.SpriteSize.SmallMapIndicOffsetX * SpriteUtil.SCALE_FACTOR;
                    int scaledOffsetY = (int)SpriteUtil.SpriteSize.SmallMapIndicOffsetY * SpriteUtil.SCALE_FACTOR;
                    int scaledLen2 = (int)SpriteUtil.SpriteSize.SmallMapIndicatorWidth * SpriteUtil.SCALE_FACTOR;
                    int scaledHeight2 = (int)SpriteUtil.SpriteSize.SmallMapIndicatorHeight * SpriteUtil.SCALE_FACTOR;
                    Point dest2 = dest1 + new Point(scaledOffsetX, scaledOffsetY);
                    Point size2 = new Point(scaledLen2, scaledHeight2);
                    Rectangle destRect2 = new Rectangle(dest2, size2);
                    if (curRoom.HasLink)
                    {
                        linkIndic.Draw(spriteBatch, destRect2);
                    }
                    else if (curRoom.HasTriforce)
                    {
                        triforceIndic.Draw(spriteBatch, destRect2);
                    }
                }
            }
        }
        public void Update()
        {
            linkIndic.Update();
            triforceIndic.Update();
            room.Update();
        }
    }
}
