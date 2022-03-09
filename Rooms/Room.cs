using Microsoft.Xna.Framework;
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
        // we should really not have both of these, and ideally I would rather have neither
        public EntityList roomEntities; // Wrapper for roomEntities, makes it so we don't pass game1 everywhere
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
            LoadEntities(parser.ParseRoomLayout());            
            DoorState[] states = parser.ParseDoorState();
            linkDoorSpawns = parser.ParseLinkSpawns(gridSize);
            linkDefaultSpawn = LinkDoorSpawn(Direction.Up);
            Type = parser.ParseRoomType();
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
            // the order of drawing matters because things can overlap.
            // in particular, blocks *have* to be first, since otherwise floor tiles
            // would draw all over everything else. that would be very bad.
            foreach (var b in roomEntities.Blocks())
            {
                b.Draw(spriteBatch);
            }
            if (walls != null)
            {
                walls.Draw(spriteBatch);
            }
            foreach (var d in doors)
            {
                d.Value.Draw(spriteBatch);
            }
            foreach (var p in roomEntities.Pickups())
            {
                p.Draw(spriteBatch);
            }
            foreach (var en in roomEntities.Enemies())
            {
                en.Draw(spriteBatch);
            }
            foreach (var p in roomEntities.Projectiles())
            {
                p.Draw(spriteBatch);
            }
        }
        private void LoadEntities(IList<string>[,] data)
        {
            roomEntities = new EntityList();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Point dest = topLeft + new Point(gridSize * i, gridSize * j);
                    foreach (string s in data[i, j])
                    {
                        var ent = CSVParser.DecodeToken(s, dest, g);
                        if (ent != null)
                        {
                            roomEntities.Add(ent);
                        }
                    }
                }
            }
        }
        public void UpdateAll()
        {
            var toBeRemoved = new List<IEntity>();
            bool hasPickup = !g.Player.CanPickUp(); // let link pick up no more than 1 thing, and only if doing so is valid
            foreach (var en in roomEntities)
            {
                en.Update();
                if (en.ReadyToDespawn)
                {
                    toBeRemoved.Add(en);
                    en.DespawnEffect();
                }
                else if (en is IPickup p && !hasPickup && p.CurrentLoc.Intersects(g.Player.CurrentLoc)) // move this to collisionhandler?
                {
                    hasPickup = true;
                    g.Player.PickUp(p);
                    toBeRemoved.Add(en);
                }
            }
            toBeRemoved.ForEach(en => roomEntities.Remove(en));
        }

        public Point DoorPos(Direction dir)
        {
            // offsets determined by magic, i can't explain how they work
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
            int index = dir switch //index is location of correct door in array of linkDoorSpawns
            {
                Direction.Left => 0,
                Direction.Down => 1,
                Direction.Right => 2,
                Direction.Up => 3
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
