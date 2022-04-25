using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Commands
{
    public class SpawnEnemy : ICommand
    {
        private Room r;
        private IEnemy enemy;
        public SpawnEnemy(Room r, IEnemy enemy)
        {
            this.r = r;
            this.enemy = enemy;
        }

        public void Execute()
        {
            Point size = enemy.CurrentLoc.Size;
            Point place = r.LinkDoorSpawn(Direction.Right) - new Point((int)SpriteUtil.SpriteSize.GenericBlockX * SpriteUtil.SCALE_FACTOR, 0); // right door area is valid in most rooms
            enemy.CurrentLoc = new Rectangle(place, size);
            r.RegisterEntity(enemy);
        }
    }
}
