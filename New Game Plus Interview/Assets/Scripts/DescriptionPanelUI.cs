using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanelUI : MonoBehaviour
{
    public static DescriptionPanelUI Instance;

    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _itemInfo;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        HidePanel();
    }

    public void ShowItemDescription(ItemSO itemData)
    {
        _itemImage.sprite = itemData.itemSprite;
        _itemName.text = itemData.itemName;
        _itemInfo.text = itemData.itemDescription;
        _canvasGroup.alpha = 1;
    }

    private void HidePanel()
    {
        _canvasGroup.alpha = 0;
    }
}
