using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IEntity
    {
        public void Draw(SpriteBatch spriteBatch); // maybe reconsider how this fits into Items in inventories?
        public void Update();
        public bool ReadyToDespawn { get; }
        public void DespawnEffect();
    }
}
