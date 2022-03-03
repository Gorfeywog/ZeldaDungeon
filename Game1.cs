using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using ZeldaDungeon.Commands;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Entities.Items;
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
        private IList<IProjectile> projectiles = new List<IProjectile>(); // maybe replace this with a dedicated type?
        public ILink Player { get; private set; }
        private IList<Room> rooms;
        public int CurrentRoomIndex { get; private set; }
        public int RoomCount { get => rooms.Count; }
        public Room CurrentRoom { get => rooms[CurrentRoomIndex]; }


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
            _graphics.PreferredBackBufferWidth = 256*SpriteUtil.SCALE_FACTOR;  // make window the size of a room, so there's no weird dead space
            _graphics.PreferredBackBufferHeight = 176*SpriteUtil.SCALE_FACTOR; // probably should change this whenever we introduce UI
            _graphics.ApplyChanges();                    // but I like the idea of fixing the size.
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
            var toBeRemoved = new List<IProjectile>();
            int len = projectiles.Count; // despawn effects may register new projectiles, so can't foreach
            for(int i = 0; i < len; i++)
            {
                IProjectile p = projectiles[i];
                p.Update();
                if (p.ReadyToDespawn)
                {
                    p.DespawnEffect();
                    toBeRemoved.Add(p);
                }
            }
            foreach (IProjectile p in toBeRemoved)
            {
                projectiles.Remove(p);
            }
            Player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // consider also scaling by a matrix, maybe?
            Matrix translator = Matrix.CreateTranslation(-CurrentRoom.topLeft.X, -CurrentRoom.topLeft.Y, 0);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: translator);
            CurrentRoom.DrawAll(_spriteBatch);
            foreach (IProjectile p in projectiles)
            {
                p.Draw(_spriteBatch);
            }
            Player.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
        }
        public void SetupPlayer()
        {
            Player = new Link(CurrentRoom.linkDefaultSpawn);
        }
        public void SetupRooms()
        {
            rooms = new List<Room>();
            for (int i = 0; i <= 16; i++)
            {
                rooms.Add(new Room(this, @"RoomData\Room" + i + ".csv")); // has to be after LoadContent, since this uses sprites
            }
            CurrentRoomIndex = 0;
        }

        public void Reset()
        {
            projectiles = new List<IProjectile>();
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
            Point dummyItemSpawn = new Point(0); // the position doesn't matter since it only appears through Link
            keyboardController.RegisterCommand(Keys.D1, new LinkUseItem(this, new BombItem(dummyItemSpawn, this)));
            keyboardController.RegisterCommand(Keys.D2, new LinkUseItem(this, new ArrowItem(dummyItemSpawn, this)));
            keyboardController.RegisterCommand(Keys.D3, new LinkUseItem(this, new MagicArrowItem(dummyItemSpawn, this)));
            keyboardController.RegisterCommand(Keys.D4, new LinkUseItem(this, new Candle(dummyItemSpawn, this, true)));
            keyboardController.RegisterCommand(Keys.D5, new LinkUseItem(this, new BoomerangItem(dummyItemSpawn, this, false)));
            keyboardController.RegisterCommand(Keys.D6, new LinkUseItem(this, new BoomerangItem(dummyItemSpawn, this, true)));
            keyboardController.RegisterCommand(Keys.D7, new LinkUseItem(this, new HeartItem(dummyItemSpawn)));
            keyboardController.RegisterCommand(Keys.D8, new LinkUseItem(this, new KeyItem(dummyItemSpawn)));
            keyboardController.RegisterCommand(Keys.D9, new LinkUseItem(this, new RupyItem(dummyItemSpawn)));
            keyboardController.RegisterCommand(Keys.D0, new LinkUseItem(this, new TriforcePieceItem(dummyItemSpawn)));
            keyboardController.RegisterCommand(Keys.E, new DamageLink(this));
            mouseController.RegisterCommand(new Rectangle(112 * SpriteUtil.SCALE_FACTOR, 0, 32 * SpriteUtil.SCALE_FACTOR, 
                32 * SpriteUtil.SCALE_FACTOR), new LinkUseDoor(this, Direction.Up));
            mouseController.RegisterCommand(new Rectangle(112 * SpriteUtil.SCALE_FACTOR, 144 * SpriteUtil.SCALE_FACTOR,
                32 * SpriteUtil.SCALE_FACTOR, 32 * SpriteUtil.SCALE_FACTOR), new LinkUseDoor(this, Direction.Down));
            mouseController.RegisterCommand(new Rectangle(0, 72 * SpriteUtil.SCALE_FACTOR, 32 * SpriteUtil.SCALE_FACTOR,
                32 * SpriteUtil.SCALE_FACTOR), new LinkUseDoor(this, Direction.Left));
            mouseController.RegisterCommand(new Rectangle(224 * SpriteUtil.SCALE_FACTOR, 72 * SpriteUtil.SCALE_FACTOR,
                32 * SpriteUtil.SCALE_FACTOR, 32 * SpriteUtil.SCALE_FACTOR), new LinkUseDoor(this, Direction.Right));
        }

        public void RegisterProjectile(IProjectile p) // strongly consider moving this to either Room or a dedicated type
        {
            projectiles.Add(p);
        }

        public void TeleportToRoom(int index)
        {
            CurrentRoomIndex = index;
            Player.CurrentLoc = new Rectangle(CurrentRoom.linkDefaultSpawn, Player.CurrentLoc.Size);
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
