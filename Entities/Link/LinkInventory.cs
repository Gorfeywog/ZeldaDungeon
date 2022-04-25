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
        public void AddItem(IItem item, int quantity = 1) 
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
            return new Dictionary<IItem, int>(itemCount); 
        }
        public int Size => itemCount.Count;
        public IEnumerable<IItem> HeldItems() 
        {
            foreach (var item in itemCount.Keys)
            {
                if (HasItem(item)) { yield return item; }
            }
        }
    }
}
