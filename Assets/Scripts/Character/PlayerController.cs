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
        _Rigidbody ??= GetComponent<Rigidbody>();
        _InteractorBehavoir ??= GetComponent<InteractorBehavoir>();

        _PlayerActions = new PlayerActions();
        _PlayerActions.PlayerActionMap.Enable();

        //Generally this is how we can bind inputs...
        //Either .performed for a specified trigger or .started
        //_PlayerActions.PlayerActionMap.InteractPressed.performed += OnInteractInput;
        //_PlayerActions.PlayerActionMap.InteractReleased.performed += OnInteractStop;
        _PlayerActions.PlayerActionMap.Interact.started += OnInteractStart;
        _PlayerActions.PlayerActionMap.Interact.canceled += OnInteractCancelled;
    }

    private void FixedUpdate()
    {
        if (_Rigidbody && PlayerState)
        {
            Vector2 _PlayerMoveInput = _PlayerActions.PlayerActionMap.Move.ReadValue<Vector2>();
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

    }
}
