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
            return new AttackingLeftLink(linkSpriteSheet, GridToPoint(2,4));
        }

        public ISprite CreateAttackingRightLink()
        {
            return new AttackingRightLink(linkSpriteSheet, GridToPoint(3,4));
        }

        public ISprite CreateAttackingUpLink()
        {
            return new AttackingUpLink(linkSpriteSheet, GridToPoint(0,4));
        }

        public ISprite CreateAttackingDownLink()
        {
            return new AttackingDownLink(linkSpriteSheet, GridToPoint(1,4));
        }
        // Note that Idle sprites just use the first frame of the
        // walk animation, and the UsingItem sprites do the same.
        // This should be changed if possible.
        public ISprite CreateIdleLeftLink()
        {
            return new IdleLeftLink(linkSpriteSheet, GridToPoint(0, 2));
        }

        public ISprite CreateIdleRightLink()
        {
            return new IdleRightLink(linkSpriteSheet, GridToPoint(0, 3));
        }

        public ISprite CreateIdleUpLink()
        {
            return new IdleUpLink(linkSpriteSheet, GridToPoint(0, 0));
        }

        public ISprite CreateIdleDownLink()
        {
            return new IdleDownLink(linkSpriteSheet, GridToPoint(0, 1));
        }

        public ISprite CreateUILeftLink()
        {
            return new UILeftLink(linkSpriteSheet, GridToPoint(0, 2));
        }

        public ISprite CreateUIRightLink()
        {
            return new UIRightLink(linkSpriteSheet, GridToPoint(0, 3));
        }

        public ISprite CreateUIUpLink()
        {
            return new UIUpLink(linkSpriteSheet, GridToPoint(0, 0));
        }

        public ISprite CreateUIDownLink()
        {
            return new UIDownLink(linkSpriteSheet, GridToPoint(0, 1));
        }

        public ISprite CreateDamagedLeftLink()
        {
            return new DamagedLeftLink(linkSpriteSheet, GridToPoint(0, 2));
        }
        public ISprite CreateDamagedRightLink()
        {
            return new DamagedRightLink(linkSpriteSheet, GridToPoint(0, 3));
        }

        public ISprite CreateDamagedUpLink()
        {
            return new DamagedUpLink(linkSpriteSheet, GridToPoint(0, 0));
        }

        public ISprite CreateDamagedDownLink()
        {
            return new DamagedDownLink(linkSpriteSheet, GridToPoint(0, 1));
        }

        public ISprite CreateWalkingLeftLink()
        {
            Point[] topLefts = { GridToPoint(0, 2), GridToPoint(1, 2) };
            return new WalkingLeftLink(linkSpriteSheet, topLefts);
        }

        public ISprite CreateWalkingRightLink()
        {
            Point[] topLefts = { GridToPoint(0, 3), GridToPoint(1, 3) };
            return new WalkingRightLink(linkSpriteSheet, topLefts);
        }

        public ISprite CreateWalkingUpLink()
        {
            Point[] topLefts = { GridToPoint(0, 0), GridToPoint(1, 0) };
            return new WalkingUpLink(linkSpriteSheet, topLefts);
        }

        public ISprite CreateWalkingDownLink()
        {
            Point[] topLefts = { GridToPoint(0, 1), GridToPoint(1, 1) };
            return new WalkingDownLink(linkSpriteSheet, topLefts);
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}
