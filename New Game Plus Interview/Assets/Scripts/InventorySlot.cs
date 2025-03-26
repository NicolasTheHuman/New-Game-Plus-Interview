using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private ItemSO _itemData;
    public ItemSO ItemData => _itemData;
    
    [SerializeField]
    private Image _itemImage;
    
    [SerializeField] 
    private TextMeshProUGUI _amountText;

    public bool IsEmpty => !_itemData;

    private void Start()
    {
        _itemImage.gameObject.SetActive(false);
    }

    public void ItemAdded(ItemSO item)
    {
        _itemData = item;
        _itemImage.sprite = item.itemSprite;
        _itemImage.gameObject.SetActive(true);
    }

    public void ItemConsumed()
    {
        _itemImage.sprite = null;
        _itemImage.gameObject.SetActive(false);
        _amountText.text = "";
        _itemData = null;
    }

    public void UpdateText(string newText)
    {
        _amountText.text = newText;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        
        if (droppedItem && droppedItem.parentInventorySlot != this)
            SwapItems(droppedItem.parentInventorySlot, this);
    }

    private void SwapItems(InventorySlot fromSlot, InventorySlot toSlot)
    {
        var tempItem = fromSlot._itemData;
        var tempText = fromSlot._amountText.text;

        //Check if the toSlot has an item or is an empty slot
        if (!toSlot.IsEmpty)
        {
            fromSlot.ItemAdded(toSlot.ItemData);
            fromSlot._amountText.text = toSlot._amountText.text;
        }
        else
        {
            fromSlot._itemData = null;
        }

        toSlot.ItemAdded(tempItem);
        toSlot._amountText.text = tempText;
    }
}
