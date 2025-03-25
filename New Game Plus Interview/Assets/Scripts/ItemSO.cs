using UnityEngine;

[CreateAssetMenu(menuName = "Item SO", fileName = "NewItem")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int maxStackSize = 1;
    public ItemType itemType;
}

public enum ItemType
{
    Cookware, Ingredients, Food
}
