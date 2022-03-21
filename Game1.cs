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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController keyboardController;
        private MouseController mouseController;
        public ILink Player { get; private set; }
        private IList<Room> rooms;
        private EntityList CurrentRoomEntities { get => CurrentRoom.roomEntities; }
        public int CurrentRoomIndex { get; private set; }
        public int RoomCount { get => rooms.Count; }
        public Room CurrentRoom { get => rooms[CurrentRoomIndex]; }
        private int xDoorSize = (int)SpriteUtil.SpriteSize.DoorX * SpriteUtil.SCALE_FACTOR;
        private int yDoorSize = (int)SpriteUtil.SpriteSize.DoorY * SpriteUtil.SCALE_FACTOR;
        private int xCenter = SpriteUtil.X_POS_CENTER * SpriteUtil.SCALE_FACTOR;
        private int yCenter = SpriteUtil.Y_POS_CENTER * SpriteUtil.SCALE_FACTOR;
        private int yTop = SpriteUtil.Y_POS_TOP * SpriteUtil.SCALE_FACTOR;
        private int yBottom = SpriteUtil.Y_POS_BOTTOM * SpriteUtil.SCALE_FACTOR;
        private int xLeft = SpriteUtil.X_POS_LEFT * SpriteUtil.SCALE_FACTOR;
        private int xRight = SpriteUtil.X_POS_RIGHT * SpriteUtil.SCALE_FACTOR;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            keyboardController = new KeyboardController();
            mouseController = new MouseController();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = SpriteUtil.ROOM_WIDTH * SpriteUtil.SCALE_FACTOR;  // make window the size of a room, so there's no weird dead space
            _graphics.PreferredBackBufferHeight = SpriteUtil.ROOM_HEIGHT * SpriteUtil.SCALE_FACTOR; 
            _graphics.ApplyChanges();                    
            SetupRooms();
            SetupPlayer();
            RegisterCommands(); // has to be after SetupPlayer, since some commands use Link directly
        }

        protected override void LoadContent()
        {
            // sprites taken from some combination of:
            // https://nesmaps.com/maps/Zelda/sprites/ZeldaSprites.html
            // https://www.spriters-resource.com/nes/legendofzelda/
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            DoorSpriteFactory.Instance.LoadAllTextures(Content);
            SpecialSpriteFactory.Instance.LoadAllTextures(Content);

        }

        protected override void Update(GameTime gameTime)
        {

            keyboardController.UpdateState();
            keyboardController.ExecuteCommands();
            mouseController.UpdateState();
            mouseController.ExecuteCommands();
            CurrentRoom.UpdateAll();
            Player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Matrix translator = Matrix.CreateTranslation(-CurrentRoom.topLeft.X, -CurrentRoom.topLeft.Y, 0);
            GraphicsDevice.Clear(Color.Black); // this affects the old man room
            _spriteBatch.Begin(transformMatrix: translator);
            CurrentRoom.DrawAll(_spriteBatch);
            Player.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
        }
        public void SetupPlayer()
        {
            Player = new Link(CurrentRoom.linkDefaultSpawn, CurrentRoom.roomEntities, this);
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

        private void RegisterCommands()
        {
            keyboardController.RegisterCommand(Keys.Q, new Quit(this));
            keyboardController.RegisterCommand(Keys.R, new Reset(this));
            ICommand linkUp = new MoveLink(this, Direction.Up);
            ICommand linkDown = new MoveLink(this, Direction.Down);
            ICommand linkLeft = new MoveLink(this, Direction.Left);
            ICommand linkRight = new MoveLink(this, Direction.Right);
            ICommand linkStopUp = new StopLink(this, Direction.Up);
            ICommand linkStopDown = new StopLink(this, Direction.Down);
            ICommand linkStopLeft = new StopLink(this, Direction.Left);
            ICommand linkStopRight = new StopLink(this, Direction.Right);
            ICommand linkAttack = new LinkAttack(this);
            keyboardController.RegisterHoldCommand(Keys.W, linkUp, linkStopUp);
            keyboardController.RegisterHoldCommand(Keys.Up, linkUp, linkStopUp);
            keyboardController.RegisterHoldCommand(Keys.A, linkLeft, linkStopLeft);
            keyboardController.RegisterHoldCommand(Keys.Left, linkLeft, linkStopLeft);
            keyboardController.RegisterHoldCommand(Keys.S, linkDown, linkStopDown);
            keyboardController.RegisterHoldCommand(Keys.Down, linkDown, linkStopDown);
            keyboardController.RegisterHoldCommand(Keys.D, linkRight, linkStopRight);
            keyboardController.RegisterHoldCommand(Keys.Right, linkRight, linkStopRight);
            keyboardController.RegisterCommand(Keys.Z, linkAttack);
            keyboardController.RegisterCommand(Keys.N, linkAttack);
            keyboardController.RegisterCommand(Keys.O, new DecRoom(this));
            keyboardController.RegisterCommand(Keys.P, new IncRoom(this));
            keyboardController.RegisterCommand(Keys.D1, new LinkUseItem(this, new BombItem(this)));
            keyboardController.RegisterCommand(Keys.D2, new LinkUseItem(this, new ArrowItem(this, false)));
            keyboardController.RegisterCommand(Keys.D3, new LinkUseItem(this, new ArrowItem(this, true)));
            keyboardController.RegisterCommand(Keys.D4, new LinkUseItem(this, new CandleItem(this, true)));
            keyboardController.RegisterCommand(Keys.D5, new LinkUseItem(this, new BoomerangItem(this, false)));
            keyboardController.RegisterCommand(Keys.D6, new LinkUseItem(this, new BoomerangItem(this, true)));
            keyboardController.RegisterCommand(Keys.E, new DamageLink(this));
            
            //Sets up locations to click to move between doors with mouse
            mouseController.RegisterCommand(new Rectangle(xCenter, yTop, xDoorSize, yDoorSize), new LinkUseDoor(this, Direction.Up));
            mouseController.RegisterCommand(new Rectangle(xCenter, yBottom, xDoorSize, yDoorSize), new LinkUseDoor(this, Direction.Down));
            mouseController.RegisterCommand(new Rectangle(xLeft, yCenter, xDoorSize, yDoorSize), new LinkUseDoor(this, Direction.Left));
            mouseController.RegisterCommand(new Rectangle(xRight, xCenter, xDoorSize, yDoorSize), new LinkUseDoor(this, Direction.Right));
        }

        public void TeleportToRoom(int index)
        {
            CurrentRoomIndex = index;
            Player.CurrentLoc = new Rectangle(CurrentRoom.linkDefaultSpawn, Player.CurrentLoc.Size);
            Player.ChangeRoom(CurrentRoom);
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
            Player.ChangeRoom(CurrentRoom);
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
