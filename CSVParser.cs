using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Items;

namespace ZeldaDungeon
{
    public class CSVParser
    {
        private const int width = 16;
        private const int height = 11;
        public CSVParser() { }
        // array corresponds to the room's grid, list stores every entity on a tile?
        // all rows and columns must have the prescribed dimensions, or bad stuff happens
        public IList<String>[,] ParseFile(string path) // should this be static?
        {
            IList<String>[,] tokens = new IList<String>[width,height];
            string[] lines = System.IO.File.ReadAllLines(path);
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
        public static IEntity DecodeToken(string token, Point pos, Game1 g)
        {
            return token switch // how handle wr? should wr even be in these files? walls are the same in basically every room, could handle doors specially
            {
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
                _ => throw new ArgumentOutOfRangeException() // again, still need to handle wr somehow probably
            };
        }
    }
}
