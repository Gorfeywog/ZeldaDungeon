using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static ZeldaDungeon.Sprites.SpriteUtil;
using ZeldaDungeon.Entities;
using static ZeldaDungeon.Entities.Direction;

namespace ZeldaDungeon.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpriteSheet;
        private Texture2D bowserSpriteSheet;
        private Texture2D rickSpriteSheet;
        private Texture2D gameNWatchSpriteSheet;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();
        
        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory() {   }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpriteSheet = content.Load<Texture2D>("enemysprites");
            bowserSpriteSheet = content.Load<Texture2D>("bowsersprites");
            rickSpriteSheet = content.Load<Texture2D>("rick");
            gameNWatchSpriteSheet = content.Load<Texture2D>("gameNWatch");
        }

        public ISprite CreateBowserSprite()
        {
            int width = (int)SpriteSize.BowserX;
            int height = (int)SpriteSize.BowserY;

            Rectangle[] sourceRectangles = { new Rectangle(0, 209, width, height), new Rectangle(34, 209, width, height) };
            return new AnimatedSprite(bowserSpriteSheet, sourceRectangles);
        }

        public ISprite CreateBowserFireSprite()
        {
            int width = (int)SpriteSize.BowserFireX;
            int height = (int)SpriteSize.BowserFireY;

            Rectangle[] sourceRectangles = { new Rectangle(102, 242, width, height), new Rectangle(102, 252, width, height) };
            return new AnimatedSprite(bowserSpriteSheet, sourceRectangles);
        }

        public ISprite CreateKoopaSprite()
        {
            int width = (int)SpriteSize.KoopaX;
            int height = (int)SpriteSize.KoopaY;

            Rectangle[] sourceRectangles = { new Rectangle(0, 182, width, height), new Rectangle(18, 182, width, height) };
            return new AnimatedSprite(bowserSpriteSheet, sourceRectangles);
        }

        public ISprite CreateRickAstleySprite()
        {
            int width = (int)SpriteSize.RickX;
            int height = (int)SpriteSize.RickY;

            Rectangle[] sourceRectangles = { new Rectangle(5, 5, width, height), new Rectangle(59, 5, width, height) };
            return new AnimatedSprite(rickSpriteSheet, sourceRectangles);
        }

        public ISprite CreateMrGameNWatchSprite()
        {
            int width = (int)SpriteSize.GameNWatchX;
            int height = (int)SpriteSize.GameNWatchY;

            Rectangle[] sourceRectangles = { new Rectangle(352, 21, width, height), new Rectangle(378, 21, width, height) };
            return new AnimatedSprite(gameNWatchSpriteSheet, sourceRectangles);
        }
        public ISprite CreateKeeseSprite()
        {
            int width = (int)SpriteSize.KeeseX;
            int height = (int)SpriteSize.KeeseY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 4, width, height), GridToRectangle(1, 4, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateAquamentusSprite()
        {
            int width = (int)SpriteSize.AquamentusX;
            int height = (int)SpriteSize.AquamentusY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 0, width, height), GridToRectangle(1, 0, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateFireballSprite()
        {
            int width = (int)SpriteSize.FireballX;
            int height = (int)SpriteSize.FireballY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 11, width, height), GridToRectangle(1, 11, width, height),
                GridToRectangle(2, 11, width, height), GridToRectangle(3, 11, width, height)};
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateGelSprite()
        {
            int width = (int)SpriteSize.GelX;
            int height = (int)SpriteSize.GelY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 1, width, height), GridToRectangle(1, 1, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteLeft()
        {
            int width = (int)SpriteSize.RopeX;
            int height = (int)SpriteSize.RopeY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 5, width, height), GridToRectangle(1, 5, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateRopeSpriteRight()
        {
            int width = (int)SpriteSize.RopeX;
            int height = (int)SpriteSize.RopeY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 5, width, height), GridToRectangle(3, 5, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateStalfosSprite()
        {
            int width = (int)SpriteSize.StalfosX;
            int height = (int)SpriteSize.StalfosY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 6, width, height), GridToRectangle(1, 6, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateTrapSprite()
        {
            return new StaticSprite(enemySpriteSheet, GridToRectangle(0, 7, (int)SpriteSize.TrapX, (int)SpriteSize.TrapY));
        }

        public ISprite CreateOldManSprite()
        {
            return new StaticSprite(enemySpriteSheet, GridToRectangle(1, 7, 16, 16));
        }
        public ISprite CreateGoriyaSprite(Direction dir, bool isRed)
        {
            return (dir, isRed) switch
            {
                (Up, true) => CreateRedGoriyaSpriteUp(),
                (Down, true) => CreateRedGoriyaSpriteDown(),
                (Left, true) => CreateRedGoriyaSpriteLeft(),
                (Right, true) => CreateRedGoriyaSpriteRight(),
                (Up, false) => CreateBlueGoriyaSpriteUp(),
                (Down, false) => CreateBlueGoriyaSpriteDown(),
                (Left, false) => CreateBlueGoriyaSpriteLeft(),
                (Right, false) => CreateBlueGoriyaSpriteRight()
            };
        }

        private ISprite CreateBlueGoriyaSpriteLeft()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(4, 2, width, height), GridToRectangle(5, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateBlueGoriyaSpriteRight()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(6, 2, width, height), GridToRectangle(7, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateBlueGoriyaSpriteUp()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 2, width, height), GridToRectangle(1, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateBlueGoriyaSpriteDown()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 2, width, height), GridToRectangle(3, 2, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateRedGoriyaSpriteLeft()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(4, 3, width, height), GridToRectangle(5, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateRedGoriyaSpriteRight()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(6, 3, width, height), GridToRectangle(7, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateRedGoriyaSpriteUp()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 3, width, height), GridToRectangle(1, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        private ISprite CreateRedGoriyaSpriteDown()
        {
            int width = (int)SpriteSize.GoriyaX;
            int height = (int)SpriteSize.GoriyaY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 3, width, height), GridToRectangle(3, 3, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            return new StaticSprite(enemySpriteSheet, GridToRectangle(0, 10, width, height));
        }
        public ISprite CreateBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 10, width, height), 
                GridToRectangle(1, 10, width, height), GridToRectangle(2, 10, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateStaticMagicBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            return new StaticSprite(enemySpriteSheet, GridToRectangle(3, 10, width, height));
        }
        public ISprite CreateMagicBoomerangSprite()
        {
            int width = (int)SpriteSize.BoomerangX;
            int height = (int)SpriteSize.BoomerangY;

            Rectangle[] sourceRectangles = { GridToRectangle(3, 10, width, height), 
                GridToRectangle(4, 10, 5, 8), GridToRectangle(5, 10, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
        public ISprite CreateWallMasterSpriteSW()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 8, width, height), GridToRectangle(1, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteSE()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(2, 8, width, height), GridToRectangle(3, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNW()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(4, 8, width, height), GridToRectangle(5, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateWallMasterSpriteNE()
        {
            int width = (int)SpriteSize.WallMasterX;
            int height = (int)SpriteSize.WallMasterY;

            Rectangle[] sourceRectangles = { GridToRectangle(6, 8, width, height), GridToRectangle(7, 8, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateCloudSprite()
        {
            int width = (int) SpriteSize.CloudX;
            int height = (int) SpriteSize.CloudY;

            Rectangle[] sourceRectangles = { GridToRectangle(0, 9, width, height), GridToRectangle(1, 9, width, height), 
                GridToRectangle(2, 9, width, height), GridToRectangle(3, 9, width, height), 
                GridToRectangle(4, 9, width, height), GridToRectangle(5, 9, width, height), 
                GridToRectangle(6, 9, width, height), GridToRectangle(7, 9, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }

        public ISprite CreateHitEffectSprite() 
        {
            return new StaticSprite(enemySpriteSheet, GridToRectangle(6, 10, 
               (int) SpriteSize.HitEffectX, (int) SpriteSize.HitEffectY));
        }
        
        public ISprite CreateEnemyDeathSprite()
        {
            int width = (int)SpriteSize.EnemyDeathX;
            int height = (int)SpriteSize.EnemyDeathY;
            Rectangle[] sourceRectangles = { GridToRectangle(0, 12, width, height), GridToRectangle(1, 12, width, height),
                GridToRectangle(2, 12, width, height), GridToRectangle(3, 12, width, height) };
            return new AnimatedSprite(enemySpriteSheet, sourceRectangles);
        }
    }

}