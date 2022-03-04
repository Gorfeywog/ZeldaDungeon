using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Sprites
{
    public class LinkSpriteFactory
    {
        private Texture2D linkSpriteSheet;

        private static LinkSpriteFactory instance = new LinkSpriteFactory();

        public static LinkSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private LinkSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            linkSpriteSheet = content.Load<Texture2D>("linksprites");
        }
        public ISprite CreateAttackingLeftLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(2,4, 27, 16), damaged);
        }

        public ISprite CreateAttackingRightLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(3,4, 27, 16), damaged);
        }

        public ISprite CreateAttackingUpLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(0,4, 16, 27), damaged);
        }

        public ISprite CreateAttackingDownLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(1,4, 16, 27), damaged);
        }
        public ISprite CreateIdleLeftLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(0, 2, 16, 16), damaged);
        }

        public ISprite CreateIdleRightLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(0, 3, 16, 16), damaged);
        }

        public ISprite CreateIdleUpLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(0, 0, 16, 16), damaged);
        }

        public ISprite CreateIdleDownLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(0, 1, 16, 16), damaged);
        }

        public ISprite CreateUILeftLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(3, 5, 16, 16), damaged);
        }

        public ISprite CreateUIRightLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(4, 5, 16, 16), damaged);
        }

        public ISprite CreateUIUpLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(5, 5, 16, 16), damaged);
        }

        public ISprite CreateUIDownLink(bool damaged = false)
        {
            return new StaticSprite(linkSpriteSheet, SpriteUtil.GridToRectangle(2, 5, 16, 16), damaged);
        }
        public ISprite CreateWalkingLeftLink(bool damaged = false)
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 2, 16, 16), SpriteUtil.GridToRectangle(1, 2, 16, 16) };
            return new AnimatedSprite(linkSpriteSheet, sourceRectangles, damaged);
        }

        public ISprite CreateWalkingRightLink(bool damaged = false)
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 3, 16, 16), SpriteUtil.GridToRectangle(1, 3, 16, 16) };
            return new AnimatedSprite(linkSpriteSheet, sourceRectangles, damaged);
        }

        public ISprite CreateWalkingUpLink(bool damaged = false)
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 0, 16, 16), SpriteUtil.GridToRectangle(1, 0, 16, 16) };
            return new AnimatedSprite(linkSpriteSheet, sourceRectangles, damaged);
        }

        public ISprite CreateWalkingDownLink(bool damaged = false)
        {
            Rectangle[] sourceRectangles = { SpriteUtil.GridToRectangle(0, 1, 16, 16), SpriteUtil.GridToRectangle(1, 1, 16, 16) };
            return new AnimatedSprite(linkSpriteSheet, sourceRectangles, damaged);
        }
        private static Random r = new Random();
        private static int evilChance = 10;
        public ISprite CreatePickupLink(bool damaged = false)
        {
            Rectangle sourceRectangle;
            if (r.Next(evilChance) == 0)
            {
                sourceRectangle = SpriteUtil.GridToRectangle(2, 3, 16, 16);
            }
            else
            {
                sourceRectangle = SpriteUtil.GridToRectangle(3, 3, 16, 16);
            }
            return new StaticSprite(linkSpriteSheet, sourceRectangle, damaged);
        }
    }
}
