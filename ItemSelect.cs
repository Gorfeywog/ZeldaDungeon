using System.Collections.Generic;
using ZeldaDungeon.Entities;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon
{
    public class ItemSelect
    {
        private ILink link;
        private int selectionIndex = -1; // may be -1 for no selection
        private IList<IItem> selectables;

        public ItemSelect(ILink link)
        {
            this.link = link;
        }
        public void Update()
        {
            // regenerating selectables every frame is probably a bad idea for performance, but there are worse ideas in this game
            selectables = Selectables();
        }
        public IItem SelectedItem() // may return null
        {
            if (selectionIndex == -1)
            {
                return null;
            }
            else if (selectionIndex >= selectables.Count)
            {
                selectionIndex = -1; // bad selection! reset to nothing!
                return null;
            }
            else return selectables[selectionIndex];
        }
        public void IncSelection()
        {
            if (selectables.Count == 0)
            {
                selectionIndex = -1;
                return;
            }
            else
            {
                selectionIndex = (selectionIndex + 1) % selectables.Count;
            }
        }
        public void DecSelection()
        {
            if (selectionIndex > 0)
            {
                selectionIndex--;
            }
            else
            {
                selectionIndex = selectables.Count - 1;
            }
        }
        private IList<IItem> Selectables()
        {
            var selectables = new List<IItem>();
            foreach (var item in link.GetInv().HeldItems())
            {
                if (item.Selectable) { selectables.Add(item); }
            }
            selectables.Sort(CompareItemsByMagic);
            return selectables;
        }
        private static int CompareItemsByMagic(IItem x, IItem y)
        {
            if (!x.Selectable || !y.Selectable) { return 0; } // who cares about order if neither can be selected?
            return SortScore(x) - SortScore(y);
        }
        // used to sort items following the same (fairly arbitrary) ordering used in the pause menu
        // ordered as: bow, boomerang, magic boomerang, red candle, blue candle, bomb
        // do not try to apply any real meaning to these.
        private static int SortScore(IItem x)
        {
            int score = 0;
            if (x is BowItem)
            {
                score = 10;
            }
            else if (x is BoomerangItem xBoom)
            {
                score = xBoom.IsMagic ? 8 : 9;
            }
            else if (x is CandleItem xCand)
            {
                score = xCand.IsRed ? 7 : 6;
            }
            else if (x is BombItem)
            {
                score = 5;
            }
            return score;
        }
    }
}
