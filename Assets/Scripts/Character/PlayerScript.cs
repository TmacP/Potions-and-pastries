using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionScript : MonoBehaviour
{
    private PlayerActions _PlayerActions;
    public PlayerStateData PlayerState;
    private Rigidbody _Rigidbody;
    private InteractorBehavoir _InteractorBehavoir;
    private Vector3 _PlayerMoveInput;

    private void Awake()
    {
        _Rigidbody ??= GetComponent<Rigidbody>();
        _InteractorBehavoir ??= GetComponent<InteractorBehavoir>();

        _PlayerActions = new PlayerActions();
        _PlayerActions.PlayerActionMap.Enable();

        //Generally this is how we can bind inputs...
        _PlayerActions.PlayerActionMap.Interact.performed += Interacting => { OnInteractInput(); };
    }

    private void FixedUpdate()
    {
        if (_Rigidbody && PlayerState)
        {
            _PlayerMoveInput = _PlayerActions.PlayerActionMap.Move.ReadValue<Vector2>();
            _Rigidbody.velocity = new Vector3(
                _PlayerMoveInput.x * PlayerState.MoveSpeed,
                _Rigidbody.velocity.y,
                _PlayerMoveInput.y * PlayerState.MoveSpeed);
        }
    }

    protected void OnInteractInput()
    {
        if(_InteractorBehavoir != null)
        {
            _InteractorBehavoir.TryInteract();
        }
    }
}
