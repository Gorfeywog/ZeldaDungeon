using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Items;

namespace ZeldaDungeon.Rooms
{
    public class CSVParser
    {
        private const int width = 16;
        private const int height = 11;
        private string path; // used for debugging
        private string[] lines;
        public CSVParser(String path)
        {
            this.path = path;
            lines = System.IO.File.ReadAllLines(path);
        }
        // array corresponds to the room's grid, list stores every entity on a tile?
        // all rows and columns must have the prescribed dimensions, or bad stuff happens.
        // for now, this just straight-up ignores the walls; i think we can safely eliminate them
        // from the csv files, but we may want to indicate doors in some way?
        public IList<String>[,] ParseRoomLayout() // should this be static?
        {
            IList<String>[,] tokens = new IList<String>[width, height];
            for (int i = 0; i < height; i++) // height *should* match lines.Length
            {
                string[] lineBlocks = lines[i].Split(',');
                for (int j = 0; j < width; j++)
                {
                    string currentBlock = lineBlocks[j];
                    tokens[j, i] = new List<string>(currentBlock.Split(';'));
                }
            }
            return tokens;
        }
        public DoorState[] ParseDoorState()
        {
            DoorState[] states = new DoorState[4];
            string[] lastRow = lines[height].Split(',');
            for (int i = 0; i < 4; i++)
            {
                states[i] = lastRow[i] switch
                {
                    "od" => DoorState.Open,
                    "cd" => DoorState.Closed,
                    "nd" => DoorState.None,
                    "ld" => DoorState.Locked,
                    "hd" => DoorState.BlockedHole,
                    _ => throw new ArgumentException()
                };
            }
            return states;
        }
        public Point ParsePos()
        {
            string[] lastRow = lines[height].Split(',');
            int rawX = int.Parse(lastRow[4]);
            int rawY = int.Parse(lastRow[5]);
            return new Point(rawX, rawY);
        }
        public static IEntity DecodeToken(string token, Point pos, Game1 g) // may return null!
        {
            return token switch // how handle wr? should wr even be in these files? walls are the same in basically every room, could handle doors specially
            {
                "npb" => new NonPushableBlock(pos),
                "wr" => new BlueUnwalkableGapBlock(pos), // hacky temporary fix; figure out a better solution!
                "bfb" => new BlueFloorBlock(pos),
                "bsb" => new BlueSandBlock(pos),
                "bugb" => new BlueUnwalkableGapBlock(pos),
                "fb" => new FireBlock(pos),
                "lb" => new LadderBlock(pos),
                "pb" => new PushableBlock(pos),
                "sb" => new StairsBlock(pos),
                "s1b" => new Statue1Block(pos),
                "s2b" => new Statue2Block(pos),
                "wbb" => new WhiteBrickBlock(pos),
                "aqe" => new Aquamentus(pos, g), // If we end up taking g out, collision will break
                "ge" => new Gel(pos), // we could just use g to be more consisten, just thought room was better
                "gre" => new Goriya(pos, g, true),
                "gbe" => new Goriya(pos, g, false),
                "om" => new OldMan(pos),
                "ke" => new Keese(pos),
                "re" => new Rope(pos),
                "se" => new Stalfos(pos),
                "te" => new Trap(pos),
                "wme" => new WallMaster(pos),
                "ai" => new ArrowItem(pos, g),
                "bomi" => new BombItem(pos, g),
                "bowi" => new BowItem(pos),
                "cli" => new ClockItem(pos),
                "coi" => new CompassItem(pos),
                "fi" => new FairyItem(pos),
                "hci" => new HeartContainerItem(pos),
                "hi" => new HeartItem(pos),
                "ki" => new KeyItem(pos),
                "mi" => new MapItem(pos),
                "ri" => new RupyItem(pos),
                "tpi" => new TriforcePieceItem(pos),
                "wbi" => new BoomerangItem(pos, g, false),
                "" => null,
                _ => throw new ArgumentOutOfRangeException() // again, still need to handle wr somehow probably
            };
        }
    }
}
