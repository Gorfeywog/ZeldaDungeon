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
        private IDictionary<Direction, Door> doors = new Dictionary<Direction, Door>();
		public IList<IEntity> roomEntities; // Used for collision handling - evil!
        private IList<IEnemy> roomEnemies; // maybe should split logic involving these lists into a new class?
        private IList<IBlock> roomBlocks;
        private IList<IPickup> pickups;
        private readonly int gridSize = 16 * SpriteUtil.SCALE_FACTOR;
        private static readonly Direction[] directions = { Direction.Left, Direction.Down, Direction.Right, Direction.Up }; // the order matters; based off structure of the csv files
        private Game1 g;
        public Point gridPos { get; private set; }
        public Point topLeft { get => gridPos * new Point(SpriteUtil.ROOM_WIDTH * SpriteUtil.SCALE_FACTOR, 
            SpriteUtil.ROOM_HEIGHT * SpriteUtil.SCALE_FACTOR); } // maybe cache this somehow?
        private Point[] linkDoorSpawns; // these are relative, not absolute!
        public Point linkDefaultSpawn { get; private set; }
        public Room(Game1 g, string path)
        {
            this.g = g;
            var parser = new CSVParser(path);
            var data = parser.ParseRoomLayout();
            this.gridPos = parser.ParsePos();
            // 512 and 352 are width and height of a room, respectively
            roomEnemies = new List<IEnemy>();
            roomBlocks = new List<IBlock>();
            roomEntities = new List<IEntity>();
            pickups = new List<IPickup>();
            // this evil nested loop should probably live in its own method
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Point dest = topLeft + new Point(gridSize * i, gridSize * j);
                    foreach (string s in data[i, j])
                    {
                        var ent = CSVParser.DecodeToken(s, dest, g);
                        roomEntities.Add(ent);
                        if (ent is IEnemy en)
                        {
                            roomEnemies.Add(en);
                        }
                        else if (ent is IBlock b)
                        {
                            roomBlocks.Add(b);
                        }
                        else if (ent is IPickup pickup)
                        {
                            pickups.Add(pickup);
                        }
                    }
                }
            }
            DoorState[] states = parser.ParseDoorState();
            int numDoors = 4;
            for (int i = 0; i < numDoors; i++) {
                Direction d = directions[i];
                doors[d] = new Door(DoorPos(d), d, states[i]);
            }
            walls = new Walls(topLeft);
            linkDoorSpawns = parser.ParseLinkSpawns(gridSize);
            linkDefaultSpawn = topLeft + new Point(SpriteUtil.LINK_DEFAULT_SPAWN * SpriteUtil.SCALE_FACTOR); // TODO - there should be some logic for the ladder rooms
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var b in roomBlocks) // draw blocks first, for overlap purposes
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
            foreach (var i in pickups)
            {
                i.Draw(spriteBatch);
            }
            foreach (var en in roomEnemies)
            {
                en.Draw(spriteBatch);
            }
        }
        public void UpdateAll()
        {
            var blocksToBeRemoved = new List<IBlock>();
            var enemiesToBeRemoved = new List<IEnemy>();
            var pickupsToBeRemoved = new List<IPickup>();
            bool hasPickup = !g.Player.CanPickUp(); // let link pick up no more than 1 thing, and only if doing so is valid
            foreach (var enemy in roomEnemies)
            {
                enemy.Update();
                if (enemy.ReadyToDespawn)
                {
                    enemiesToBeRemoved.Add(enemy);
                    enemy.DespawnEffect();
                }
            }
            enemiesToBeRemoved.ForEach(e => roomEnemies.Remove(e));
            foreach (var pickup in pickups)
            {
                pickup.Update();
                if (pickup.ReadyToDespawn)
                {
                    pickupsToBeRemoved.Add(pickup);
                    pickup.DespawnEffect();
                }
                else if (!hasPickup && pickup.CurrentLoc.Intersects(g.Player.CurrentLoc)) // maybe let collisionhandler do this stuff?
                {
                    hasPickup = true;
                    g.Player.PickUp(pickup);
                    pickupsToBeRemoved.Add(pickup);
                }
            }
            pickupsToBeRemoved.ForEach(p => pickups.Remove(p));
            foreach (var block in roomBlocks)
            {
                block.Update();
                if (block.ReadyToDespawn)
                {
                    blocksToBeRemoved.Add(block);
                    block.DespawnEffect();
                }
            }
            blocksToBeRemoved.ForEach(b => roomBlocks.Remove(b));
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
