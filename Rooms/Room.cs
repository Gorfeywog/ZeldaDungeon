﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Rooms
{
    public class Room
    {
        private Walls walls; // may be null to represent a room without walls!
        private IDictionary<Direction, Door> doors = new Dictionary<Direction, Door>(); // may be empty for a room without walls
        public EntityList roomEntities; // does not contain the walls or doors; maybe should?
        private IList<IProjectile> projBuffer; // store projectiles until we can safely add them to roomEntities
        private readonly int gridSize = 16 * SpriteUtil.SCALE_FACTOR;
        private static readonly Direction[] directions = { Direction.Left, Direction.Down, Direction.Right, Direction.Up }; // the order matters; based off structure of the csv files
        private Game1 g;
        public RoomType Type { get; private set; }
        public Point gridPos { get; private set; }
        public Point topLeft { get => gridPos * new Point(16, 11) * new Point(gridSize); }
        private Point[] linkDoorSpawns; // these are relative, not absolute!
        public Point linkDefaultSpawn { get; private set; }
        public Room(Game1 g, string path)
        {
            this.g = g;
            var parser = new CSVParser(path);
            this.gridPos = parser.ParsePos();
            roomEntities = parser.ParseRoomLayout(gridSize, topLeft, g);        
            DoorState[] states = parser.ParseDoorState();
            linkDoorSpawns = parser.ParseLinkSpawns(gridSize);
            linkDefaultSpawn = LinkDoorSpawn(Direction.Up);
            Type = parser.ParseRoomType();
            projBuffer = new List<IProjectile>();
            if (Type == RoomType.Normal)
            {
                int numDoors = 4;
                for (int i = 0; i < numDoors; i++)
                {
                    Direction d = directions[i];
                    doors[d] = new Door(DoorPos(d), d, states[i]);
                }
                walls = new Walls(topLeft);
            }
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            var drawLists = new Dictionary<DrawLayer, List<IEntity>>();
            var layers = (DrawLayer[]) Enum.GetValues(typeof(DrawLayer));
            Array.Sort(layers);
            foreach (DrawLayer layer in layers)
            {
                drawLists[layer] = new List<IEntity>();
            }
            foreach (var en in roomEntities)
            {
                drawLists[en.Layer].Add(en);
            }
            drawLists[g.Player.Layer].Add(g.Player);
            foreach (var d in doors.Values)
            {
                drawLists[d.Layer].Add(d);
            }
            if (walls != null) 
            { 
                drawLists[walls.Layer].Add(walls);
            }
            foreach (var layer in layers)
            {
                drawLists[layer].ForEach(e => e.Draw(spriteBatch));
            }

        }
        public void RegisterProjectile(IProjectile proj)
        {
            projBuffer.Add(proj);
        }
        public void UpdateAll()
        {
            var toBeRemoved = new List<IEntity>();
            bool hasPickup = !g.Player.CanPickUp();
            foreach (var en in roomEntities)
            {
                en.Update();
                if (en.ReadyToDespawn)
                {
                    toBeRemoved.Add(en);
                }
                else if (en is IPickup p && !hasPickup && p.CurrentLoc.Intersects(g.Player.CurrentLoc))
                {
                    if (p.HoldsUp)
                    {
                        hasPickup = true;
                    }
                    g.Player.PickUp(p);
                    toBeRemoved.Add(en);
                }
            }
            foreach (var en in toBeRemoved)
            {
                roomEntities.Remove(en);
                en.DespawnEffect();
            }
            foreach (var proj in projBuffer)
            {
                roomEntities.Add(proj);
            }
            projBuffer.Clear();
        }

        public Point DoorPos(Direction dir)
        {
            Point offset = dir switch
            {
                Direction.Up => new Point(SpriteUtil.X_POS_CENTER * SpriteUtil.SCALE_FACTOR, SpriteUtil.Y_POS_TOP * SpriteUtil.SCALE_FACTOR),
                Direction.Left => new Point(SpriteUtil.X_POS_LEFT * SpriteUtil.SCALE_FACTOR, SpriteUtil.Y_POS_CENTER * SpriteUtil.SCALE_FACTOR),
                Direction.Right => new Point(SpriteUtil.X_POS_RIGHT * SpriteUtil.SCALE_FACTOR, SpriteUtil.Y_POS_CENTER * SpriteUtil.SCALE_FACTOR),
                Direction.Down => new Point(SpriteUtil.X_POS_CENTER * SpriteUtil.SCALE_FACTOR, SpriteUtil.Y_POS_BOTTOM * SpriteUtil.SCALE_FACTOR),
                _ => throw new ArgumentException()
            };
            return topLeft + offset;
        }
        public Point LinkDoorSpawn(Direction dir)
        {
            int index = dir switch
            {
                Direction.Left => 0,
                Direction.Down => 1,
                Direction.Right => 2,
                Direction.Up => 3,
                _ => throw new ArgumentException()
            };
            return topLeft + linkDoorSpawns[index];
        }

        // only call these through Game1, so it can unlock/explode the corresponding door on other side
        // for each of these, return value is just whether "something happened"
        public bool UnlockDoor(Direction dir) 
        {
            return doors[dir].Unlock();
        }
        public bool ExplodeDoor(Direction dir)
        {
            return doors[dir].Explode();
        }
    }
}
