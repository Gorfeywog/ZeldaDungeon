using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon
{
    public class CSVParser
    {
        private const int width = 16;
        private const int height = 11;
        public CSVParser() { }
        // array corresponds to the room's grid, list stores every entity on a tile?
        // all rows and columns must have the prescribed dimensions, or bad stuff happens
        public IList<String>[,] parseFile(string path)
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
    }
}
