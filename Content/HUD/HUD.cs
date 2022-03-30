using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaDungeon.Sprites
{
    class HUD : ISprite
    {
        private ISprite HUD_sprite = SpecialSpriteFactory.Instance.CreateHUD();

        public void Draw(SpriteBatch spriteBatch, Rectangle sourceRectangle)
        {
            HUD_sprite.Draw(spriteBatch, sourceRectangle);
        }

        public void Update()
        {
            HUD_sprite.Update();
            // this will change as more in the HUD is implemented. As of now, we're just displaying a static sprite that doesn't change at all.
        }
    }
}
