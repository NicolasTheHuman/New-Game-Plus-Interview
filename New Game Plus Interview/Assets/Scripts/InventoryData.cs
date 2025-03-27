using System;
using System.Collections.Generic;

namespace Interview.SaveSystem
{
    [Serializable]
    public class InventoryData
    {
        public int inventorySize;
        public List<InventoryDataSlot> inventorySlots = new List<InventoryDataSlot>();
    }

    [Serializable]
    public class InventoryDataSlot
    {
        public int slotIndex;
        public string itemID;
        public int quantity;

        public InventoryDataSlot(ItemSO item, int amount, int index)
        {
            itemID = item != null ? item.itemName : "";
            quantity = amount;
            slotIndex = index;
        }
    }
}
