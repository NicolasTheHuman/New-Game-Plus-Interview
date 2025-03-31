using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputs _playerInput;
    private PlayerAnimation _animationHandler;
    private Player _player;

    public float moveSpeed = 5f;

    private InputAction _moveAction;
    private InputAction _interactAction;
    
    private IInteractable _currentInteractable;

    public PlayerAnimation AnimationHandler => _animationHandler;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _player = GetComponent<Player>().SetController(this);
        _playerInput = new PlayerInputs();
        _moveAction = _playerInput.FindAction("Move");
        _interactAction = _playerInput.FindAction("Interact");
        _animationHandler = GetComponentInChildren<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        Movement();
        Interact();
    }

    private void Movement()
    {
        var dir = _moveAction.ReadValue<Vector2>();
        var moveDir = new Vector3(dir.x, 0f, dir.y).normalized;

        if (moveDir.magnitude < 0.1f)
        {
            _animationHandler.SetMoving(false, Vector2.zero);
            return;
        }

        transform.rotation = Quaternion.LookRotation(moveDir);

        _characterController.Move(moveDir * (moveSpeed * Time.deltaTime));
        _animationHandler.SetMoving(true, new Vector2(moveDir.x, moveDir.z));
    }

    private void Interact()
    {
        if(_currentInteractable == null || _interactAction.ReadValue<float>() <= 0.5f)
            return;

        _currentInteractable.Interact();
        _currentInteractable = null;
        
        _animationHandler.PlayInteract();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
            _currentInteractable = interactable;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == _currentInteractable)
            _currentInteractable = null;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
