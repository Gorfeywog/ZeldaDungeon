﻿using System;
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
        private int dim0 = 0, dim1 = 1;
        public HUDMap()
        {
            linkIndic = UISpriteFactory.Instance.CreateSmallMapLinkIndicator();
            triforceIndic = UISpriteFactory.Instance.CreateSmallMapTriforceIndicator();
            room = UISpriteFactory.Instance.CreateSmallMapRoom();
        }
        public void Draw(SpriteBatch spriteBatch, Point gridTopLeft, HUDMapRoomState[,] mapGrid)
        {
            for (int i = 0; i < mapGrid.GetLength(dim0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(dim1); j++)
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
