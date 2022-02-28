using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Items;

namespace ZeldaDungeon.Rooms
{
    public class Room
    {
        private Walls walls; // may be null to represent a room without walls!
        private IDictionary<Direction, Door> doors = new Dictionary<Direction, Door>();
        private IList<IEnemy> roomEnemies; // maybe should split logic involving these lists into a new class?
        private IList<IBlock> roomBlocks;
        private const int gridSize = 32;
        private static readonly Direction[] directions = { Direction.Left, Direction.Down, Direction.Right, Direction.Up }; // the order matters; based off structure of the csv files
        private Game1 g;
        public Point topLeft { get; private set; }
        public Point linkDefaultSpawn { get; private set; }
        public Room(Game1 g, string path)
        {
            this.g = g;
            var parser = new CSVParser(path);
            var data = parser.ParseRoomLayout();
            this.topLeft = parser.ParsePos();
            roomEnemies = new List<IEnemy>();
            roomBlocks = new List<IBlock>();
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
            foreach (var en in roomEnemies)
            {
                en.Draw(spriteBatch);
            }
        }
        public void UpdateAll()
        {
            var blocksToBeRemoved = new List<IBlock>();
            var enemiesToBeRemoved = new List<IEnemy>();

            foreach (var enemy in roomEnemies)
            {
                enemy.Update();
                if (enemy.ReadyToDespawn)
                {
                    enemiesToBeRemoved.Add(enemy);
                    enemy.DespawnEffect();
                }
            }
            foreach (var ent in enemiesToBeRemoved)
            {
                roomEnemies.Remove(ent);
            }
            foreach (var block in roomBlocks)
            {
                block.Update();
                if (block.ReadyToDespawn)
                {
                    blocksToBeRemoved.Add(block);
                    block.DespawnEffect();
                }
            }
            foreach (var ent in blocksToBeRemoved)
            {
                roomBlocks.Remove(ent);
            }
        }

        private Point DoorPos(Direction dir)
        {
            // offsets determined by magic, i can't explain how they work
            Point offset = dir switch
            {
                Direction.Up => new Point(224, 0),
                Direction.Left => new Point(0, 144),
                Direction.Right => new Point(448, 144),
                Direction.Down => new Point(224, 288)
            };
            return topLeft + offset;
        }
    }
}
