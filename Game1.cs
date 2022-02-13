using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ZeldaDungeon.Commands;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController keyboardController;
        // lists of enemies, items, blocks that can be displayed
        private IList<IEnemy> enemies;
        private IList<IItem>  items;
        private IList<IBlock> blocks;
        // which enemy, item, block is being displayed, as an index into the above lists. 
        public int CurrentEnemyIndex { get; set; }
        public int CurrentItemIndex { get; set; }
        public int CurrentBlockIndex { get; set; }
        public int EnemyCount { get => enemies.Count; }
        public int ItemCount { get => items.Count; }
        public int BlockCount { get => blocks.Count;  }
        public ILink Player { get; private set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            keyboardController = new KeyboardController();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            RegisterCommands();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.UpdateState();
            keyboardController.ExecuteCommands();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // adding a change to see if this goes into a new branch

            base.Draw(gameTime);
        }

        public void SetupLists()
        {
            enemies = new List<IEnemy>();
            items = new List<IItem>();
            blocks = new List<IBlock>();
            // add code here to insert every enemy, item, block to their respective lists


            CurrentEnemyIndex = 0;
            CurrentItemIndex = 0;
            CurrentBlockIndex = 0;
        }

        public void SetupPlayer()
        {
            Player = new Link();
        }

        private void RegisterCommands()
        {
            keyboardController.RegisterCommand(Keys.Q, new Quit(this));
            keyboardController.RegisterCommand(Keys.R, new Reset(this));
            keyboardController.RegisterCommand(Keys.W, new MoveLink(this, LinkStateMachine.LinkDirection.Up));
            keyboardController.RegisterCommand(Keys.Up, new MoveLink(this, LinkStateMachine.LinkDirection.Up));
            keyboardController.RegisterCommand(Keys.A, new MoveLink(this, LinkStateMachine.LinkDirection.Left));
            keyboardController.RegisterCommand(Keys.Left, new MoveLink(this, LinkStateMachine.LinkDirection.Left));
            keyboardController.RegisterCommand(Keys.S, new MoveLink(this, LinkStateMachine.LinkDirection.Down));
            keyboardController.RegisterCommand(Keys.Down, new MoveLink(this, LinkStateMachine.LinkDirection.Down));
            keyboardController.RegisterCommand(Keys.D, new MoveLink(this, LinkStateMachine.LinkDirection.Right));
            keyboardController.RegisterCommand(Keys.Right, new MoveLink(this, LinkStateMachine.LinkDirection.Right));
            // TODO - add commands for Link using each item
            keyboardController.RegisterCommand(Keys.E, new DamageLink(this));
            keyboardController.RegisterCommand(Keys.T, new ChangeBlock(this, true));
            keyboardController.RegisterCommand(Keys.Y, new ChangeBlock(this, false));
            keyboardController.RegisterCommand(Keys.U, new ChangeItem(this, true));
            keyboardController.RegisterCommand(Keys.I, new ChangeItem(this, false));
            keyboardController.RegisterCommand(Keys.O, new ChangeEnemy(this, true));
            keyboardController.RegisterCommand(Keys.P, new ChangeEnemy(this, false));
        }
    }
}
