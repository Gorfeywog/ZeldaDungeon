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
		public IList<IEntity> roomEntities; // Used for collision handling
        private IList<IEnemy> roomEnemies; // maybe should split logic involving these lists into a new class?
        private IList<IBlock> roomBlocks;
        private IList<IPickup> pickups;
        private const int gridSize = 16 * SpriteUtil.SCALE_FACTOR;
        private static readonly Direction[] directions = { Direction.Left, Direction.Down, Direction.Right, Direction.Up }; // the order matters; based off structure of the csv files
        private Game1 g;
        public Point gridPos { get; private set; }
        public Point topLeft { get => gridPos * new Point(512, 352); } // maybe cache this somehow?
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
            pickups = new List<IPickup>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Point dest = topLeft + new Point(gridSize * i, gridSize * j);
                    foreach (string s in data[i, j])
                    {
                        var ent = CSVParser.DecodeToken(s, dest, g);
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
            for (int i = 0; i < 4; i++) {
                Direction d = directions[i];
                doors[d] = new Door(DoorPos(d), d, states[i]);
            }
            walls = new Walls(topLeft);
            linkDefaultSpawn = topLeft + new Point(32 * 4); // TODO - there should be some logic for the ladder rooms
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
                Direction.Up => new Point(224, 0),
                Direction.Left => new Point(0, 144),
                Direction.Right => new Point(448, 144),
                Direction.Down => new Point(224, 288),
                _ => throw new ArgumentException()
            };
            return topLeft + offset;
        }

        public Point LinkDoorSpawn(Direction dir)
        {
            Point doorPos = DoorPos(dir);
            return EntityUtils.Offset(doorPos, dir, -32);
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
