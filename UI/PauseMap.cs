using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private ISprite room_UR;
        private ISprite room_UL;
        private ISprite room_LRD;
        private ISprite room_DR;
        private ISprite room_DL;
        public PauseMap()
        {
            room_R = UISpriteFactory.Instance.CreateMapRoomR();
            room_LRUD = UISpriteFactory.Instance.CreateMapRoomLRUD();
            room_L = UISpriteFactory.Instance.CreateMapRoomL();
            room_UD = UISpriteFactory.Instance.CreateMapRoomUD();
            room_UR = UISpriteFactory.Instance.CreateMapRoomUR();
            room_UL = UISpriteFactory.Instance.CreateMapRoomUL();
            room_LRD = UISpriteFactory.Instance.CreateMapRoomLRD();
            room_DR = UISpriteFactory.Instance.CreateMapRoomDR();
            room_DL = UISpriteFactory.Instance.CreateMapRoomDL();
        }

        public void Draw(SpriteBatch spriteBatch, Point gridTopLeft, HUDMapRoomState[,] mapGrid)
        {
            /*for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    var curRoom = mapGrid[i, j];
                    int scaledLen1 = (int)SpriteUtil.SpriteSize.BigMapRoomWidth * SpriteUtil.SCALE_FACTOR;
                    int scaledHeight1 = (int)SpriteUtil.SpriteSize.BigMapRoomHeight * SpriteUtil.SCALE_FACTOR;
                    Point dest1 = gridTopLeft + new Point(i * scaledLen1, j * scaledHeight1);
                    Point size1 = new Point(scaledLen1, scaledHeight1);
                    Rectangle destRect1 = new Rectangle(dest1, size1);
                    if (true)
                    {
                        room_R.Draw(spriteBatch, destRect1);
                    }
                }
            }*/
        }
        public void Update()
        {
            /*room_R.Update();
            room_LRUD.Update();
            room_L.Update();
            room_UD.Update();
            room_UR.Update();
            room_UL.Update();
            room_LRD.Update();
            room_DR.Update();
            room_DL.Update();*/
        }
    }
}
