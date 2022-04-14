using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    class PauseMap
    {
        private ISprite room_R;
        private ISprite room_LRUD;
        private ISprite room_L;
        private ISprite room_UD;
        private ISprite room_RU;
        private ISprite room_LU;
        private ISprite room_LRD;
        private ISprite room_RD;
        private ISprite room_LD;
        private ISprite room_None;
        private ISprite room_D;
        private ISprite room_U;
        private ISprite room_DL;
        private ISprite room_LUD;
        private ISprite room_RUD;
        private ISprite room_LRU;
        private ISprite room_LR;
        public PauseMap()
        {
            room_L = UISpriteFactory.Instance.CreateMapRoomL();
            room_R = UISpriteFactory.Instance.CreateMapRoomR();
            room_LRUD = UISpriteFactory.Instance.CreateMapRoomLRUD();
            room_UD = UISpriteFactory.Instance.CreateMapRoomUD();
            room_RU = UISpriteFactory.Instance.CreateMapRoomRU();
            room_LU = UISpriteFactory.Instance.CreateMapRoomLU();
            room_LRD = UISpriteFactory.Instance.CreateMapRoomLRD();
            room_RD = UISpriteFactory.Instance.CreateMapRoomRD();
            room_LD = UISpriteFactory.Instance.CreateMapRoomLD();
            room_None = UISpriteFactory.Instance.CreateMapRoomNone();
            room_D = UISpriteFactory.Instance.CreateMapRoomD();
            room_U = UISpriteFactory.Instance.CreateMapRoomU();
            room_DL = UISpriteFactory.Instance.CreateMapRoomDL();
            room_LUD = UISpriteFactory.Instance.CreateMapRoomLUD();
            room_RUD = UISpriteFactory.Instance.CreateMapRoomRUD();
            room_LRU = UISpriteFactory.Instance.CreateMapRoomLRU();
            room_LR = UISpriteFactory.Instance.CreateMapRoomLR();
        }

        public void Draw(SpriteBatch spriteBatch, Point gridTopLeft, PauseMapRoomState[,] mapGrid)
        {
            for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    var curRoom = mapGrid[i, j];
                    int scaledLen = (int)SpriteUtil.SpriteSize.BigMapRoomWidth * SpriteUtil.SCALE_FACTOR;
                    int scaledHeight = (int)SpriteUtil.SpriteSize.BigMapRoomHeight * SpriteUtil.SCALE_FACTOR;
                    Point dest = gridTopLeft + new Point(i * scaledLen, j * scaledHeight);
                    Point size = new Point(scaledLen, scaledHeight);
                    Rectangle destRect = new Rectangle(dest, size);
                    if (curRoom.RoomKnown)
                    {
                        SpriteFor(curRoom).Draw(spriteBatch, destRect);
                        Debug.WriteLine(curRoom);
                    }
                }
            }
        }
        public void Update()
        {
            room_L.Update();
            room_R.Update();
            room_LRUD.Update();
            room_UD.Update();
            room_RU.Update();
            room_LU.Update();
            room_LRD.Update();
            room_RD.Update();
            room_LD.Update();
            room_None.Update();
            room_D.Update();
            room_U.Update();
            room_DL.Update();
            room_LUD.Update();
            room_RUD.Update();
            room_LRU.Update();
            room_LR.Update();;
        }
        // only handles directions, not visibility
        // looks like a lot of code, but is basically just a binary search
        // either we have this here or we have basically the same code in UISpriteFactory instead
        private ISprite SpriteFor(PauseMapRoomState state) 
        {
            if (state.HasDoor(Direction.Left))
            {
                if (state.HasDoor(Direction.Right))
                {
                    if (state.HasDoor(Direction.Up))
                    {
                        return state.HasDoor(Direction.Down) ? room_LRUD : room_LRU;
                    }
                    else
                    {
                        return state.HasDoor(Direction.Down) ? room_LRD : room_LR;
                    }
                }
                else
                {
                    if (state.HasDoor(Direction.Up))
                    {
                        return state.HasDoor(Direction.Down) ? room_LUD : room_LU;
                    }
                    else
                    {
                        return state.HasDoor(Direction.Down) ? room_LD : room_L;
                    }
                }
            }
            else
            {
                if (state.HasDoor(Direction.Right))
                {
                    if (state.HasDoor(Direction.Up))
                    {
                        return state.HasDoor(Direction.Down) ? room_RUD : room_RU;
                    }
                    else
                    {
                        return state.HasDoor(Direction.Down) ? room_RD : room_R;
                    }
                }
                else
                {
                    if (state.HasDoor(Direction.Up))
                    {
                        return state.HasDoor(Direction.Down) ? room_UD : room_U;
                    }
                    else
                    {
                        return state.HasDoor(Direction.Down) ? room_D : room_None;
                    }
                }
            }
        }
    }
}
