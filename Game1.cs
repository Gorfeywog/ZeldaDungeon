using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
            base.Initialize();
            SetupLists();
            SetupPlayer();
            RegisterCommands(); // has to be after SetupPlayer, since some commands use Link directly
        }

        protected override void LoadContent()
        {
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
            enemies[CurrentEnemyIndex].Update();
            items[CurrentItemIndex].Update();
            blocks[CurrentBlockIndex].Update();
            Player.Update();

            enemies[CurrentEnemyIndex].Move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            enemies[CurrentEnemyIndex].Draw(_spriteBatch);
            items[CurrentItemIndex].Draw(_spriteBatch);
            blocks[CurrentBlockIndex].Draw(_spriteBatch);
            Player.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        public void SetupLists()
        {
            enemies = new List<IEnemy>();
            items = new List<IItem>();
            blocks = new List<IBlock>();
            Point enemySpawn = new Point(600, 300);
            Point itemSpawn = new Point(200, 200);
            Point blockSpawn = new Point(300, 300);
            enemies.Add(new Aquamentus(enemySpawn));
            enemies.Add(new BlueGoriya(enemySpawn));
            enemies.Add(new Gel(enemySpawn));
            enemies.Add(new Keese(enemySpawn));
            enemies.Add(new RedGoriya(enemySpawn));
            enemies.Add(new Rope(enemySpawn));
            enemies.Add(new Stalfos(enemySpawn));
            enemies.Add(new Trap(enemySpawn));
            items.Add(new ArrowItem(itemSpawn));
            items.Add(new BombItem(itemSpawn));
            items.Add(new BowItem(itemSpawn));
            items.Add(new ClockItem(itemSpawn));
            items.Add(new CompassItem(itemSpawn));
            items.Add(new FairyItem(itemSpawn));
            items.Add(new HeartContainerItem(itemSpawn));
            items.Add(new HeartItem(itemSpawn));
            items.Add(new KeyItem(itemSpawn));
            items.Add(new MapItem(itemSpawn));
            items.Add(new RupyItem(itemSpawn));
            items.Add(new TriforcePieceItem(itemSpawn));
            items.Add(new WoodenBoomerangItem(itemSpawn));
            blocks.Add(new BlueFloorBlock(blockSpawn));
            blocks.Add(new BlueSandBlock(blockSpawn));
            blocks.Add(new BlueUnwalkableGapBlock(blockSpawn));
            blocks.Add(new FireBlock(blockSpawn));
            blocks.Add(new LadderBlock(blockSpawn));
            blocks.Add(new PushableBlock(blockSpawn));
            blocks.Add(new Statue1Block(blockSpawn));
            blocks.Add(new Statue2Block(blockSpawn));
            blocks.Add(new WhiteBrickBlock(blockSpawn));
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
