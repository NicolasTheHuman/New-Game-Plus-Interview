using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventorySlot itemSlotPrefab;
    public GridLayoutGroup gridLayout;
    
    public int startingInventorySize = 12;
    public int expandStep = 4;
    
    public Transform inventoryPanel;
    public Inventory inventory;

    private readonly List<InventorySlot> _uiSlots = new ();
    private readonly Dictionary<ItemSO, InventorySlot> _inventoryItems = new ();
    private void Start()
    {
        _uiSlots.Clear();
        _inventoryItems.Clear();
        CreateInventorySlots(startingInventorySize);
        inventory.OnItemAdded += AddItem;
    }

    void CreateInventorySlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var inventorySlot = Instantiate(itemSlotPrefab, inventoryPanel);
            _uiSlots.Add(inventorySlot);
        }
    }

    private void AddItem(ItemSO item, int amount)
    {
        //Check if item exist in inventory
        if (_inventoryItems.TryGetValue(item, out var inventoryItem))
        {
            inventoryItem.UpdateText($"x{inventory.ItemsInDictionary[item]}");
            return;
        }

        //Check for empty slot
        foreach (var slot in _uiSlots)
        {
            if(!slot.IsEmpty)
                continue;
            
            slot.ItemAdded(item.itemSprite);
            slot.UpdateText($"x{amount}");
            _inventoryItems.TryAdd(item, slot);
            return;
        }
        
        //If there are no empty slots add an extra row
        var oldSlotCount = _uiSlots.Count;
        CreateInventorySlots(expandStep);
        
        if(_uiSlots.Count > oldSlotCount)//Avoid infinite recursion if something goes wrong
            AddItem(item, amount);
    }

    public void RemoveItem(ItemSO item, int amount)
    {
        for (int i = 0; i < _uiSlots.Count; i++)
        {
            if (_uiSlots[i].ItemImage.sprite != item.itemSprite) 
                continue;
            
            if (inventory.ItemsInDictionary[item] <= 0)//Clear slot
            {
                _uiSlots[i].ItemConsumed();
                _inventoryItems.Remove(item);
            }
            else
                _uiSlots[i].UpdateText($"{_inventoryItems[item]}");

            return;
        }
    }
}
