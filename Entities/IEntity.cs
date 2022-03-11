using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IEntity
    {
        public void Draw(SpriteBatch spriteBatch);
        public void Update();

        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get; }
        public void DespawnEffect();
    }
}
