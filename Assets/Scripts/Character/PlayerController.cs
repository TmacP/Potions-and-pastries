using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{
    private PlayerActions _PlayerActions;
    private Rigidbody _Rigidbody;
    private InteractorBehavoir _InteractorBehavoir;
    private InputAction _menuOpenCloseACtion;
    public bool MenuOpenCloseInput { get; private set;}

    [SerializeField] private InventoryManager _InventoryManager;
    [SerializeField] private InventoryToggle _InventoryToggle;

    //if we cannot find a gamemanager and playerstate use this speed instead.
    //This is so players don't break on levels without a gamemanager
    private readonly float _fallbackSpeed = 20.0f;


    private void Awake()
    {
        Debug.Log("Player Awake");
        _Rigidbody ??= GetComponent<Rigidbody>();
        _InteractorBehavoir ??= GetComponent<InteractorBehavoir>();

        _PlayerActions = new PlayerActions();
        _PlayerActions.PlayerActionMap.Enable();
        _PlayerActions.PlayerMovementMap.Enable();
        _PlayerActions.Inventory.Disable();

        //Generally this is how we can bind inputs...
        //Either .performed for a specified trigger or .started/.cancelled
        //_PlayerActions.PlayerActionMap.InteractPressed.performed += OnInteractInput;
        //_PlayerActions.PlayerActionMap.InteractReleased.performed += OnInteractStop;
        //I didn't think we needed to remove them ondisable but apperently we do or the scene changes get weird
        _PlayerActions.PlayerActionMap.Interact.started += OnInteractStart;
        _PlayerActions.PlayerActionMap.Interact.canceled += OnInteractCancelled;
        _PlayerActions.PlayerActionMap.OpenInventory.performed += OnOpenInventory;
        _PlayerActions.Inventory.CloseInventory.performed += OnCloseInventory;
        //_PlayerActions.PlayerActionMap.MenuOpenClose.performed += OnMenuOpen;
        _menuOpenCloseACtion = _PlayerActions.PlayerActionMap.MenuOpenClose;
    }

    public void Start()
    {
        GameEventManager.instance.OnChangeGameState += OnGameStateChanged;
        if(_InventoryManager != null)
        {
            _InventoryManager = Instantiate(_InventoryManager);
            _InventoryManager.InitializeInventoryManager(GameManager.Instance.PlayerState.Inventory);
            _InventoryToggle = _InventoryManager.GetComponent<InventoryToggle>();
        }
    }
    private void Update()
    {
        MenuOpenCloseInput = _menuOpenCloseACtion.WasPressedThisFrame();
    }

    public void OnDisable()
    {
        _PlayerActions.PlayerActionMap.Interact.started -= OnInteractStart;
        _PlayerActions.PlayerActionMap.Interact.canceled -= OnInteractStart;
        _PlayerActions.PlayerActionMap.OpenInventory.performed -= OnOpenInventory;
        _PlayerActions.Inventory.CloseInventory.performed -= OnCloseInventory;
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
        if (_Rigidbody)
        {
            float Speed = _fallbackSpeed;
            if(GameManager.Instance && GameManager.Instance.PlayerState)
            {
                Speed = GameManager.Instance.PlayerState.MoveSpeed;
            }

            Vector2 _PlayerMoveInput = _PlayerActions.PlayerMovementMap.Move.ReadValue<Vector2>();
            _Rigidbody.velocity = new Vector3(
                _PlayerMoveInput.x * Speed,
                _Rigidbody.velocity.y,
                _PlayerMoveInput.y * Speed);
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

    protected void OnOpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(_InventoryToggle == null)
            {
                GameObject[] Objects = GameObject.FindGameObjectsWithTag("PlayerInventoryHUD");
                
                foreach (GameObject obj in Objects)
                {
                    InventoryToggle toggle = obj.GetComponent<InventoryToggle>();
                    if(toggle != null)
                    {
                        _InventoryToggle = toggle;
                        break;
                    }
                }
            }

            if(_InventoryToggle != null)
            {
                if(_InventoryToggle.Toggle())
                {
                    _PlayerActions.PlayerMovementMap.Disable();
                    _PlayerActions.PlayerActionMap.Disable();
                    _PlayerActions.Inventory.Enable();
                }
                else
                {
                    _PlayerActions.PlayerMovementMap.Enable();
                    _PlayerActions.PlayerActionMap.Enable();
                    _PlayerActions.Inventory.Disable();
                }
            }
            GameEventManager.instance.ToggleInventory();
        }
    }

    protected void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_InventoryToggle != null)
            {
                if (_InventoryToggle.Toggle())
                {
                    _PlayerActions.PlayerMovementMap.Disable();
                    _PlayerActions.PlayerActionMap.Disable();
                    _PlayerActions.Inventory.Enable();
                }
                else
                {
                    _PlayerActions.PlayerMovementMap.Enable();
                    _PlayerActions.PlayerActionMap.Enable();
                    _PlayerActions.Inventory.Disable();
                }
            }
            GameEventManager.instance.ToggleInventory();
        }
    }

  


}
