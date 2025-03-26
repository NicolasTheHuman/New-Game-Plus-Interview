using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public InventorySlot parentInventorySlot;
    private Transform _parentTransform;
    private int _orderInLayer;
    private Vector3 _imagePosition;
    private Image _itemImage;

    private void Awake()
    {
        _itemImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var myTransform = transform;
        _parentTransform = myTransform.parent;
        _imagePosition = myTransform.position;
        _orderInLayer = transform.GetSiblingIndex();
        
        transform.SetParent(myTransform.root);
        transform.SetAsLastSibling();
        
        _itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentTransform);
        transform.SetSiblingIndex(_orderInLayer);
        transform.position = _imagePosition;
        
        _itemImage.raycastTarget = true;
        
        //If moved to an empty slot in the inventory the previous slot needs to clear the data
        if(parentInventorySlot.IsEmpty)
            parentInventorySlot.ItemConsumed();
    }
}
