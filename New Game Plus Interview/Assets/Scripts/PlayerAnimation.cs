using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private readonly int _moveSpeed_ParamName = Animator.StringToHash("IsMoving");
    private readonly int _interact_ParamName = Animator.StringToHash("Interact");
    private readonly int _horizontal_ParamName = Animator.StringToHash("Horizontal");
    private readonly int _vertical_ParamName = Animator.StringToHash("Vertical");
    private readonly int _equip_ParamName = Animator.StringToHash("Equip");
    private readonly int _unequip_ParamName = Animator.StringToHash("Unequip");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving, Vector2 moveDir)
    {
        _animator.SetBool(_moveSpeed_ParamName, isMoving);
        _animator.SetFloat(_horizontal_ParamName, moveDir.x);
        _animator.SetFloat(_vertical_ParamName, moveDir.y);
    }

    public void PlayInteract()
    {
        _animator.SetTrigger(_interact_ParamName);
    }

    public void PlayEquipUnequip(bool equip)
    {
        if (equip)
        {
            _animator.SetTrigger(_equip_ParamName);
            return;
        }
        
        _animator.SetTrigger(_unequip_ParamName);
    }
    
}
