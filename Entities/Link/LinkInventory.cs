using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.Entities.Link
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
                itemCount[item] = item.Consumable ? ct - 1 : ct; 
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddItem(IItem item, int quantity = 1) // quantity must be positive or bad things may happen
        {
            int ct = itemCount.ContainsKey(item) ? itemCount[item] : 0;
            itemCount[item] = ct + quantity;
        }
        public bool HasItem(IItem item)
        {
            return itemCount.ContainsKey(item) && itemCount[item] > 0;
        }

        public IDictionary<IItem, int> GetDict()
        {
            return itemCount;
        }
    }
}
