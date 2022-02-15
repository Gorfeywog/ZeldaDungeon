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
        // up, down, left, right
        public ISprite CreateAttackingLeftLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 27, 16, GridToPoint(2,4));
        }

        public ISprite CreateAttackingRightLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 27, 16, GridToPoint(3,4));
        }

        public ISprite CreateAttackingUpLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 27, GridToPoint(0,4));
        }

        public ISprite CreateAttackingDownLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 27, GridToPoint(1,4));
        }
        // Note that Idle sprites just use the first frame of the
        // walk animation.
        // This should be changed if possible.
        public ISprite CreateIdleLeftLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 2));
        }

        public ISprite CreateIdleRightLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 3));
        }

        public ISprite CreateIdleUpLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 0));
        }

        public ISprite CreateIdleDownLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 1));
        }

        public ISprite CreateUILeftLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(3, 5));
        }

        public ISprite CreateUIRightLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(4, 5));
        }

        public ISprite CreateUIUpLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(5, 5));
        }

        public ISprite CreateUIDownLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(2, 5));
        }

        public ISprite CreateDamagedLeftLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 2), Color.Red);
        }
        public ISprite CreateDamagedRightLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 3), Color.Red);
        }

        public ISprite CreateDamagedUpLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 0), Color.Red);
        }

        public ISprite CreateDamagedDownLink()
        {
            return new StaticLinkSprite(linkSpriteSheet, 16, 16, GridToPoint(0, 1), Color.Red);
        }

        public ISprite CreateWalkingLeftLink()
        {
            Point[] topLefts = { GridToPoint(0, 2), GridToPoint(1, 2) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateWalkingRightLink()
        {
            Point[] topLefts = { GridToPoint(0, 3), GridToPoint(1, 3) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateWalkingUpLink()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts);
        }

        public ISprite CreateWalkingDownLink()
        {
            Point[] topLefts = { GridToPoint(0, 1), GridToPoint(1, 1) };
            return new AnimatedLinkSprite(linkSpriteSheet, 16, 16, topLefts);
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}
