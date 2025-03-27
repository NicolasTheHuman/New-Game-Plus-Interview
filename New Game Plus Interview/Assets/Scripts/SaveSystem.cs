using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Interview.SaveSystem
{
    public static class SaveSystem
    {
        private static string SavePath => Application.persistentDataPath + "/inventory.json";

        public static void SaveInventorySlots(InventoryUI inventoryUI, Inventory inventory)
        {
            InventoryData data = new InventoryData()
            {
                inventorySize = inventoryUI.InventorySlots.Count
            };

            for (int i = 0; i < inventoryUI.InventorySlots.Count; i++)
            {
                var slot = inventoryUI.InventorySlots[i];
                
                if(!slot)
                    continue;

                if (slot.IsEmpty)
                {
                    data.inventorySlots.Add(new InventoryDataSlot(null, 0, i));
                    continue;
                }
                
                data.inventorySlots.Add(new InventoryDataSlot(slot.ItemData, inventory.ItemsInDictionary.GetValueOrDefault(slot.ItemData, 0), i));
            }

            var json = JsonUtility.ToJson(data, true);
            
            File.WriteAllText(SavePath, json);
            
            Debug.Log($"Inventory saved to: {SavePath}");
        }

        public static InventoryData LoadInventory()
        {
            if (!File.Exists(SavePath)) 
                return null;
            
            var json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<InventoryData>(json);

        }
        
    }
}
