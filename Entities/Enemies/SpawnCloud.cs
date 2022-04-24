using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class SpawnCloud : IEnemy
    {
        public ISprite CloudSprite { get; private set; }
        public Rectangle CurrentLoc { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Floor; }
        public bool isFriendly { get => false; }

        public DrawLayer Layer { get => DrawLayer.Normal; }
        public EntityList roomEntities;
        private IEnemy contents;
        private Room r;
        private int remainingTime;
        private static readonly int MAX_TIME = 30;
        public SpawnCloud(Point position, Room r, IEnemy contents)
        {
            CloudSprite = EnemySpriteFactory.Instance.CreateCloudSprite();
            Point size = new Point((int)SpriteUtil.SpriteSize.CloudX * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.SpriteSize.CloudY * SpriteUtil.SCALE_FACTOR);
            CurrentLoc = new Rectangle(position, size);
            this.r = r;
            this.contents = contents;
            remainingTime = MAX_TIME;
        }

        public void Move() { }

        public void Attack() { }

        public void TakeDamage(Direction direction) { }

        public void Knockback(Direction direction) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            CloudSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            CloudSprite.Update();
            CloudSprite.Update(); // update twice because animation is too slow otherwise
            remainingTime--;
            if (remainingTime <= 0)
            {
                ReadyToDespawn = true;
            }
        }
        public void DespawnEffect() => r.RegisterEntity(contents);
        public bool ReadyToDespawn { get; private set; }
    }
}