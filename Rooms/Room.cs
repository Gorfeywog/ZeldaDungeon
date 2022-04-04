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
        private IDictionary<Direction, Door> doors = new Dictionary<Direction, Door>(); // may be empty for a room without walls
        public EntityList roomEntities; // holds the walls and the doors, but **not** Link.
        private IList<IEntity> entityBuffer; // hold entities until can safely add to roomEntities
        private static readonly int gridSize = 16 * SpriteUtil.SCALE_FACTOR;
        private static readonly Direction[] directions = { Direction.Left, Direction.Down, Direction.Right, Direction.Up }; // the order matters; based off structure of the csv files
        public Game1 G { get; private set; }
        public RoomType Type { get; private set; }
        public Point GridPos { get; private set; }
        public Point TopLeft { get => GridPos * new Point(16, 11) * new Point(gridSize); }
        private static Point RoomSize { get => new Point(16 * gridSize, 11 * gridSize); }
        public Rectangle RoomPos { get => new Rectangle(TopLeft, RoomSize); }
        private Point[] linkDoorSpawns; // these are relative, not absolute!
        public Point LinkDefaultSpawn { get; private set; }
        public RoomStateMachine StateMachine { get; private set; }
        public Room(Game1 g, string path)
        {
            this.G = g;
            var parser = new CSVParser(path, this, g);
            this.GridPos = parser.ParsePos();
            roomEntities = parser.ParseRoomLayout(gridSize, TopLeft);        
            DoorState[] states = parser.ParseDoorState();
            linkDoorSpawns = parser.ParseLinkSpawns(gridSize);
            LinkDefaultSpawn = LinkDoorSpawn(Direction.Up);
            Type = parser.ParseRoomType();
            entityBuffer = new List<IEntity>();
            if (Type == RoomType.Normal)
            {
                int numDoors = 4;
                for (int i = 0; i < numDoors; i++)
                {
                    Direction d = directions[i];
                    doors[d] = new Door(DoorPos(d), d, states[i]);
                    roomEntities.Add(doors[d]);
                }
               roomEntities.Add(new Walls(TopLeft));
            }
            StateMachine = new RoomStateMachine();
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
            drawLists[G.Player.Layer].Add(G.Player);
            foreach (var layer in layers)
            {
                drawLists[layer].ForEach(e => e.Draw(spriteBatch));
            }

        }
        public void RegisterEntity(IEntity ent)
        {
            entityBuffer.Add(ent);
        }
        public void UpdateAll()
        {
            StateMachine.Update();
            if (StateMachine.State == RoomState.PickUp) { return; } // time stops while Link holds up stuff
            var toBeRemoved = new List<IEntity>();
            bool hasPickup = !G.Player.CanPickUp();
            foreach (var en in roomEntities)
            {
                if (StateMachine.State != RoomState.Clock || !(en is IEnemy)) // enemies don't do stuff in clock state
                {
                    en.Update();
                }
                if (en is IEnemy enemy && enemy.ReadyToDespawn)
                {
                    toBeRemoved.Add(en);
                }
                if (en is IProjectile proj && proj.ReadyToDespawn)
                {
                    toBeRemoved.Add(en);
                }
                else if (en is IPickup p && !hasPickup && p.CurrentLoc.Intersects(G.Player.CurrentLoc))
                {
                    if (p.HoldsUp)
                    {
                        hasPickup = true;
                        StateMachine.PickUp();
                    }
                    G.Player.PickUp(p);
                    toBeRemoved.Add(en);
                }
            }
            foreach (var en in toBeRemoved)
            {
                roomEntities.Remove(en);
                en.DespawnEffect();
            }
            foreach (var en in entityBuffer)
            {
                roomEntities.Add(en);
            }
            entityBuffer.Clear();
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
            return TopLeft + offset;
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
            return TopLeft + linkDoorSpawns[index];
        }
        public bool UnlockDoor(Direction dir) 
        {
            SoundManager.Instance.PlaySound("DoorOpened");
            return doors[dir].Unlock();
        }
        public bool ExplodeDoor(Direction dir)
        {
            SoundManager.Instance.PlaySound("DoorOpened");
            return doors[dir].Explode();
        }
        public bool OpenDoor(Direction dir)
        {
            SoundManager.Instance.PlaySound("DoorOpened");
            return doors[dir].Open();
        }
        public void UseClock()
        {
            StateMachine.UseClock();
        }

        public void PlayerEnters(ILink player)
        {
            StateMachine.EnterEffects();
        }
        public void PlayerExits(ILink player)
        {
            StateMachine.ExitEffects();
        }
    }
}
