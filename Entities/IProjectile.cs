﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IProjectile : IDrawable
    {
        public Point CurrentPoint { get; }
        public bool ReadyToDespawn { get; } // maybe even move this to IDrawable? it seems pretty useful
        public void DespawnEffect(); // create explosions, etc.
    }
}