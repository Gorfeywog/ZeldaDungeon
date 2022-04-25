using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Commands;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class SpecialTrigger : IBlock
    {
        private bool hasTriggered;
        private bool Repeatable;
        public CollisionHeight Height { get => CollisionHeight.Floor; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        private ICommand effect;
        public Rectangle CurrentLoc { get; set; }
        public SpecialTrigger(Point position, bool Repeatable = false)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX / 2; // smaller than a normal block so Link can't accidentally hit it
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY / 2;
            Point adjustedPosition = position + new Point(width, height); // spawn it in center, not in top left
            CurrentLoc = new Rectangle(adjustedPosition, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            hasTriggered = false;
            this.Repeatable = Repeatable;
        }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Update() { }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
        public void Trigger()
        {
            if (effect != null && (!hasTriggered || Repeatable))
            {
                hasTriggered = true;
                effect.Execute();
            }
        }
        public void RegisterEffect(ICommand effect)
        {
            this.effect = effect;
        }
    }
}

