using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.MiscEffects;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Projectiles
{
    public class BombProjectile : IProjectile
    {
        private static readonly int FUSE_TIME = 60;
        private int timer = FUSE_TIME; // counts down
        private ISprite sprite = ItemSpriteFactory.Instance.CreateBomb(); 
        public Rectangle CurrentLoc { get; set; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public bool ReadyToDespawn { get => timer <= 0; }
        private Game1 g;
        private Room r => g.CurrentRoom;
        public BombProjectile(Point position, Game1 g)
        {
            int width = (int)SpriteUtil.SpriteSize.BombWidth;
			int height = (int)SpriteUtil.SpriteSize.BombLength;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            this.g = g;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        private const float EXPLOSION_RADIUS = 30; 
        public void DespawnEffect()
        {
            r.RegisterEntity(new SmokeCloud(CurrentLoc.Location));
            foreach (var entity in r.roomEntities)
            {
                if (entity is IEnemy anEnemy && WillBeHit(anEnemy))
                {
                    anEnemy.TakeDamage(DamageLevel.Heavy);
                }
                if (entity is Door d && WillBeHit(d))
                {
                    g.ExplodeRoomDoor(d.Dir);
                }
            }
        }
        private bool WillBeHit(IEntity target)
        {
            Point p = target.CurrentLoc.Center;
            Point offset = CurrentLoc.Center - p;
            return offset.ToVector2().Length() < EXPLOSION_RADIUS * SpriteUtil.SCALE_FACTOR;
        }
        public void OnHit(IEntity target) { }
        public void Update()
        {
            sprite.Update();
            timer--;
        }
    }
}
