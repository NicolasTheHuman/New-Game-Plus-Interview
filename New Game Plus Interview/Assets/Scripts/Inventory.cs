using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemSO, int> _itemsInInventory = new();

    public Dictionary<ItemSO, int> ItemsInDictionary => _itemsInInventory;

    public Action<ItemSO, int> OnItemAdded;
    
    public void AddItem(ItemSO item, int amount = 1)
    {
        if (!_itemsInInventory.ContainsKey(item))
        {
            _itemsInInventory.TryAdd(item, Mathf.Min(amount, item.maxStackSize));
            OnItemAdded?.Invoke(item, amount);
            return;
        }

        var oldAmount = _itemsInInventory[item];
        //If the item already exist add a stack 
        _itemsInInventory[item] = Mathf.Min(_itemsInInventory[item] + amount, item.maxStackSize);
        
        if(oldAmount < _itemsInInventory[item])//If one item was added to the stack
            OnItemAdded?.Invoke(item, amount);
    }

    public void RemoveItem(ItemSO item, int amount = 1)
    {
        if (!_itemsInInventory.ContainsKey(item)) 
            return;
        
        _itemsInInventory[item] -= amount;
            
        if(_itemsInInventory[item] <= 0)
            _itemsInInventory.Remove(item);
    }
}
