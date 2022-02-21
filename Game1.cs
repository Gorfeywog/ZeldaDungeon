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
        private IList<IProjectile> projectiles = new List<IProjectile>(); // maybe replace this with a dedicated type?
        private int currentFrame;
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
            currentFrame = 0;

        }

        protected override void Initialize()
        {
            base.Initialize();
            SetupLists();
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
            currentFrame++;

            keyboardController.UpdateState();
            keyboardController.ExecuteCommands();

            enemies[CurrentEnemyIndex].Update();
            items[CurrentItemIndex].Update();
            blocks[CurrentBlockIndex].Update();

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
            enemies[CurrentEnemyIndex].Draw(_spriteBatch);
            items[CurrentItemIndex].Draw(_spriteBatch);
            blocks[CurrentBlockIndex].Draw(_spriteBatch);
            foreach (IProjectile p in projectiles)
            {
                p.Draw(_spriteBatch);
            }
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
            enemies.Add(new Aquamentus(enemySpawn, this));
            enemies.Add(new Goriya(enemySpawn, this, false));
            enemies.Add(new Gel(enemySpawn));
            enemies.Add(new Keese(enemySpawn));
            enemies.Add(new Goriya(enemySpawn, this, true));
            enemies.Add(new Rope(enemySpawn));
            enemies.Add(new Stalfos(enemySpawn));
            enemies.Add(new Trap(enemySpawn));
            enemies.Add(new WallMaster(enemySpawn));
            items.Add(new ArrowItem(itemSpawn, this));
            items.Add(new BombItem(itemSpawn, this));
            items.Add(new BoomerangItem(itemSpawn, this, false));
            items.Add(new BoomerangItem(itemSpawn, this, true));
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

        public void Reset()
        {
            projectiles = new List<IProjectile>();
            SetupLists();
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
