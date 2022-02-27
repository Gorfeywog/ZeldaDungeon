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
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController keyboardController;
        private IList<IProjectile> projectiles = new List<IProjectile>(); // maybe replace this with a dedicated type?
        // which enemy, item, block is being displayed, as an index into the above lists. 
        public ILink Player { get; private set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            keyboardController = new KeyboardController();
            var test = new CSVParser();
            var csvData = test.ParseFile(@"RoomData\room3.csv");
            using (StreamWriter sw = File.CreateText(@"RoomData\output.txt"))
            {
                foreach (List<string> tokens in csvData)
                {
                    sw.WriteLine(tokens[0]);
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
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
        }

        protected override void Update(GameTime gameTime)
        {

            keyboardController.UpdateState();
            keyboardController.ExecuteCommands();
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
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
            Player = new Link();
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
            keyboardController.RegisterCommand(Keys.T, new ChangeBlock(this, true));
            keyboardController.RegisterCommand(Keys.Y, new ChangeBlock(this, false));
            keyboardController.RegisterCommand(Keys.U, new ChangeItem(this, true));
            keyboardController.RegisterCommand(Keys.I, new ChangeItem(this, false));
            keyboardController.RegisterCommand(Keys.O, new ChangeEnemy(this, true));
            keyboardController.RegisterCommand(Keys.P, new ChangeEnemy(this, false));
        }

        public void RegisterProjectile(IProjectile p)
        {
            projectiles.Add(p);
        }
    }
}
