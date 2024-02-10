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

    public Animator frontAnimator;
    public Animator backAnimator;
    private bool faceLeft = true;
    private bool faceBack = false;

    private void Awake()
    {
        _Rigidbody ??= GetComponent<Rigidbody>();
        _InteractorBehavoir ??= GetComponent<InteractorBehavoir>();

        _PlayerActions = new PlayerActions();
        _PlayerActions.PlayerActionMap.Enable();
        
        frontAnimator = transform.Find("F_BaseCharacter").GetComponent<Animator>();
        backAnimator = transform.Find("B_BaseCharacter").GetComponent<Animator>();

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

            frontAnimator.SetFloat("MoveSpeed", _Rigidbody.velocity.magnitude);
            backAnimator.SetFloat("MoveSpeed", _Rigidbody.velocity.magnitude);

            if (_PlayerMoveInput.x > 0 && faceLeft)
            {
                transform.Find("F_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                transform.Find("B_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                faceLeft = false;
            }
            else if (_PlayerMoveInput.x < 0 && !faceLeft)
            {
                transform.Find("F_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                transform.Find("B_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                faceLeft = true;
            }

            if (_PlayerMoveInput.y > 0 && !faceBack)
            {
                frontAnimator.Rebind();
                transform.Find("F_BaseCharacter").gameObject.SetActive(false);
                faceBack = true;
                transform.Find("B_BaseCharacter").gameObject.SetActive(true);
            }
            else if (_PlayerMoveInput.y < 0 && faceBack)
            {
                backAnimator.Rebind();
                transform.Find("F_BaseCharacter").gameObject.SetActive(true);
                faceBack = false;
                transform.Find("B_BaseCharacter").gameObject.SetActive(false);
            }


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
