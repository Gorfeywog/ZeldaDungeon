using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class LadderVoidBlock : IBlock // used to emulate gravity
    {
        // notice: no sprite
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public Rectangle CurrentLoc { get; set; }
        public LadderVoidBlock(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
			int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Update() { }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

