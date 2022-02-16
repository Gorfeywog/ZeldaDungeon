﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Items
{
    public class TriforcePieceItem : IItem
    {
        private ISprite sprite = ItemSpriteFactory.Instance.CreateTriforcePiece(); // TODO: Check with Luke that this is correct.
        private static int width = 16;
        private static int height = 16;
        public Point CurrentPoint { get; set; }
        public TriforcePieceItem(Point position)
        {
            CurrentPoint = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void Update() => sprite.Update();
        public void UseOn(ILink player)
        {
            // do nothing, for now
        }
    }
}
