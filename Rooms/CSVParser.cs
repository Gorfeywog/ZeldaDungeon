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
     * 11 rows of 16 (possibly empty) entries, representing blocks, floor tiles, enemies, pickups, etc.
     * Note that if an enemy is on a grid space, if the immediate next item on the same grid space is a pickup,
     * then the enemy will hold the pickup and drop it upon death.
     * Note that the special entry "spt" must be followed by one of a few special tokens that do not correspond to entities.
     * 1 row of 1 entry representing an ordered pair (two values sep. by ;), representing the location of the room
     * 1 row of 4 tokens representing initial states of doors, ordered *clockwise from the left*. Depending on room type may be meaningless.
     * 1 row of 4 entries, each representing an ordered pair, that specify where Link spawns after using the respective door 
     *      or otherwise entering from the corresponding side, for instance by going down into a 'ladder room'.
     *      note that for top and bottom doors, Link spawns on the seam of two tiles. To account for this, these must be parsed
     *      as a floating point type! for the default door spawns, this is 2;5,7.5;8,13;5,7.5;2
     * 1 row of 1 entry, representing type of the room (0 for normal, 1 for 'ladder room', could extend to arbitrarily many room types
     */
    public class CSVParser
    {
        private const int width = 16;
        private const int height = 11;
        private string[] lines;
        private Room r;
        private Game1 g;
        public CSVParser(String path, Room r, Game1 g)
        {
            lines = System.IO.File.ReadAllLines(path);
            this.r = r;
            this.g = g;
        }
        // array corresponds to the room's grid, list stores every entity on a tile
        // all rows and columns must have the prescribed dimensions
        private IList<String>[,] ParseRoomTokens()
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
        public EntityList ParseRoomLayout(int gridSize, Point topLeft)
        {
            var data = ParseRoomTokens();
            var roomEntities = new EntityList();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Point dest = topLeft + new Point(gridSize * i, gridSize * j);
                    AddEntitiesOnTile(roomEntities, dest, data[i, j]);
                    
                }
            }
            return roomEntities;
        }
        private void AddEntitiesOnTile(EntityList roomEntities, Point dest, IList<string> tokens)
        {
            int k = 0;
            while (k < tokens.Count)
            {
                string s = tokens[k];
                IEntity ent = DecodeToken(s, dest);
                if (k + 1 < tokens.Count && ent is IEnemy anEnemy)
                {
                    string nextS = tokens[k + 1];
                    IEntity nextEnt = DecodeToken(nextS, dest);
                    if (nextEnt is IPickup aPickup)
                    {
                        var combinedEnt = new ItemHolder(anEnemy, aPickup, r);
                        roomEntities.Add(combinedEnt);
                    }
                    else
                    {
                        roomEntities.Add(ent);
                        roomEntities.Add(nextEnt);
                    }
                    k += 2;
                }
                else if (ent != null)
                {
                    roomEntities.Add(ent);
                    k++;
                }
                else
                {
                    k++;
                }
            }
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
                    "ohd" => DoorState.Hole,
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

        public RoomType ParseRoomType()
        {
            string typeRow = lines[height + 3];
            return (RoomType)int.Parse(typeRow);
        }
        public IEntity DecodeToken(string token, Point pos) // may return null!
        {
            return token switch
            {
                "npb" => new NonPushableBlock(pos),
                "wr" => new RoomVoidBlock(pos),
                "bfb" => new BlueFloorBlock(pos),
                "bsb" => new BlueSandBlock(pos),
                "bugb" => new BlueUnwalkableGapBlock(pos),
                "fb" => new FireBlock(pos),
                "lb" => new LadderBlock(pos),
                "pb" => new PushableBlock(pos, r),
                "lvb" => new LadderVoidBlock(pos),
                "sb" => new StairsBlock(pos),
                "s1b" => new Statue1Block(pos),
                "s2b" => new Statue2Block(pos),
                "wbb" => new WhiteBrickBlock(pos),
                "aqe" => new Aquamentus(pos, r),
                "ge" => new Gel(pos, r), 
                "gre" => new Goriya(pos, r, true),
                "gbe" => new Goriya(pos, r, false),
                "om" => new OldMan(pos, r),
                "ke" => new Keese(pos, r),
                "re" => new Rope(pos, r),
                "se" => new Stalfos(pos, r),
                "te" => new Trap(pos, r, g),
                "wme" => new WallMaster(pos, r),
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
