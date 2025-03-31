using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform equipmentTransform;
    
    private float _hunger;

    private GameObject _equippedObject;
    private PlayerController _playerController;

    public GameObject EquippedObject => _equippedObject;
    
    public Player SetController(PlayerController controller)
    {
        _playerController = controller;
        return this;
    }
    
    public void EatFood(float fillAmount)
    {
        _hunger += fillAmount;
        //Update Hunger Slider
    }

    public void EquipItem(GameObject itemToEquip)
    {
        if(_equippedObject)
            Destroy(_equippedObject);
        
        _equippedObject = Instantiate(itemToEquip, equipmentTransform);
        //Equip Animation
        _playerController.AnimationHandler.PlayEquipUnequip(true);
    }

    public void UnequipItem()
    {
        _playerController.AnimationHandler.PlayEquipUnequip(false);
        Destroy(_equippedObject);
        _equippedObject = null;
    }
}
