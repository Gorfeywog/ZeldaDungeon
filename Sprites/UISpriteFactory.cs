using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;


namespace ZeldaDungeon.Sprites
{
    // this holds all the sprites necessary to display hearts, maps, etc. that will go onto the HUD.
    class UISpriteFactory
    {
        private Texture2D UISpriteSheet;
        private static UISpriteFactory instance = new UISpriteFactory();
        public static UISpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadAllTextures(ContentManager content)
        {
            UISpriteSheet = content.Load<Texture2D>("uisprites");
        }

        public ISprite CreateMapRoomR()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(7, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }

        public ISprite CreateMapRoomLRUD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }

        public ISprite CreateMapRoomL()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }

        public ISprite CreateMapRoomUD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(0, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomRU()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(7, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomLU()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomLRD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomRD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomLD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomNone()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(6, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomLR()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(5, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(0, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomU()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(6, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomDL()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 0,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomLRU()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(5, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomRUD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateMapRoomLUD()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 1,
                (int)SpriteUtil.SpriteSize.BigMapRoomWidth, (int)SpriteUtil.SpriteSize.BigMapRoomHeight));
        }
        public ISprite CreateX()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(0, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateA()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 3,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber(int n)
        {
            return n switch
            {
                0 => CreateNumber0(),
                1 => CreateNumber1(),
                2 => CreateNumber2(),
                3 => CreateNumber3(),
                4 => CreateNumber4(),
                5 => CreateNumber5(),
                6 => CreateNumber6(),
                7 => CreateNumber7(),
                8 => CreateNumber8(),
                9 => CreateNumber9(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        private ISprite CreateNumber0()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber1()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber2()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber3()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber4()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(5, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber5()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(6, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber6()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(7, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber7()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 3,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber8()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(5, 3,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        private ISprite CreateNumber9()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(6, 3,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateEmptyHeart()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(0, 3,
                (int)SpriteUtil.SpriteSize.HUDHeartWidth, (int)SpriteUtil.SpriteSize.HUDHeartHeight));
        }
        public ISprite CreateFullHeart()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 3,
                (int)SpriteUtil.SpriteSize.HUDHeartWidth, (int)SpriteUtil.SpriteSize.HUDHeartHeight));
        }
        public ISprite CreateHalfHeart()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 3,
                (int)SpriteUtil.SpriteSize.HUDHeartWidth, (int)SpriteUtil.SpriteSize.HUDHeartHeight));
        }
        public ISprite CreateSmallMapRoom()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(0, 4,
                (int)SpriteUtil.SpriteSize.SmallMapRoomWidth, (int)SpriteUtil.SpriteSize.SmallMapRoomHeight));
        }
        public ISprite CreateSmallMapLinkIndicator()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 4,
                (int)SpriteUtil.SpriteSize.SmallMapRoomWidth, (int)SpriteUtil.SpriteSize.SmallMapRoomHeight));
        }
        public ISprite CreateSmallMapTriforceIndicator()
        {
            int wd = (int)SpriteUtil.SpriteSize.SmallMapRoomWidth;
            int ht = (int)SpriteUtil.SpriteSize.SmallMapRoomHeight;
            Rectangle[] sources = { SpriteUtil.GridToRectangle(2, 4, wd, ht), SpriteUtil.GridToRectangle(3, 4, wd, ht) };
            return new AnimatedSprite(UISpriteSheet, sources);
        }
    }
}
