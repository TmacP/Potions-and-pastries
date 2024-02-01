using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{
    private PlayerActions _PlayerActions;
    public PlayerStateData PlayerState;
    private Rigidbody _Rigidbody;
    private InteractorBehavoir _InteractorBehavoir;


    private void Awake()
    {
        Debug.Log("Player Awake");
        _Rigidbody ??= GetComponent<Rigidbody>();
        _InteractorBehavoir ??= GetComponent<InteractorBehavoir>();

        _PlayerActions = new PlayerActions();
        _PlayerActions.PlayerActionMap.Enable();
        _PlayerActions.PlayerMovementMap.Enable();

        //Generally this is how we can bind inputs...
        //Either .performed for a specified trigger or .started/.cancelled
        //_PlayerActions.PlayerActionMap.InteractPressed.performed += OnInteractInput;
        //_PlayerActions.PlayerActionMap.InteractReleased.performed += OnInteractStop;
        //I didn't think we needed to remove them ondisable but apperently we do or the scene changes get weird
        _PlayerActions.PlayerActionMap.Interact.started += OnInteractStart;
        _PlayerActions.PlayerActionMap.Interact.canceled += OnInteractCancelled;
        _PlayerActions.PlayerActionMap.Inventory.performed += OnToggleInventory;
    }

    public void Start()
    {
        GameEventManager.instance.OnChangeGameState += OnGameStateChanged;
    }

    public void OnDisable()
    {
        _PlayerActions.PlayerActionMap.Interact.started -= OnInteractStart;
        _PlayerActions.PlayerActionMap.Interact.canceled -= OnInteractStart;
        _PlayerActions.PlayerActionMap.Inventory.performed -= OnToggleInventory;
    }

    protected void OnGameStateChanged(EGameState NewGameState, EGameState OldGameState)
    {
        switch (NewGameState)
        {
            case EGameState.MainState:
                _PlayerActions.PlayerActionMap.Enable();
                _PlayerActions.PlayerMovementMap.Enable();
                break;
            case EGameState.PauseState:
                _PlayerActions.PlayerActionMap.Disable();
                _PlayerActions.PlayerMovementMap.Disable();
                break;
            case EGameState.MovementDisabledState:
                _PlayerActions.PlayerActionMap.Enable();
                _PlayerActions.PlayerMovementMap.Disable();
                break;
            default:
                Debug.Log("Gamemanager::ChangeGameState unknown game state given");
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_Rigidbody && PlayerState)
        {
            Vector2 _PlayerMoveInput = _PlayerActions.PlayerMovementMap.Move.ReadValue<Vector2>();
            _Rigidbody.velocity = new Vector3(
                _PlayerMoveInput.x * PlayerState.MoveSpeed,
                _Rigidbody.velocity.y,
                _PlayerMoveInput.y * PlayerState.MoveSpeed);
        }
    }

    protected void OnInteractStart(InputAction.CallbackContext context)
    {
        if(_InteractorBehavoir != null)
        {
            _InteractorBehavoir.TryInteract();
        }
    }

    protected void OnInteractCancelled(InputAction.CallbackContext context)
    {
        if (_InteractorBehavoir != null)
        {
            _InteractorBehavoir.InteractReleased();
        }
    }

    protected void OnToggleInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Toggling Player Controller");
            GameEventManager.instance.ToggleInventory();
        }
    }


}
