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
        private HUD static_HUD;
        private GameOverScreen gameOver;
        private WinScreen winScreen; 
        private PauseMenu static_PauseMenu;
        public int CurrentRoomIndex { get; private set; }
        public int RoomCount { get => Rooms.Count; }
        public Room CurrentRoom { get => Rooms[CurrentRoomIndex]; }

        private static readonly int ROOM_TRANS_FRAME_COUNT = 90;
        private static readonly int PAUSEMENU_TRANS_FRAME_COUNT = 90;
        private static readonly int LINK_DEATH_FRAME_COUNT = 300;
        private int transFrame;
        private Room oldRoom; 
        public GameState State { get; private set; }
        public SpriteFont zeldaFont;
        public ItemSelect Select { get; private set; }
        private bool mainMenu;

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
            graphics.PreferredBackBufferWidth = SpriteUtil.ROOM_WIDTH * SpriteUtil.SCALE_FACTOR; 
            graphics.PreferredBackBufferHeight = (SpriteUtil.ROOM_HEIGHT + SpriteUtil.HUD_HEIGHT) * SpriteUtil.SCALE_FACTOR; 
            graphics.ApplyChanges();
            SetupRooms();
            SetupPlayer();
            Select = new ItemSelect(Player);
            static_HUD = new HUD(this);
            static_PauseMenu = new PauseMenu(this);
            gameOver = new GameOverScreen(zeldaFont);
            mainMenu = true;
            controllers.RegisterCommands(); 
            controllers.RegisterMainMenuCommands(mainMenu);
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
            UISpriteFactory.Instance.LoadAllTextures(Content);
            SpecialSpriteFactory.Instance.LoadAllTextures(Content);

            // audio taken from:
            // https://www.sounds-resource.com/nes/legendofzelda/sound/598/

            SoundManager.Instance.LoadAllAudio(Content);

            // font taken from https://www.fontspace.com/pixel-emulator-font-f21507

            zeldaFont = Content.Load<SpriteFont>("pixelem");

        }

        protected override void Update(GameTime gameTime)
        {
            
            switch (State) {
                case GameState.Normal:
                    controllers.Update();
                    CurrentRoom.UpdateAll();
                    Player.Update();
                    static_HUD.Update();
                    controllers.RegisterMainMenuCommands(mainMenu);
                    break;
                case GameState.RoomTransition:
                    transFrame++;
                    if (transFrame == ROOM_TRANS_FRAME_COUNT)
                    {
                        State = GameState.Normal;
                    }
                    static_HUD.Update();
                    break;
                case GameState.PauseMenuTransitionTo:
                case GameState.PauseMenuTransitionAway:
                    transFrame++;
                    if (transFrame == PAUSEMENU_TRANS_FRAME_COUNT)
                    {
                        if (State == GameState.PauseMenuTransitionTo)
                        {
                            State = GameState.PauseMenu;
                        }
                        else
                        {
                            State = GameState.Normal;
                        }
                    }
                    static_HUD.Update();
                    static_PauseMenu.Update();
                    break;
                case GameState.PauseMenu:
                    controllers.Update();
                    static_HUD.Update();
                    static_PauseMenu.Update();
                    break;
                case GameState.LinkDying:
                    transFrame++;
                    if (transFrame == LINK_DEATH_FRAME_COUNT)
                    {
                        State = GameState.GameOver;
                    }
                    Player.Update(); 
                    static_HUD.Update();
                    static_PauseMenu.Update();
                    break;
                case GameState.GameOver:
                case GameState.WinTower:
                case GameState.WinTriforce:
                    controllers.Update();
                    break;
            }
            Select.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Point roomTopLeft;
            if (State != GameState.RoomTransition)
            {
                roomTopLeft = CurrentRoom.TopLeft;
            } 
            else
            {
                roomTopLeft = EntityUtils.Interpolate(oldRoom.TopLeft, CurrentRoom.TopLeft, transFrame, ROOM_TRANS_FRAME_COUNT);
            }
            int pauseMenuHeight = SpriteUtil.SCALE_FACTOR * SpriteUtil.ROOM_HEIGHT;
            int hudHeight = SpriteUtil.SCALE_FACTOR * SpriteUtil.HUD_HEIGHT;
            Point adjustedRoomTopLeft = roomTopLeft - new Point(0, hudHeight);
            Point pauseMenuOffset = new Point(0, pauseMenuHeight + hudHeight);
            Point pauseMenuTopLeft = roomTopLeft - pauseMenuOffset;
            Point windowTopLeft = State switch
            {
                GameState.PauseMenuTransitionTo => EntityUtils.Interpolate(adjustedRoomTopLeft, pauseMenuTopLeft, transFrame, PAUSEMENU_TRANS_FRAME_COUNT),
                GameState.PauseMenuTransitionAway => EntityUtils.Interpolate(pauseMenuTopLeft, adjustedRoomTopLeft, transFrame, PAUSEMENU_TRANS_FRAME_COUNT),
                GameState.PauseMenu => pauseMenuTopLeft,
                _ => adjustedRoomTopLeft 
            };
            Matrix translator = Matrix.CreateTranslation(-windowTopLeft.X, -windowTopLeft.Y, 0);
            GraphicsDevice.Clear(Color.Black); 
            spriteBatch.Begin(transformMatrix: translator, samplerState:  SamplerState.PointClamp);
            Point hudTopLeft = new Point(pauseMenuTopLeft.X, pauseMenuTopLeft.Y + pauseMenuHeight);
            switch (State)
            {
                case GameState.LinkDying:
                case GameState.Normal:
                    CurrentRoom.DrawAll(spriteBatch);
                    Player.Draw(spriteBatch);
                    static_HUD.Draw(spriteBatch, hudTopLeft);
                    break;
                case GameState.RoomTransition:
                    oldRoom.DrawAll(spriteBatch);
                    CurrentRoom.DrawAll(spriteBatch);
                    Player.Draw(spriteBatch);
                    static_HUD.Draw(spriteBatch, hudTopLeft);
                    break;
                case GameState.PauseMenuTransitionAway:
                case GameState.PauseMenuTransitionTo:
                    CurrentRoom.DrawAll(spriteBatch);
                    Player.Draw(spriteBatch);
                    static_PauseMenu.Draw(spriteBatch, pauseMenuTopLeft);
                    static_HUD.Draw(spriteBatch, hudTopLeft);
                    break;
                case GameState.PauseMenu:
                    static_PauseMenu.Draw(spriteBatch, pauseMenuTopLeft);
                    static_HUD.Draw(spriteBatch, hudTopLeft);
                    break;
                case GameState.GameOver:
                    static_HUD.Draw(spriteBatch, hudTopLeft);
                    gameOver.Draw(spriteBatch, adjustedRoomTopLeft);
                    break;
                case GameState.WinTower:
                case GameState.WinTriforce:
                    static_HUD.Draw(spriteBatch, hudTopLeft);
                    winScreen.Draw(spriteBatch, adjustedRoomTopLeft);
                    break;
                default:
                    break;
            }
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
            foreach (string path in paths )
            {
                if (path.EndsWith(".csv"))
                {
                    Rooms.Add(new Room(this, path));
                }
            }
            CurrentRoomIndex = 17;
        }

        public void Reset()
        {
            SoundEffect death = Content.Load<SoundEffect>("SoundEffects/MinecraftOof");
            death.Play();
            State = GameState.Normal;
            SetupRooms();
            SetupPlayer();
            Select = new ItemSelect(Player);
            static_PauseMenu = new PauseMenu(this);
            mainMenu = true;
            controllers.Reset();
            controllers.RegisterCommands();
            controllers.RegisterMainMenuCommands(mainMenu);
            SoundManager.Instance.PlayMusic("MiiTheme", true);

        }
        public void PauseMenu()
        {
            if (State == GameState.Normal)
            {
                transFrame = 0;
                State = GameState.PauseMenuTransitionTo;
            } 
            else if (State == GameState.PauseMenu)
            {
                transFrame = 0;
                State = GameState.PauseMenuTransitionAway;
            }

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
                transFrame = 0;
                oldRoom = CurrentRoom;
                CurrentRoomIndex = newIndex;
                Player.CurrentLoc = new Rectangle(CurrentRoom.LinkDoorSpawn(EntityUtils.OppositeOf(dir)), Player.CurrentLoc.Size);
                oldRoom.PlayerExits(Player);
                Player.ChangeRoom(CurrentRoom);
                CurrentRoom.PlayerEnters(Player);
            }
            mainMenu = false;
        }

        public void UnlockRoomDoor(Direction dir)
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
        public int GridToRoomIndex(int x, int y) 
        {
            for (int i = 0; i < RoomCount; i++)
            {
                Room r = Rooms[i];
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
        public void Lose()
        {
            SoundManager.Instance.PlaySound("RupeesDecreasing");
            State = GameState.LinkDying;
            transFrame = 0;
        }
        public void Win(bool beatTower)
        {
            State = beatTower ? GameState.WinTower : GameState.WinTriforce;
            SoundManager.Instance.StopMusic();
            winScreen = new WinScreen(zeldaFont, beatTower);
        }
    }
}
