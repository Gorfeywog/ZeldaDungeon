using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Pickups;

namespace ZeldaDungeon.Rooms
{
    /*
     * LAYOUT OF A VALID CSV FILE:
     * 11 rows of 16 (possibly empty) entries, representing blocks, floor tiles, enemies, items, etc.
     * 1 row of 1 entry representing an ordered pair (two values sep. by ;), representing the location of the room
     * 1 row of 4 tokens representing initial states of doors, ordered *clockwise from the left*.
     * 1 row of 4 entries, each representing an ordered pair, that specify where Link spawns after using the respective door.
     *      note that for top and bottom doors, Link spawns on the seam of two tiles. To account for this, these must be parsed
     *      as a floating point type! for the default door spawns, this is 2;5,7.5;8,13;5,7.5;2
     * nothing here yet, possibly a boolean to represent whether it's a normal room (walls and doors) or a ladder room
     */
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
        public IList<String>[,] ParseRoomLayout()
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
            string[] doorRow = lines[height+1].Split(',');
            for (int i = 0; i < 4; i++)
            {
                states[i] = doorRow[i] switch
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
            string[] posRow = lines[height].Split(';');
            int rawX = int.Parse(posRow[0]);
            int rawY = int.Parse(posRow[1]);
            return new Point(rawX, rawY);
        }

        public Point[] ParseLinkSpawns(int tileSize) // tileSize should be 32 at the default scale, for instance
        {
            var spawns = new Point[4];
            string[] spawnsRow = lines[height + 2].Split(',');
            for (int i = 0; i < 4; i++)
            {
                string[] spawn = spawnsRow[i].Split(';');
                float rawX = float.Parse(spawn[0]);
                int fixedX = (int)Math.Round(rawX * tileSize);
                float rawY = float.Parse(spawn[1]);
                int fixedY = (int)Math.Round(rawY * tileSize);
                spawns[i] = new Point(fixedX, fixedY);
            }
            return spawns;
        }
        public static IEntity DecodeToken(string token, Point pos, Game1 g) // may return null!
        {
            return token switch
            {
                "npb" => new NonPushableBlock(pos),
                "wr" => new BlueUnwalkableGapBlock(pos),
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
                "aqe" => new Aquamentus(pos, g),
                "ge" => new Gel(pos),
                "gre" => new Goriya(pos, g, true),
                "gbe" => new Goriya(pos, g, false),
                "om" => new OldMan(pos),
                "ke" => new Keese(pos),
                "re" => new Rope(pos),
                "se" => new Stalfos(pos),
                "te" => new Trap(pos),
                "wme" => new WallMaster(pos),
                "ai1" => new ArrowPickup(pos, g, false),
                "ai2" => new ArrowPickup(pos, g, true),
                "ci1" => new Candle(pos, g, false),
                "ci2" => new Candle(pos, g, true),
                "bomi" => new BombPickup(pos, g),
                "bowi" => new BowPickup(pos),
                "cli" => new ClockPickup(pos),
                "coi" => new CompassPickup(pos),
                "fi" => new FairyPickup(pos),
                "hci" => new HeartContainerPickup(pos),
                "hi" => new HeartPickup(pos),
                "ki" => new KeyPickup(pos),
                "mi" => new MapPickup(pos),
                "ri" => new RupyPickup(pos),
                "tpi" => new TriforcePiecePickup(pos),
                "wbi" => new BoomerangPickup(pos, g, false),
                "wbi1" => new BoomerangPickup(pos, g, false),
                "wbi2" => new BoomerangPickup(pos, g, true),
                "" => null,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
    }
}
