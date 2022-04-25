using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Commands;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class SpecialTrigger : IBlock
    {
        public CollisionHeight Height { get => CollisionHeight.Floor; }
        public DrawLayer Layer { get => DrawLayer.Low; }
        private ICommand effect;
        public Rectangle CurrentLoc { get; set; }
        public SpecialTrigger(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX / 2; 
            int height = (int)SpriteUtil.SpriteSize.GenericBlockY / 2;
            Point adjustedPosition = position + new Point(width, height); 
            CurrentLoc = new Rectangle(adjustedPosition, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR)); ;
        }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Update() { }
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
        public void Trigger()
        {
            if (effect != null) effect.Execute();
        }
        public void RegisterEffect(ICommand effect)
        {
            this.effect = effect;
        }
    }
}

