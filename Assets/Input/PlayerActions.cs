//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""PlayerActionMap"",
            ""id"": ""a083a6f3-2b02-419a-9d0b-465d3ea7a97c"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""f6adfaa6-fb78-49d4-8604-88f396bf4b4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1a404b37-db89-4807-81d9-264731527993"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryInteract"",
                    ""type"": ""Button"",
                    ""id"": ""46dc5bff-5b38-4a84-a595-6ee1f5810d3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""9f6d8198-354b-4d58-b082-8497f99ea537"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToolbarScroll"",
                    ""type"": ""Value"",
                    ""id"": ""5b9e5538-b27b-40d4-8bee-6d564565df7c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MenuOpenClose"",
                    ""type"": ""Button"",
                    ""id"": ""e3fd73d6-5318-4040-9064-57237868d7e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenRecipeBook"",
                    ""type"": ""Button"",
                    ""id"": ""0f0c13a8-81e0-474d-90eb-22bcb89e4643"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7a47c6f0-3ee6-43fa-b357-2f2042bc3b0b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7801a10-bd42-4577-9141-f3549f6843e6"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3547e65b-af9c-4e78-b461-324d54a581d8"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c071d82-e16d-44c8-92ea-a9a14409e57c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dde9a46-a70c-4be0-a6a8-c617e2b4fcda"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f45db8e9-460b-4c73-9d31-8069de9c0e3d"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""811c064b-b4b9-455d-9d86-57cdab1f6dbe"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""596a2f54-d2e0-4dcb-b97a-d201c6a46869"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2df4138a-3957-4a41-a39b-080e9266a355"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8d7455a-79e9-4973-8735-56a590da94f8"",
                    ""path"": ""<DualShockGamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d31f479-be08-4688-ba07-36f6e8a9e337"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a49629e2-8e95-4192-b7bc-e840d47e17c9"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToolbarScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""896450d9-f40d-40dc-bcc9-818d4677088c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MenuOpenClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55511209-0d22-4b9d-8754-b31c66ba686b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuOpenClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34087ba2-3587-49bf-a097-a634f0087ae9"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenRecipeBook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerMovementMap"",
            ""id"": ""03d1c749-b705-4f8a-8eae-2ccba45d4d9b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c10ece80-8e2f-420c-8c01-89f751b027d3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9a1a5909-41b0-4991-a84d-553e45c03c16"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""860a80c6-205e-411a-9759-8529544e1cc1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""180e20ac-0305-4033-a6c9-04966b5e04ba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f307f421-dbdd-4828-86df-29cd780af5b3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b2d721a9-b3c0-4495-afc4-43fd46dec491"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d618c871-78ef-4dbd-98c9-2ef1c3d8c465"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""8ef39b34-bd84-4d4f-8929-46121ec69583"",
            ""actions"": [
                {
                    ""name"": ""CloseInventory"",
                    ""type"": ""Button"",
                    ""id"": ""1b2c8794-46e6-4771-b5d3-24872ecc84c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ControllerMove"",
                    ""type"": ""Button"",
                    ""id"": ""30d4848c-b7da-4354-bfcb-76e786040c67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1430924e-9459-44f6-a1ce-ac0b4b049b93"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aebadc50-4624-4cba-9ca3-d332df7f813d"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""872927a8-5999-4c24-950e-04720fe02d7b"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62e43950-3141-46a2-a71d-759f7b757fbc"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec453779-f8b0-4987-a524-ee11a7e4b569"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7be89bc1-0134-462f-bd0f-ad51816ee640"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e6213c1-e065-4b1b-b275-3fb0ba33fdbb"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControllerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""32721adb-5a82-4a7c-9b73-7bb1b02f5d89"",
            ""actions"": [
                {
                    ""name"": ""MenuOpenClose"",
                    ""type"": ""Button"",
                    ""id"": ""cbd4288b-1984-40b6-9895-67e8bb44d53a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ea25166e-4b09-4fda-8b98-56726db093ba"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MenuOpenClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98576b12-197f-487d-8abd-ecef4035dd3e"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuOpenClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerActionMap
        m_PlayerActionMap = asset.FindActionMap("PlayerActionMap", throwIfNotFound: true);
        m_PlayerActionMap_Interact = m_PlayerActionMap.FindAction("Interact", throwIfNotFound: true);
        m_PlayerActionMap_Jump = m_PlayerActionMap.FindAction("Jump", throwIfNotFound: true);
        m_PlayerActionMap_SecondaryInteract = m_PlayerActionMap.FindAction("SecondaryInteract", throwIfNotFound: true);
        m_PlayerActionMap_OpenInventory = m_PlayerActionMap.FindAction("OpenInventory", throwIfNotFound: true);
        m_PlayerActionMap_ToolbarScroll = m_PlayerActionMap.FindAction("ToolbarScroll", throwIfNotFound: true);
        m_PlayerActionMap_MenuOpenClose = m_PlayerActionMap.FindAction("MenuOpenClose", throwIfNotFound: true);
        m_PlayerActionMap_OpenRecipeBook = m_PlayerActionMap.FindAction("OpenRecipeBook", throwIfNotFound: true);
        // PlayerMovementMap
        m_PlayerMovementMap = asset.FindActionMap("PlayerMovementMap", throwIfNotFound: true);
        m_PlayerMovementMap_Move = m_PlayerMovementMap.FindAction("Move", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_CloseInventory = m_Inventory.FindAction("CloseInventory", throwIfNotFound: true);
        m_Inventory_ControllerMove = m_Inventory.FindAction("ControllerMove", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MenuOpenClose = m_Menu.FindAction("MenuOpenClose", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerActionMap
    private readonly InputActionMap m_PlayerActionMap;
    private List<IPlayerActionMapActions> m_PlayerActionMapActionsCallbackInterfaces = new List<IPlayerActionMapActions>();
    private readonly InputAction m_PlayerActionMap_Interact;
    private readonly InputAction m_PlayerActionMap_Jump;
    private readonly InputAction m_PlayerActionMap_SecondaryInteract;
    private readonly InputAction m_PlayerActionMap_OpenInventory;
    private readonly InputAction m_PlayerActionMap_ToolbarScroll;
    private readonly InputAction m_PlayerActionMap_MenuOpenClose;
    private readonly InputAction m_PlayerActionMap_OpenRecipeBook;
    public struct PlayerActionMapActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerActionMapActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_PlayerActionMap_Interact;
        public InputAction @Jump => m_Wrapper.m_PlayerActionMap_Jump;
        public InputAction @SecondaryInteract => m_Wrapper.m_PlayerActionMap_SecondaryInteract;
        public InputAction @OpenInventory => m_Wrapper.m_PlayerActionMap_OpenInventory;
        public InputAction @ToolbarScroll => m_Wrapper.m_PlayerActionMap_ToolbarScroll;
        public InputAction @MenuOpenClose => m_Wrapper.m_PlayerActionMap_MenuOpenClose;
        public InputAction @OpenRecipeBook => m_Wrapper.m_PlayerActionMap_OpenRecipeBook;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Add(instance);
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @SecondaryInteract.started += instance.OnSecondaryInteract;
            @SecondaryInteract.performed += instance.OnSecondaryInteract;
            @SecondaryInteract.canceled += instance.OnSecondaryInteract;
            @OpenInventory.started += instance.OnOpenInventory;
            @OpenInventory.performed += instance.OnOpenInventory;
            @OpenInventory.canceled += instance.OnOpenInventory;
            @ToolbarScroll.started += instance.OnToolbarScroll;
            @ToolbarScroll.performed += instance.OnToolbarScroll;
            @ToolbarScroll.canceled += instance.OnToolbarScroll;
            @MenuOpenClose.started += instance.OnMenuOpenClose;
            @MenuOpenClose.performed += instance.OnMenuOpenClose;
            @MenuOpenClose.canceled += instance.OnMenuOpenClose;
            @OpenRecipeBook.started += instance.OnOpenRecipeBook;
            @OpenRecipeBook.performed += instance.OnOpenRecipeBook;
            @OpenRecipeBook.canceled += instance.OnOpenRecipeBook;
        }

        private void UnregisterCallbacks(IPlayerActionMapActions instance)
        {
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @SecondaryInteract.started -= instance.OnSecondaryInteract;
            @SecondaryInteract.performed -= instance.OnSecondaryInteract;
            @SecondaryInteract.canceled -= instance.OnSecondaryInteract;
            @OpenInventory.started -= instance.OnOpenInventory;
            @OpenInventory.performed -= instance.OnOpenInventory;
            @OpenInventory.canceled -= instance.OnOpenInventory;
            @ToolbarScroll.started -= instance.OnToolbarScroll;
            @ToolbarScroll.performed -= instance.OnToolbarScroll;
            @ToolbarScroll.canceled -= instance.OnToolbarScroll;
            @MenuOpenClose.started -= instance.OnMenuOpenClose;
            @MenuOpenClose.performed -= instance.OnMenuOpenClose;
            @MenuOpenClose.canceled -= instance.OnMenuOpenClose;
            @OpenRecipeBook.started -= instance.OnOpenRecipeBook;
            @OpenRecipeBook.performed -= instance.OnOpenRecipeBook;
            @OpenRecipeBook.canceled -= instance.OnOpenRecipeBook;
        }

        public void RemoveCallbacks(IPlayerActionMapActions instance)
        {
            if (m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActionMapActions @PlayerActionMap => new PlayerActionMapActions(this);

    // PlayerMovementMap
    private readonly InputActionMap m_PlayerMovementMap;
    private List<IPlayerMovementMapActions> m_PlayerMovementMapActionsCallbackInterfaces = new List<IPlayerMovementMapActions>();
    private readonly InputAction m_PlayerMovementMap_Move;
    public struct PlayerMovementMapActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerMovementMapActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovementMap_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovementMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementMapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovementMapActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovementMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovementMapActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IPlayerMovementMapActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IPlayerMovementMapActions instance)
        {
            if (m_Wrapper.m_PlayerMovementMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovementMapActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovementMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovementMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovementMapActions @PlayerMovementMap => new PlayerMovementMapActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private List<IInventoryActions> m_InventoryActionsCallbackInterfaces = new List<IInventoryActions>();
    private readonly InputAction m_Inventory_CloseInventory;
    private readonly InputAction m_Inventory_ControllerMove;
    public struct InventoryActions
    {
        private @PlayerActions m_Wrapper;
        public InventoryActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CloseInventory => m_Wrapper.m_Inventory_CloseInventory;
        public InputAction @ControllerMove => m_Wrapper.m_Inventory_ControllerMove;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void AddCallbacks(IInventoryActions instance)
        {
            if (instance == null || m_Wrapper.m_InventoryActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Add(instance);
            @CloseInventory.started += instance.OnCloseInventory;
            @CloseInventory.performed += instance.OnCloseInventory;
            @CloseInventory.canceled += instance.OnCloseInventory;
            @ControllerMove.started += instance.OnControllerMove;
            @ControllerMove.performed += instance.OnControllerMove;
            @ControllerMove.canceled += instance.OnControllerMove;
        }

        private void UnregisterCallbacks(IInventoryActions instance)
        {
            @CloseInventory.started -= instance.OnCloseInventory;
            @CloseInventory.performed -= instance.OnCloseInventory;
            @CloseInventory.canceled -= instance.OnCloseInventory;
            @ControllerMove.started -= instance.OnControllerMove;
            @ControllerMove.performed -= instance.OnControllerMove;
            @ControllerMove.canceled -= instance.OnControllerMove;
        }

        public void RemoveCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInventoryActions instance)
        {
            foreach (var item in m_Wrapper.m_InventoryActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_MenuOpenClose;
    public struct MenuActions
    {
        private @PlayerActions m_Wrapper;
        public MenuActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuOpenClose => m_Wrapper.m_Menu_MenuOpenClose;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @MenuOpenClose.started += instance.OnMenuOpenClose;
            @MenuOpenClose.performed += instance.OnMenuOpenClose;
            @MenuOpenClose.canceled += instance.OnMenuOpenClose;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @MenuOpenClose.started -= instance.OnMenuOpenClose;
            @MenuOpenClose.performed -= instance.OnMenuOpenClose;
            @MenuOpenClose.canceled -= instance.OnMenuOpenClose;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActionMapActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSecondaryInteract(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnToolbarScroll(InputAction.CallbackContext context);
        void OnMenuOpenClose(InputAction.CallbackContext context);
        void OnOpenRecipeBook(InputAction.CallbackContext context);
    }
    public interface IPlayerMovementMapActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnCloseInventory(InputAction.CallbackContext context);
        void OnControllerMove(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMenuOpenClose(InputAction.CallbackContext context);
    }
}
