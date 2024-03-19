using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PauseScript pauseScript; // Reference to PauseScript

    public PlayerActions _PlayerActions;
    private Rigidbody _Rigidbody;
    private InteractorBehavoir _InteractorBehavoir;

    //private InputAction _menuOpenCloseAction;
    //public bool MenuOpenCloseInput { get; private set;}

    [SerializeField] private GameObject _InventoryPrefab;
    [SerializeField, HideInInspector] public InventoryManager _InventoryManager;
    
    [SerializeField] private GameObject _DeckBuildingScreenPrefab;

    
    [SerializeField] private GameObject _PauseMenuPrefab;
   

    [SerializeField] private GameObject _AllMenuFromPause;


    [SerializeField] private GameObject _HotBarPrefab;
    [SerializeField] private GameObject _CardHandPrefab;
    public Toolbar toolbar;

    public Animator frontAnimator;
    public Animator backAnimator;
    private bool faceBack = false;
    private bool faceLeft = true;

    

    //if we cannot find a gamemanager and playerstate use this speed instead.
    //This is so players don't break on levels without a gamemanager
    private readonly float _fallbackSpeed = 20.0f;
    private readonly float _downwardForce = 20.0f;


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
        _PlayerActions.Menu.Enable();
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
        _PlayerActions.PlayerActionMap.SecondaryInteract.performed += OnSecondaryInteract;
        _PlayerActions.PlayerActionMap.OpenInventory.performed += OnOpenInventory;
        _PlayerActions.Inventory.CloseInventory.performed += OnCloseInventory;
        _PlayerActions.PlayerActionMap.OpenDeckBuildingScreen.performed += OnOpenDeckBuildingScreen;
        //_PlayerActions.PlayerActionMap.MenuOpenClose.performed += OnMenuOpen;
        //_menuOpenCloseACtion = _PlayerActions.PlayerActionMap.MenuOpenClose;
        //_menuOpenCloseAction = _PlayerActions.PlayerActionMap.MenuOpenClose;
        _PlayerActions.Menu.MenuOpenClose.performed += OnPauseMenuOpen;
        
    }

    public void Start()
    {
        GameEventManager.instance.OnChangeGameState += OnGameStateChanged;
        GameEventManager.instance.OnGivePlayerItems += OnGainItems;
        GameEventManager.instance.OnRemovePlayerItems += OnRemoveItems;
        GameEventManager.instance.OnPostInventoryOpen += PostInventoryOpen;
        GameEventManager.instance.OnCloseMenu += CloseInventory_Internal;
        GameEventManager.instance.OnPurchase += OnPurchase;
        

        GameObject[] GOS = GameObject.FindGameObjectsWithTag("PlayerHUD");
        
        if(GOS.Length > 0)
        {
            GameObject HUD = GOS[0];

            InstantiateToolbar();

            if (_InventoryPrefab != null)
            {
                _InventoryPrefab = Instantiate(_InventoryPrefab);
                _InventoryPrefab.transform.SetParent(HUD.transform, false);
                _InventoryPrefab.transform.SetAsFirstSibling();
                _InventoryManager = _InventoryPrefab.GetComponentInChildren<InventoryManager>();
                Assert.IsNotNull(_InventoryManager);
                _InventoryManager.InitializeInventoryManager(GameManager.Instance.PlayerState.Inventory);
            }
        }
        GameEventManager.instance.CloseMenu();
    }
    private void Update()
    {
        //MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }

    public void OnDisable()
    {
        _PlayerActions.PlayerActionMap.Interact.started -= OnInteractStart;
        _PlayerActions.PlayerActionMap.Interact.canceled -= OnInteractStart;
        _PlayerActions.PlayerActionMap.OpenInventory.performed -= OnOpenInventory;
        _PlayerActions.Inventory.CloseInventory.performed -= OnCloseInventory;
        _PlayerActions.PlayerActionMap.OpenDeckBuildingScreen.performed -= OnOpenDeckBuildingScreen;
        _PlayerActions.Menu.MenuOpenClose.performed -= OnPauseMenuOpen;

        GameEventManager.instance.OnChangeGameState -= OnGameStateChanged;
        GameEventManager.instance.OnGivePlayerItems -= OnGainItems;
        GameEventManager.instance.OnRemovePlayerItems -= OnRemoveItems;
        GameEventManager.instance.OnPostInventoryOpen -= PostInventoryOpen;
        GameEventManager.instance.OnCloseMenu -= CloseInventory_Internal;
        GameEventManager.instance.OnClosePauseMenu -= ClosePauseMenu;
        GameEventManager.instance.OnPurchase -= OnPurchase;
    }

    protected void OnGameStateChanged(EGameState NewGameState, EGameState OldGameState)
    {
        switch (NewGameState)
        {
            case EGameState.MainState:
                _PlayerActions.PlayerActionMap.Enable();
                _PlayerActions.PlayerMovementMap.Enable();
                break;
            case EGameState.NightState:
                _PlayerActions.PlayerActionMap.Enable();
                _PlayerActions.PlayerMovementMap.Enable();
                InstantiateToolbar(true);
                break;
            case EGameState.PauseState:
                _PlayerActions.PlayerActionMap.Disable();
                _PlayerActions.PlayerMovementMap.Disable();
                break;
            case EGameState.MovementDisabledState:
                _PlayerActions.PlayerActionMap.Enable();
                _PlayerActions.PlayerMovementMap.Disable();
                break;
            case EGameState.QuitState:
                Application.Quit();
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
        float Gravity = _downwardForce;
        if(GameManager.Instance && GameManager.Instance.PlayerState)
        {
            Speed = GameManager.Instance.PlayerState.MoveSpeed;
            Gravity = GameManager.Instance.PlayerState.Gravity;
        }

        Vector2 _PlayerMoveInput = _PlayerActions.PlayerMovementMap.Move.ReadValue<Vector2>();
        _Rigidbody.velocity = new Vector3(
            _PlayerMoveInput.x * Speed,
            _Rigidbody.velocity.y,
            _PlayerMoveInput.y * Speed);

        // Add downward force
        _Rigidbody.AddForce(Vector3.down * _downwardForce, ForceMode.Acceleration);

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
            //transform.Find("F_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            //transform.Find("B_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

            Vector3 scale = transform.Find("F_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("F_BaseCharacter").transform.localScale = scale;
            scale = transform.Find("B_BaseCharacter").transform.localScale ;
            scale.x *= -1.0f;
            transform.Find("B_BaseCharacter").transform.localScale = scale;
            faceLeft = false;
        }
        else if (_PlayerMoveInput.x < 0 && !faceLeft)
        {
            //transform.Find("F_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            //transform.Find("B_BaseCharacter").transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

            Vector3 scale = transform.Find("F_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("F_BaseCharacter").transform.localScale = scale;
            scale = transform.Find("B_BaseCharacter").transform.localScale;
            scale.x *= -1.0f;
            transform.Find("B_BaseCharacter").transform.localScale = scale;


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
                EInteractionResult Result = _InteractorBehavoir.TryInteract(Data);
                if (Result == EInteractionResult.Success_ConsumeItem)
                {
                    for (int i = Data.Count - 1; i >= 0; i--)
                    {
                        toolbar.UseSelectedItem();
                    }
                }
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

    protected void OnSecondaryInteract(InputAction.CallbackContext context)
    {
        if (_InteractorBehavoir != null)
        {
            if (toolbar != null)
            {
                List<InventoryItemData> Data = new List<InventoryItemData>();
                InventoryItemData ActionItem = toolbar.GetSelectedItem();
                if (ActionItem != null)
                {
                    Data.Add(toolbar.GetSelectedItem());
                }
                EInteractionResult Result = _InteractorBehavoir.TrySecondaryInteract(Data);
                if (Result == EInteractionResult.Success_ConsumeItem)
                {
                    for (int i = Data.Count - 1; i >= 0; i--)
                    {
                        toolbar.UseSelectedItem();
                    }
                }
            }
            else
            {
                _InteractorBehavoir.TryInteract();
            }
        }
    }

    protected void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_InventoryManager != null && !_InventoryManager.gameObject.activeSelf && GameManager.Instance.GetGameState() != EGameState.NightState)
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


    protected void OnPauseMenuOpen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_PauseMenuPrefab.gameObject.activeSelf)
            {
                
                _PauseMenuPrefab.gameObject.SetActive(true);
                
                pauseScript.showMenu();
                
                OnGameStateChanged(EGameState.PauseState, EGameState.MainState);
                
                


            }
            else
            {
                
                OnPauseMenuClose();
            }
        }
    }

    public void OnPauseMenuClose()
    {

        
        OnGameStateChanged(EGameState.MainState, EGameState.PauseState);
        
        GameEventManager.instance.ClosePauseMenu();
        _PauseMenuPrefab.gameObject.SetActive(false);
        pauseScript.Close();
      
        //EventSystem.current.SetSelectedGameObject(null);
        /*
        foreach (Transform child in _PauseMenuPrefab.transform)
        {
            child.gameObject.SetActive(false);
        }*/
        
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

    
    private void ClosePauseMenu()
    {
        _PlayerActions.PlayerMovementMap.Enable();
        _PlayerActions.PlayerActionMap.Enable();
        _PauseMenuPrefab.gameObject.SetActive(false);
        // what to do here??
    }



    public void OnGainItems(List<InventoryItemData> Items)
    {
        if(GameManager.Instance.GetGameState() == EGameState.NightState && toolbar != null)
        {
            foreach(InventoryItemData item in Items)
            {
                if(toolbar.IsFull())
                {
                    DeckManager Deck = GetComponent<DeckManager>();
                    if(Deck != null)
                    {
                        Deck.AddCardToDiscard(item);
                    }
                }
                else
                {
                    toolbar.ToolbarManager.AddItem(item);
                }
            }
        }
        else
        {
            foreach (InventoryItemData item in Items)
            {
                _InventoryManager.AddItem(item);
            }
        }
    }

    public void OnRemoveItems(List<InventoryItemData> Items)
    {
        foreach (InventoryItemData item in Items)
        {
            toolbar.ToolbarManager.RemoveItem(item);
        }
    }

    public void KillGame()
    {
        OnGameStateChanged(EGameState.QuitState, EGameState.MainState);
    }
    public void OnPurchase(int Cost)
    {
        //Kinda convoluted but safe
        long CurrentGold = GameManager.Instance.PlayerState.Gold;
        CurrentGold -= Cost;
        Assert.IsTrue(CurrentGold > 0);
        CurrentGold = CurrentGold > 0 ? CurrentGold : 0;
        long DeltaGold = GameManager.Instance.PlayerState.Gold - CurrentGold;
        GameManager.Instance.PlayerState.Gold = CurrentGold;
        GameEventManager.instance.PostPlayerGoldChanged(CurrentGold, DeltaGold);
    }

    public void OnOpenDeckBuildingScreen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_DeckBuildingScreenPrefab != null && GameManager.Instance.GetGameState() != EGameState.NightState)
            {
                GameEventManager.instance.CloseMenu();
                Instantiate(_DeckBuildingScreenPrefab);
            }
            else
            {
                OnCloseInventory(context);
            }
        }
    }

    private void InstantiateToolbar(bool CardHand = false)
    {
        if(toolbar != null)
        {
            Destroy(toolbar.gameObject);
        }

        GameObject[] GOS = GameObject.FindGameObjectsWithTag("PlayerHUD");
        if (GOS.Length > 0)
        {
            GameObject HUD = GOS[0];

            if(CardHand)
            {
                GameObject Toolbar = Instantiate(_CardHandPrefab);
                Toolbar.transform.SetParent(HUD.transform, false);
                Toolbar.transform.SetAsFirstSibling();

                InventoryManager TBManager = Toolbar.GetComponent<InventoryManager>();
                if (TBManager != null)
                {
                    TBManager.InitializeInventoryManager(GameManager.Instance.PlayerState.CardHand);
                    TBManager.CloseOnCloseMenuEvent = false;
                }
                toolbar = Toolbar.GetComponent<Toolbar>();
            }
            else if(_HotBarPrefab != null)
            {

                GameObject Toolbar = Instantiate(_HotBarPrefab);
                Toolbar.transform.SetParent(HUD.transform, false);
                Toolbar.transform.SetAsFirstSibling();

                InventoryManager TBManager = Toolbar.GetComponent<InventoryManager>();
                if (TBManager != null)
                {
                    TBManager.InitializeInventoryManager(GameManager.Instance.PlayerState.ToolBar);
                    TBManager.CloseOnCloseMenuEvent = false;
                }
                toolbar = Toolbar.GetComponent<Toolbar>();
            }
            
        }


        
    }
}
