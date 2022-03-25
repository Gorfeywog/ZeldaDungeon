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

        private static int roomTransFrameCount = 90;
        private int roomTransFrame;
        private Room oldRoom; // only used while transitioning between rooms
        public GameState State { get; private set; }

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
            switch (State) {
                case GameState.Normal:
                    controllers.Update();
                    CurrentRoom.UpdateAll();
                    Player.Update();
                    break;
                case GameState.RoomTransition:
                    roomTransFrame++;
                    if (roomTransFrame == roomTransFrameCount)
                    {
                        State = GameState.Normal;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Point windowTopLeft = default; // default assignment just so it compiles; will never actually be used
            if (State == GameState.Normal)
            {
                windowTopLeft = CurrentRoom.TopLeft;
            }
            else if (State == GameState.RoomTransition)
            {
                windowTopLeft = EntityUtils.Interpolate(oldRoom.TopLeft, CurrentRoom.TopLeft, roomTransFrame, roomTransFrameCount);
            }
            Matrix translator = Matrix.CreateTranslation(-windowTopLeft.X, -windowTopLeft.Y, 0);
            GraphicsDevice.Clear(Color.Black); // this affects the old man room
            spriteBatch.Begin(transformMatrix: translator);
            CurrentRoom.DrawAll(spriteBatch);
            if (State == GameState.RoomTransition)
            {
                oldRoom.DrawAll(spriteBatch);
            }
            Player.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
        public void SetupPlayer()
        {
            Player = new Link(CurrentRoom.LinkDefaultSpawn, CurrentRoom.roomEntities, this);
            Player.ChangeRoom(CurrentRoom);
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
            Player.CurrentLoc = new Rectangle(CurrentRoom.LinkDefaultSpawn, Player.CurrentLoc.Size);
            Player.ChangeRoom(CurrentRoom);
        }
        public void UseRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                // TODO - check door state for validity of this!
                State = GameState.RoomTransition;
                roomTransFrame = 0; // count-up instead of count-down for ease of drawing
                oldRoom = CurrentRoom;
                CurrentRoomIndex = newIndex;
                Player.CurrentLoc = new Rectangle(CurrentRoom.LinkDoorSpawn(EntityUtils.OppositeOf(dir)), Player.CurrentLoc.Size);
            }
            Player.ChangeRoom(CurrentRoom);
        }

        public void UnlockRoomDoor(Direction dir) // TODO - condense this set of three methods into one
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.UnlockDoor(dir);
                rooms[newIndex].UnlockDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public void ExplodeRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.ExplodeDoor(dir);
                rooms[newIndex].ExplodeDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public void OpenRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.OpenDoor(dir);
                rooms[newIndex].OpenDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public int GridToRoomIndex(Point p) => GridToRoomIndex(p.X, p.Y);
        public int GridToRoomIndex(int x, int y) // if no such room exists return -1 as an error value
        {
            for (int i = 0; i < RoomCount; i++)
            {
                Room r = rooms[i];
                // using a loop here is maybe not ideal, but there will only ever be ~20 rooms
                if (r.GridPos.X == x && r.GridPos.Y == y)
                {
                    return i;
                }
            }
            return -1;
        }
        public int DirToRoomIndex(Direction d)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, d, 1);
            return GridToRoomIndex(newGridPos);
        }
    }
}
