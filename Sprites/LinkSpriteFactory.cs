using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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
                return Instance;
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
            return new AttackingLeftLink
        }

        public ISprite CreateAttackingRightLink()
        {

        }

        public ISprite CreateAttackingUpLink()
        {

        }

        public ISprite CreateAttackingDownLink()
        {

        }

        public ISprite CreateIdleLeftLink()
        {

        }

        public ISprite CreateIdleRightLink()
        {

        }

        public ISprite CreateIdleUpLink()
        {

        }

        public ISprite CreateIdleDownLink()
        {

        }

        public ISprite CreateUILeftLink()
        {

        }

        public ISprite CreateUIRightLink()
        {

        }

        public ISprite CreateUIUpLink()
        {

        }

        public ISprite CreateUIDownLink()
        {

        }

        public ISprite CreateDamagedLeftLink()
        {

        }

        public ISprite CreateDamagedRightLink()
        {

        }

        public ISprite CreateDamagedUpLink()
        {

        }

        public ISprite CreateDamagedDownLink()
        {

        }

        public ISprite CreateWalkingLeftLink()
        {

        }

        public ISprite CreateWalkingRightLink()
        {

        }

        public ISprite CreateWalkingUpLink()
        {

        }

        public ISprite CreateWalkingDownLink()
        {

        }

    }
}
