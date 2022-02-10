using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.LinkSprites;

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

        private LinkSpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            linkSpriteSheet = content.Load<Texture2D>("File name of Link's SpriteSheet");

        }

        public ISprite CreateAttackingLeftLink()
        {
            return new AttackingLeftLink(linkSpriteSheet);
        }

        public ISprite CreateAttackingRightLink()
        {
            return new AttackingRightLink(linkSpriteSheet);
        }

        public ISprite CreateAttackingUpLink()
        {
            return new AttackingUpLink(linkSpriteSheet);
        }

        public ISprite CreateAttackingDownLink()
        {
            return new AttackingDownLink(linkSpriteSheet);
        }

        public ISprite CreateIdleLeftLink()
        {
            return new IdleLeftLink(linkSpriteSheet);
        }

        public ISprite CreateIdleRightLink()
        {
            return new IdleRightLink(linkSpriteSheet);
        }

        public ISprite CreateIdleUpLink()
        {
            return new IdleUpLink(linkSpriteSheet);
        }

        public ISprite CreateIdleDownLink()
        {
            return new IdleDownLink(linkSpriteSheet);
        }

        public ISprite CreateUILeftLink()
        {
            return new UILeftLink(linkSpriteSheet);
        }

        public ISprite CreateUIRightLink()
        {
            return new UIRightLink(linkSpriteSheet);
        }

        public ISprite CreateUIUpLink()
        {
            return new UIUpLink(linkSpriteSheet);
        }

        public ISprite CreateUIDownLink()
        {
            return new UIDownLink(linkSpriteSheet);
        }

        public ISprite CreateDamagedLeftLink()
        {
            return new DamagedLeftLink(linkSpriteSheet);
        }

        public ISprite CreateDamagedRightLink()
        {
            return new DamagedRightLink(linkSpriteSheet);
        }

        public ISprite CreateDamagedUpLink()
        {
            return new DamagedUpLink(linkSpriteSheet);
        }

        public ISprite CreateDamagedDownLink()
        {
            return new DamagedDownLink(linkSpriteSheet);
        }

        public ISprite CreateWalkingLeftLink()
        {
            return new WalkingLeftLink(linkSpriteSheet);
        }

        public ISprite CreateWalkingRightLink()
        {
            return new WalkingRightLink(linkSpriteSheet);
        }

        public ISprite CreateWalkingUpLink()
        {
            return new WalkingUpLink(linkSpriteSheet);
        }

        public ISprite CreateWalkingDownLink()
        {
            return new WalkingDownLink(linkSpriteSheet);
        }

    }
}
