using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI
{
    public class PauseMapManager
    {
        public static readonly int MAP_GRID_LENGTH = 8;
        public static readonly int PAUSE_MAP_OFFSET_X = 126;
        public static readonly int PAUSE_MAP_OFFSET_Y = 10;
        private Game1 g;
        private PauseMapRoomState[,] mapGrid;
        private PauseMap pauseMap;
        public PauseMapManager(Game1 g)
        {
            this.g = g;
            mapGrid = new PauseMapRoomState[MAP_GRID_LENGTH, MAP_GRID_LENGTH];
            pauseMap = new PauseMap();
            Update(false); // prevent a tricky crash
        }
        public void Draw(SpriteBatch spriteBatch, Point pausePos)
        {
            Point pauseMapTopLeft = pausePos + new Point(PAUSE_MAP_OFFSET_X * SpriteUtil.SCALE_FACTOR, PAUSE_MAP_OFFSET_Y * SpriteUtil.SCALE_FACTOR);
            pauseMap.Draw(spriteBatch, pauseMapTopLeft, mapGrid);
        }
        private static readonly Direction[] possible = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        public void Update(bool hasMap) // (perhaps inaccurately to the original) pause map only appears at all if you have the map
        {
            for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    int index = g.GridToRoomIndex(i, j);
                    if (!hasMap || index == -1 || g.Rooms[index].Type == RoomType.Ladder || !g.Rooms[index].Visited)
                    {
                        mapGrid[i, j] = new PauseMapRoomState(false, new HashSet<Direction>());
                    }
                    else
                    {
                        Room r = g.Rooms[i];
                        var directions = new HashSet<Direction>();
                        foreach (var d in possible)
                        {
                            if (r.HasVisibleDoor(d))
                            {
                                directions.Add(d);
                            }
                        }
                        mapGrid[i, j] = new PauseMapRoomState(true, directions);
                    }
                }
            }
            pauseMap.Update();
        }
        
    }
}
