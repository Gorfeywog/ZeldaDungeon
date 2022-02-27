﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites.BlockSprites;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Sprites
{
    public class DoorSpriteFactory
    {
        private Texture2D doorSpriteSheet;
        private static readonly int gridX = 32;
        private static readonly int gridY = 32;

        private static DoorSpriteFactory instance = new DoorSpriteFactory();

        
        public static DoorSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private DoorSpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            doorSpriteSheet = content.Load<Texture2D>("doorsprites");
        }

        public ISprite CreateDoor(Direction dir, DoorState state) 
        {
            // we abuse the grid layout to simplify the selection / avoid a nested switch
            int directionIndex = dir switch
            {
                Direction.Up => 0,
                Direction.Left => 1,
                Direction.Right => 2,
                Direction.Down => 3,
                _ => throw new ArgumentException()
            };
            int stateIndex = state switch
            {
                DoorState.None => 0,
                DoorState.Open => 1,
                DoorState.Locked => 2,
                DoorState.Closed => 3,
                DoorState.Hole => 4,
                _ => throw new ArgumentException()
            };
            return new StaticBlockSprite(doorSpriteSheet, 32, 32, GridToPoint(stateIndex, directionIndex));
        }

        private static Point GridToPoint(int x, int y) // convert grid position to position in pixels
        {
            return new Point(gridX * x, gridY * y);
        }
    }
}