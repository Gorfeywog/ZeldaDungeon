using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Items;

namespace ZeldaDungeon
{
    public class Room
    {
        private IList<IEntity> roomEntities;
        private const int gridSize = 32;
        private Game1 g;
        public Point topLeft { get; private set; }
        public Room(Game1 g, string path, Point topLeft)
        {
            this.g = g;
            this.topLeft = topLeft;
            var parser = new CSVParser();
            var data = parser.ParseFile(path);
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Point dest = topLeft + new Point(gridSize*i, gridSize * j); // is this reversed?
                    foreach (string s in data[i,j])
                    {
                        CSVParser.DecodeToken(s, dest, g);
                    }
                }
            }
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var ent in roomEntities)
            {
                ent.Draw(spriteBatch);
            }
        }
        public void UpdateAll()
        {
            foreach (var ent in roomEntities)
            {
                ent.Update();
            }
        }
    }
}
