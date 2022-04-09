using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
using ZeldaDungeon.UI;

namespace ZeldaDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ControllerManager controllers;
        public ILink Player { get; private set; }
        public IList<Room> Rooms { get; private set; }
        private EntityList CurrentRoomEntities { get => CurrentRoom.roomEntities; }
        // TODO: declare a variable to draw the HUD Sprite somehow
        private HUD static_HUD;

        private PauseMenu static_PauseMenu;
        public int CurrentRoomIndex { get; private set; }
        public int RoomCount { get => Rooms.Count; }
        public Room CurrentRoom { get => Rooms[CurrentRoomIndex]; }

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
            graphics.PreferredBackBufferHeight = (SpriteUtil.ROOM_HEIGHT + SpriteUtil.HUD_HEIGHT) * SpriteUtil.SCALE_FACTOR; 
            graphics.ApplyChanges();
            SetupRooms();
            SetupPlayer();
            static_HUD = new HUD(this);
            static_PauseMenu = new PauseMenu(this);
            controllers.RegisterCommands(); // has to be after SetupPlayer, since some commands use Link directly
            SoundManager.Instance.PlayMusic("MiiTheme", true);
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
            HUDSpriteFactory.Instance.LoadAllTextures(Content);
            SpecialSpriteFactory.Instance.LoadAllTextures(Content);

            // audio taken from:
            // https://www.sounds-resource.com/nes/legendofzelda/sound/598/

            SoundManager.Instance.LoadAllAudio(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            switch (State) {
                case GameState.Normal:
                    controllers.Update();
                    CurrentRoom.UpdateAll();
                    Player.Update();
                    static_HUD.Update();
                    break;
                case GameState.RoomTransition:
                    roomTransFrame++;
                    if (roomTransFrame == roomTransFrameCount)
                    {
                        State = GameState.Normal;
                    }
                    break;
                case GameState.PauseMenu:
                    controllers.Update();
                    static_HUD.Update();
                    static_PauseMenu.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Point windowTopLeft = default; // default assignment just so it compiles; will never actually be used
            Point pauseMenuTopLeft = new Point(windowTopLeft.X, windowTopLeft.Y - (SpriteUtil.HUD_HEIGHT + SpriteUtil.MAP_HEIGHT + SpriteUtil.INVENTORY_HEIGHT) * SpriteUtil.SCALE_FACTOR);
            if (State == GameState.Normal)
            {
                windowTopLeft = CurrentRoom.TopLeft;
            }
            else if (State == GameState.RoomTransition)
            {
                windowTopLeft = EntityUtils.Interpolate(oldRoom.TopLeft, CurrentRoom.TopLeft, roomTransFrame, roomTransFrameCount);
            }
            else if (State == GameState.PauseMenu)
            {
                //windowTopLeft = EntityUtils.Interpolate(CurrentRoom.TopLeft, pauseMenuTopLeft, roomTransFrame, roomTransFrameCount);
            }
            Matrix translator = Matrix.CreateTranslation(-windowTopLeft.X, -windowTopLeft.Y + SpriteUtil.HUD_HEIGHT * SpriteUtil.SCALE_FACTOR, 0);
            GraphicsDevice.Clear(Color.Black); // this affects the old man room
            spriteBatch.Begin(transformMatrix: translator);
            CurrentRoom.DrawAll(spriteBatch);
            Point hudOffset = new Point(windowTopLeft.X, windowTopLeft.Y - SpriteUtil.HUD_HEIGHT * SpriteUtil.SCALE_FACTOR);
            Point mapOffset = new Point(windowTopLeft.X, hudOffset.Y - SpriteUtil.MAP_HEIGHT * SpriteUtil.SCALE_FACTOR);
            Point inventoryOffset = new Point(windowTopLeft.X, mapOffset.Y - SpriteUtil.INVENTORY_HEIGHT * SpriteUtil.SCALE_FACTOR);
            if (State == GameState.RoomTransition)
            {
                oldRoom.DrawAll(spriteBatch);
            } 
            else if (State == GameState.PauseMenu)
            {
                static_PauseMenu.Draw(spriteBatch, hudOffset, mapOffset, inventoryOffset);
            }
            
            static_HUD.Draw(spriteBatch, hudOffset);
            Player.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
        public void SetupPlayer()
        {
            Player = new Link(CurrentRoom.LinkDefaultSpawn, this);
            Player.ChangeRoom(CurrentRoom);
            CurrentRoom.PlayerEnters(Player);
        }
        private const string roomDataPath = @"RoomData";
        public void SetupRooms()
        {
            Rooms = new List<Room>();
            var paths = Directory.GetFiles(roomDataPath);
            Array.Sort(paths); 
            foreach (string path in Directory.GetFiles(roomDataPath) )
            {
                if (path.EndsWith(".csv"))
                {
                    Rooms.Add(new Room(this, path));
                }
            }
            CurrentRoomIndex = 1;
        }

        public void Reset()
        {
            SoundEffect death = Content.Load<SoundEffect>("SoundEffects/MinecraftOof");
            death.Play();
            SetupRooms();
            SetupPlayer();
        }
        public void PauseMenu()
        {
            State = GameState.PauseMenu;
        }

        public void TeleportToRoom(int index)
        {
            oldRoom = CurrentRoom;
            CurrentRoomIndex = index;
            Player.CurrentLoc = new Rectangle(CurrentRoom.LinkDefaultSpawn, Player.CurrentLoc.Size);
            oldRoom.PlayerExits(Player);
            Player.ChangeRoom(CurrentRoom);
            CurrentRoom.PlayerEnters(Player);
        }
        public void UseRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                State = GameState.RoomTransition;
                roomTransFrame = 0; // count-up instead of count-down for ease of drawing
                oldRoom = CurrentRoom;
                CurrentRoomIndex = newIndex;
                Player.CurrentLoc = new Rectangle(CurrentRoom.LinkDoorSpawn(EntityUtils.OppositeOf(dir)), Player.CurrentLoc.Size);
                oldRoom.PlayerExits(Player);
                Player.ChangeRoom(CurrentRoom);
                CurrentRoom.PlayerEnters(Player);
            }
        }

        public void UnlockRoomDoor(Direction dir) // TODO - condense this set of three methods into one
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.UnlockDoor(dir);
                Rooms[newIndex].UnlockDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public void ExplodeRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.ExplodeDoor(dir);
                Rooms[newIndex].ExplodeDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public void OpenRoomDoor(Direction dir)
        {
            Point newGridPos = EntityUtils.Offset(CurrentRoom.GridPos, dir, 1);
            int newIndex = GridToRoomIndex(newGridPos);
            if (newIndex > -1)
            {
                CurrentRoom.OpenDoor(dir);
                Rooms[newIndex].OpenDoor(EntityUtils.OppositeOf(dir));
            }
        }

        public int GridToRoomIndex(Point p) => GridToRoomIndex(p.X, p.Y);
        public int GridToRoomIndex(int x, int y) // if no such room exists return -1 as an error value
        {
            for (int i = 0; i < RoomCount; i++)
            {
                Room r = Rooms[i];
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
