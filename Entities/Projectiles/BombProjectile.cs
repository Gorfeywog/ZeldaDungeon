﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    public class BombProjectile : IProjectile
    {
        private static int fuseTime = 300; // chosen with no methodology
        private bool isCloud = false;
        private int timer = fuseTime; // counts down
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBomb(); // should projectiles be on their own spritesheet (and thus sprite factory)?
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get => timer <= 0; }
        private Game1 g;
        public BombProjectile(Point position, Game1 g)
        {
            CurrentLoc = new Rectangle(position, new Point(8, 14));
            this.g = g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        private Direction[] doorDirections = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        private const float explodeDist = 10; // chosen arbitrarily - should be tweaked
        public void DespawnEffect()
        {
            g.RegisterProjectile(new SmokeCloud(CurrentLoc.Location));
            // this block doesn't seem to be working but i'm not sure why
            foreach (var d in doorDirections)
            {
                Point p = g.CurrentRoom.DoorPos(d) + new Point(32, 32); // add 32, 32 to get center rather than topleft
                Point offset = CurrentLoc.Center - p;
                if (offset.ToVector2().Length() < explodeDist)
                {
                    g.ExplodeRoomDoor(d);
                }
            }
        }
        public void Update()
        {
            sprite.Update();
            timer--;
        }
    }
}
