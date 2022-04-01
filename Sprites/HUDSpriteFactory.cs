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
    class HUDSpriteFactory
    {
        private Texture2D UISpriteSheet;
        private static HUDSpriteFactory instance = new HUDSpriteFactory();
        public static HUDSpriteFactory Instance
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

        public ISprite CreateMapRoom0And7()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(7, 0,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }

        public ISprite CreateMapRoom1And5And9And15()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 1,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }

        public ISprite CreateMapRoom2And14()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 0,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }

        public ISprite CreateMapRoom3And12()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(0, 1,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }
        public ISprite CreateMapRoom4()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(7, 1,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }
        public ISprite CreateMapRoom6And11()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 1,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }
        public ISprite CreateMapRoom8And10()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 0,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }
        public ISprite CreateMapRoom13()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 0,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
        }
        public ISprite CreateMapRoom16()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 0,
                (int)SpriteUtil.SpriteSize.HUDMapRoomWidth, (int)SpriteUtil.SpriteSize.HUDMapRoomHeight));
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
        public ISprite CreateNumber0()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(1, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber1()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(2, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber2()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(3, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber3()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber4()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(5, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber5()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(6, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber6()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(7, 2,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber7()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(4, 3,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber8()
        {
            return new StaticSprite(UISpriteSheet, SpriteUtil.GridToRectangle(5, 3,
                (int)SpriteUtil.SpriteSize.HUDNumberWidth, (int)SpriteUtil.SpriteSize.HUDNumberHeight));
        }
        public ISprite CreateNumber9()
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
    }
}
