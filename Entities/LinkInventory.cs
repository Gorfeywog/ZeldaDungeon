using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public class LinkInventory
    {
        private IDictionary<IItem, int> itemCount;

        public LinkInventory()
        {
            itemCount = new Dictionary<IItem, int>();
        }

        // returns true and decrements count if we can consume the item, if count was zero returns false
        public bool UseItem(IItem item)
        {
            if (itemCount.ContainsKey(item))
            {
                int ct = itemCount[item];
                itemCount[item] = item.Consumable ? ct - 1 : ct; // only decrement if it's consumable
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddItem(IItem item)
        {
            int ct = itemCount.ContainsKey(item) ? itemCount[item] : 0;
            itemCount[item] = ct + 1;
        }
        public bool HasItem(IItem item)
        {
            return itemCount.ContainsKey(item);
        }
    }
}
