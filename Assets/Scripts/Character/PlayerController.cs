using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;


    public PlayerActions _PlayerActions;
    private Rigidbody _Rigidbody;
    private InteractorBehavoir _InteractorBehavoir;
    private InputAction _menuOpenCloseACtion;
    public bool MenuOpenCloseInput { get; private set;}

    [SerializeField] private GameObject _InventoryPrefab;
    [SerializeField, HideInInspector] public InventoryManager _InventoryManager;
    
    [SerializeField] private GameObject _HotBarPrefab;
    public Toolbar toolbar;
    public Animator frontAnimator;
    public Animator backAnimator;
    private bool faceBack = false;
    private bool faceLeft = true;

    

    //if we cannot find a gamemanager and playerstate use this speed instead.
    //This is so players don't break on levels without a gamemanager
    private readonly float _fallbackSpeed = 20.0f;


    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _Rigidbody ??= GetComponent<Rigidbody>();
        _InteractorBehavoir ??= GetComponent<InteractorBehavoir>();

        _PlayerActions = new PlayerActions();
        _PlayerActions.PlayerActionMap.Enable();
        
        _PlayerActions.PlayerMovementMap.Enable();
        _PlayerActions.Inventory.Disable();
        frontAnimator = transform.Find("F_BaseCharacter").GetComponent<Animator>();
        backAnimator = transform.Find("B_BaseCharacter").GetComponent<Animator>();

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
        GameEventManager.instance.OnGivePlayerItems += OnGainItems;
        GameEventManager.instance.OnRemovePlayerItems += OnRemoveItems;
        GameEventManager.instance.OnPostInventoryOpen += PostInventoryOpen;
        GameEventManager.instance.OnCloseMenu += CloseInventory_Internal;

        GameObject[] GOS = GameObject.FindGameObjectsWithTag("PlayerHUD");
        
        if(GOS.Length > 0)
        {
            GameObject HUD = GOS[0];

            GameObject Toolbar = Instantiate(_HotBarPrefab);
            Toolbar.transform.SetParent(HUD.transform, false);
            Toolbar.transform.SetAsFirstSibling();

            InventoryManager TBManager = Toolbar.GetComponent<InventoryManager>();
            if (TBManager != null)
            {
                TBManager.InitializeInventoryManager(GameManager.Instance.PlayerState.ToolBar);
                TBManager.CloseOnCloseMenuEvent = false;
            }

            if (_InventoryPrefab != null)
            {
                _InventoryPrefab = Instantiate(_InventoryPrefab);
                //_InventoryPrefab.transform.SetParent(HUD.transform, false);
                //_InventoryPrefab.transform.SetAsFirstSibling();
                _InventoryManager = _InventoryPrefab.GetComponentInChildren<InventoryManager>();
                Assert.IsNotNull(_InventoryManager);
                _InventoryManager.InitializeInventoryManager(GameManager.Instance.PlayerState.Inventory);
            }
        }
        GameEventManager.instance.CloseMenu();
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

        GameEventManager.instance.OnChangeGameState -= OnGameStateChanged;
        GameEventManager.instance.OnGivePlayerItems -= OnGainItems;
        GameEventManager.instance.OnRemovePlayerItems -= OnRemoveItems;
        GameEventManager.instance.OnPostInventoryOpen -= PostInventoryOpen;
        GameEventManager.instance.OnCloseMenu -= CloseInventory_Internal;
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

            if(frontAnimator.gameObject.activeSelf == true)
            {
                frontAnimator.SetFloat("MoveSpeed", _Rigidbody.velocity.magnitude);
            }
            if(backAnimator.gameObject.activeSelf == true)
            {
                backAnimator.SetFloat("MoveSpeed", _Rigidbody.velocity.magnitude);
            }

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
            if(toolbar != null)
            {
                List<InventoryItemData> Data = new List<InventoryItemData>();
                InventoryItemData ActionItem = toolbar.GetSelectedItem();
                if (ActionItem != null)
                {
                    Data.Add(toolbar.GetSelectedItem());
                }
                _InteractorBehavoir.TryInteract(Data);

            }
            else
            {
                _InteractorBehavoir.TryInteract();
            }
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
        if (context.performed)
        {
            if (!_InventoryManager.gameObject.activeSelf)
            {
                GameEventManager.instance.CloseMenu();
                _InventoryManager.gameObject.SetActive(true);
            }
            else
            {
                OnCloseInventory(context);
            }
        }
    }

    public void PostInventoryOpen()
    {
        _PlayerActions.PlayerMovementMap.Disable();
        _PlayerActions.PlayerActionMap.Disable();
        _PlayerActions.Inventory.Enable();
    }

    protected void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.instance.CloseMenu();
        }
    }
    private void CloseInventory_Internal()
    {
        _PlayerActions.PlayerMovementMap.Enable();
        _PlayerActions.PlayerActionMap.Enable();
        _PlayerActions.Inventory.Disable();
    }

    public void OnGainItems(List<InventoryItemData> Items)
    {
        foreach(InventoryItemData item in Items)
        {
            _InventoryManager.AddItem(item);
        }
    }

    public void OnRemoveItems(List<InventoryItemData> Items)
    {
        foreach (InventoryItemData item in Items)
        {
            toolbar.ToolbarManager.RemoveItem(item);
        }
    }
}
