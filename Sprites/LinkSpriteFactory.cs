using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.LinkSprites;

namespace ZeldaDungeon.Sprites
{
    public class LinkSpriteFactory
    {
        private Texture2D linkSpriteSheet;
        private static readonly int gridX = 32; // how wide each sprite is
        private static readonly int gridY = 32;

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
        // note that these 4 are currently unused; they may return at some point?
        public ISprite CreateAttackingLeftLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 27, 16, GridToPoint(2,4), damaged);
        }

        public ISprite CreateAttackingRightLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 27, 16, GridToPoint(3,4), damaged);
        }

        public ISprite CreateAttackingUpLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 27, GridToPoint(0,4), damaged);
        }

        public ISprite CreateAttackingDownLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 27, GridToPoint(1,4), damaged);
        }
        // Note that Idle sprites just use the first frame of the
        // walk animation.
        // This should be changed if possible.
        public ISprite CreateIdleLeftLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 2), damaged);
        }

        public ISprite CreateIdleRightLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 3), damaged);
        }

        public ISprite CreateIdleUpLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 0), damaged);
        }

        public ISprite CreateIdleDownLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 1), damaged);
        }

        public ISprite CreateUILeftLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(3, 5), damaged);
        }

        public ISprite CreateUIRightLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(4, 5), damaged);
        }

        public ISprite CreateUIUpLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(5, 5), damaged);
        }

        public ISprite CreateUIDownLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(2, 5), damaged);
        }
        /*
        public ISprite CreateDamagedLeftLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 2), true);
        }
        public ISprite CreateDamagedRightLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 3), true);
        }

        public ISprite CreateDamagedUpLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 0), true);
        }

        public ISprite CreateDamagedDownLink(bool damaged = false)
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 1), true);
        }
        */ // obsoleted; Damaged is a modifier now
        public ISprite CreateWalkingLeftLink(bool damaged = false)
        {
            Point[] topLefts = { GridToPoint(0, 2), GridToPoint(1, 2) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts, damaged);
        }

        public ISprite CreateWalkingRightLink(bool damaged = false)
        {
            Point[] topLefts = { GridToPoint(0, 3), GridToPoint(1, 3) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts, damaged);
        }

        public ISprite CreateWalkingUpLink(bool damaged = false)
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts, damaged);
        }

        public ISprite CreateWalkingDownLink(bool damaged = false)
        {
            Point[] topLefts = { GridToPoint(0, 1), GridToPoint(1, 1) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts, damaged);
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}
