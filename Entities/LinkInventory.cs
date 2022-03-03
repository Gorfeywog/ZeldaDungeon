using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public class LinkInventory
    {
        private IDictionary<string, int> itemCount; // maps item names to counts. using names is kinda weird, but IItems don't really work for this either?
                                                    // all values must be positive, we just remove entries if count drops to 0.

        public LinkInventory()
        {
            itemCount = new Dictionary<string, int>();
        }

        // returns true and decrements count if we can consume the item, if count was zero returns false
        public bool UseItem(string itemName)
        {
            if (itemCount.ContainsKey(itemName))
            {
                int ct = itemCount[itemName];
                itemCount[itemName] = ct - 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PickupItem(string itemName)
        {
            int ct = itemCount.ContainsKey(itemName) ? itemCount[itemName] : 0;
            itemCount[itemName] = ct + 1;
        }
        public bool HasItem(string itemName)
        {
            return itemCount.ContainsKey(itemName);
        }
    }
}
