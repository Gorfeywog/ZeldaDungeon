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
        private IList<IEnemy> roomEnemies; // maybe should split logic involving these lists into a new class?
        private IList<IBlock> roomBlocks;
        private const int gridSize = 32;
        private Game1 g;
        public Point topLeft { get; private set; }
        public Room(Game1 g, string path, Point topLeft)
        {
            this.g = g;
            this.topLeft = topLeft;
            var parser = new CSVParser();
            var data = parser.ParseFile(path);
            roomEnemies = new List<IEnemy>();
            roomBlocks = new List<IBlock>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Point dest = topLeft + new Point(gridSize*i, gridSize * j); // is this reversed?
                    foreach (string s in data[i,j])
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
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var b in roomBlocks) // draw blocks first, for overlap purposes
            {
                b.Draw(spriteBatch);
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
    }
}
