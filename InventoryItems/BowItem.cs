using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    public class BowItem : IItem
    {
        private ArrowItem normArr;
        private ArrowItem magicArr;
        private Game1 g;
        public bool Consumable { get => false; }
        public BowItem(Game1 g)
        {
            this.g = g;
            normArr = new ArrowItem(g, false);
            magicArr = new ArrowItem(g, true);
        }
        public void UseOn(ILink player) 
        {
            if (player.HasItem(magicArr))
            {
                player.UseItem(magicArr);
            }
            else if (player.HasItem(normArr))
            {
                player.UseItem(normArr);
            }
        }

        public bool CanUseOn(ILink player) => player.HasItem(normArr) || player.HasItem(magicArr);
        public bool Equals(IItem other)
        {
            return other is BowItem;
        }

        public override int GetHashCode() => "bow".GetHashCode();
        public bool Selectable => true;
    }
}
