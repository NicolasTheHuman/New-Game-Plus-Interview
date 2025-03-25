using System;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    public ItemSO itemToDrop;
    public KeyCode key;

    private void Update()
    {
        if(Input.GetKeyDown(key))
            Interact();
    }

    public void Interact()
    {
        var inv = FindFirstObjectByType<Inventory>();
        
        if(!inv)
            return;
        
        inv.AddItem(itemToDrop);
    }
}
