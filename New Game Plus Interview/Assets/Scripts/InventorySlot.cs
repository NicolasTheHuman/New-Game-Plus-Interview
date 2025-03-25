using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    public Image ItemImage => _itemImage;

    [SerializeField] 
    private TextMeshProUGUI _amountText;

    private bool _isEmpty = true;
    public bool IsEmpty => _isEmpty;

    private void Start()
    {
        _itemImage.gameObject.SetActive(false);
    }

    public void ItemAdded(Sprite itemSprite)
    {
        _itemImage.sprite = itemSprite;
        _itemImage.gameObject.SetActive(true);
        _isEmpty = false;
    }

    public void ItemConsumed()
    {
        _itemImage.sprite = null;
        _itemImage.gameObject.SetActive(false);
        _amountText.text = "";
        _isEmpty = true;
    }

    public void UpdateText(string newText)
    {
        _amountText.text = newText;
    }
}
