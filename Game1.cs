using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ZeldaDungeon.Commands;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Link;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ControllerManager controllers;
        public ILink Player { get; private set; }
        private IList<Room> rooms;
        private EntityList CurrentRoomEntities { get => CurrentRoom.roomEntities; }
        public int CurrentRoomIndex { get; private set; }
        public int RoomCount { get => rooms.Count; }
        public Room CurrentRoom { get => rooms[CurrentRoomIndex]; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            controllers = new ControllerManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferredBackBufferWidth = SpriteUtil.ROOM_WIDTH * SpriteUtil.SCALE_FACTOR;  // make window the size of a room, so there's no weird dead space
            graphics.PreferredBackBufferHeight = SpriteUtil.ROOM_HEIGHT * SpriteUtil.SCALE_FACTOR; 
            graphics.ApplyChanges();                    
            SetupRooms();
            SetupPlayer();
            controllers.RegisterCommands(); // has to be after SetupPlayer, since some commands use Link directly
        }

        protected override void LoadContent()
        {
            // sprites taken from some combination of:
            // https://nesmaps.com/maps/Zelda/sprites/ZeldaSprites.html
            // https://www.spriters-resource.com/nes/legendofzelda/
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            DoorSpriteFactory.Instance.LoadAllTextures(Content);
            SpecialSpriteFactory.Instance.LoadAllTextures(Content);

        }

        protected override void Update(GameTime gameTime)
        {

            controllers.Update();
            CurrentRoom.UpdateAll();
            Player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Matrix translator = Matrix.CreateTranslation(-CurrentRoom.topLeft.X, -CurrentRoom.topLeft.Y, 0);
            GraphicsDevice.Clear(Color.Black); // this affects the old man room
            spriteBatch.Begin(transformMatrix: translator);
            CurrentRoom.DrawAll(spriteBatch);
            Player.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
        public void SetupPlayer()
        {
            Player = new Link(CurrentRoom.linkDefaultSpawn, CurrentRoom.roomEntities, this);
            Player.UpdateList(CurrentRoom.roomEntities);
        }
        private const string roomDataPath = @"RoomData";
        public void SetupRooms()
        {
            rooms = new List<Room>();
            var paths = Directory.GetFiles(roomDataPath);
            Array.Sort(paths); 
            foreach (string path in Directory.GetFiles(roomDataPath) )
            {
                if (path.EndsWith(".csv"))
                {
                    rooms.Add(new Room(this, path));
                }
            }
            CurrentRoomIndex = 1;
        }

        public void Reset()
        {
            SetupRooms();
            SetupPlayer();
        }

        public void TeleportToRoom(int index)
        {
            CurrentRoomIndex = index;
            Player.CurrentLoc = new Rectangle(CurrentRoom.linkDefaultSpawn, Player.CurrentLoc.Size);
            Player.UpdateList(CurrentRoom.roomEntities);
            CurrentRoomEntities.UpdateList(CurrentRoom.roomEntities);
        }
        public void UseRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.gridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                // TODO - check door state for validity of this!
                CurrentRoomIndex = newIndex;
                Player.CurrentLoc = new Rectangle(CurrentRoom.LinkDoorSpawn(EntityUtils.OppositeOf(dir)), Player.CurrentLoc.Size);
            }
            Player.UpdateList(CurrentRoom.roomEntities);
            CurrentRoomEntities.UpdateList(CurrentRoom.roomEntities);
        }

        public void UnlockRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.gridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.UnlockDoor(dir);
                rooms[newIndex].UnlockDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public void ExplodeRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.gridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.ExplodeDoor(dir);
                rooms[newIndex].ExplodeDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public int GridToRoomIndex(Point p) => GridToRoomIndex(p.X, p.Y);
        public int GridToRoomIndex(int x, int y) // if no such room exists return -1 as an error value
        {
            for (int i = 0; i < RoomCount; i++)
            {
                Room r = rooms[i];
                // using a loop here is maybe not ideal, but there will only ever be ~20 rooms
                if (r.gridPos.X == x && r.gridPos.Y == y)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
