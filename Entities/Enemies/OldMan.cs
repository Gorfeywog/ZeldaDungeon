using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Enemies
{
    public class OldMan : IEnemy
    {
        public ISprite OldManSprite { get; set; }
        public bool ReadyToDespawn => false;

        public Rectangle CurrentLoc { get; set; }
        private CollisionHandler Collision { get; set; }
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public EntityList roomEntities;
        public OldMan(Point position, Room r)
        {
            OldManSprite = EnemySpriteFactory.Instance.CreateOldManSprite();
            //TODO Fix old man being drawn off center in a less concrete manner
            CurrentLoc = new Rectangle(new Point(position.X - (8 * SpriteUtil.SCALE_FACTOR), position.Y),
                new Point((int)SpriteUtil.SpriteSize.OldManX * SpriteUtil.SCALE_FACTOR,
                (int)SpriteUtil.SpriteSize.OldManY * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
        }

        public void UpdateList(EntityList roomEntities)
        {
            this.roomEntities = roomEntities;
        }

        public void Move()
        {

        }

        public void Attack()
        {

        }

        public void TakeDamage()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            OldManSprite.Draw(spriteBatch, CurrentLoc);
        }

        public void Update()
        {
            OldManSprite.Update();
        }
        public void DespawnEffect() { }
    }
}