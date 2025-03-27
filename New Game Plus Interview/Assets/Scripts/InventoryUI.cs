using System;
using System.Collections.Generic;
using Interview.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventorySlot itemSlotPrefab;
    
    public int startingInventorySize = 12;
    public int expandStep = 4;
    
    public Transform inventoryPanel;
    public Inventory inventory;

    private readonly List<InventorySlot> _uiSlots = new ();
    public List<InventorySlot> InventorySlots => _uiSlots;
    
    private int _slotsOccupied = 0;

    private void Start()
    {
        _uiSlots.Clear();
        CreateInventorySlots(startingInventorySize);
        inventory.OnItemAdded += AddItem;
        inventory.OnLoad += LoadUI;
    }

    private void LoadUI(InventoryData inventoryData)
    {
        
        //Create all missing inventory slots
        for (int i = startingInventorySize; i < inventoryData.inventorySize; i++)
        {
            var inventorySlot = Instantiate(itemSlotPrefab, inventoryPanel);
            _uiSlots.Add(inventorySlot);
        }

        //populate used slots
        foreach (var dataSlot in inventoryData.inventorySlots)
        {
            var currentSlot = _uiSlots[dataSlot.slotIndex];
            var itemData = inventory.GetItemByID(dataSlot.itemID);
            
            if(!currentSlot || !itemData)
                continue;
            
            currentSlot.ItemAdded(itemData);
            currentSlot.UpdateText($"x {inventory.ItemsInDictionary[itemData]}");
            _slotsOccupied++;
        }
    }

    public void SaveInventory()
    {
        SaveSystem.SaveInventorySlots(this, inventory);
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
        if(_slotsOccupied >= _uiSlots.Count)
            CreateInventorySlots(expandStep);
        
        //Check if item exist in inventory
        foreach (var slot in _uiSlots)
        {
            if(slot.IsEmpty)
                continue;
            
            if(slot.ItemData != item)
                continue;
            
            slot.UpdateText($"x{inventory.ItemsInDictionary[item]}");
            
            SaveInventory();
            return;
        }

        //Check for empty slot
        foreach (var slot in _uiSlots)
        {
            if(!slot.IsEmpty)
                continue;

            slot.ItemAdded(item);
            slot.UpdateText($"x{amount}");
            
            _slotsOccupied++;
            SaveInventory();
            return;
        }
    }

    public void RemoveItem(ItemSO item, int amount)
    {
        for (int i = 0; i < _uiSlots.Count; i++)
        {
            if (_uiSlots[i].ItemData != item) 
                continue;
            
            if (inventory.ItemsInDictionary[item] <= 0)//Clear slot
            {
                _uiSlots[i].ItemConsumed();
                _slotsOccupied--;
            }
            else
                _uiSlots[i].UpdateText($"{inventory.ItemsInDictionary[item]}");

            SaveInventory();
            return;
        }
    }

    private void OnDestroy()
    {
        inventory.OnItemAdded -= AddItem;
        inventory.OnLoad -= LoadUI;
        SaveInventory();
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }
}
