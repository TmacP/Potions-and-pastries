using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class GateInteractionBehavoir : MonoBehaviour, IInteractable
{
    public GameObject PurchasePrompt;
    public EGameRegion BlockedRegion = EGameRegion.None;
    public int OpenCost = 10;
    public int ID = -1;
    public bool fence_rotation;


    public bool bGateOpen;
    [SerializeField] private GameObject GateComponent; //This is the component we should rotate to open the gate

    // Singleton instance of BasicInkExample
    private static BasicInkExample _inkInstance;
    public static BasicInkExample InkInstance
    {
        get
        {
            if (_inkInstance == null)
                _inkInstance = FindObjectOfType<BasicInkExample>();
            return _inkInstance;
        }
    }

//************ Start of IINteractable Interface***********
    public string InteractionPrompt => "Unlock " + BlockedRegion.ToString();

    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null)
    {
        if(PurchasePrompt != null)
        {
            GameObject Go = Instantiate(PurchasePrompt, this.gameObject.transform);
            UnlockAreaInteractionPrompt Prompt = Go.GetComponent<UnlockAreaInteractionPrompt>();
            if(Prompt != null)
            {
                Prompt.SetData(BlockedRegion, OpenCost, ID);
            }
            return EInteractionResult.Success;
        }
        return EInteractionResult.Failure;
    }

//************* END of Interface****************


    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnDoorUnlocked += OnGateUnlocked;


        SetGateState(GameManager.Instance.PersistantGameState.OpenedDoors.Contains(ID));
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnDoorUnlocked -= OnGateUnlocked;
    }

    public void  OnGateUnlocked(int GateID)
    {
        if(ID == GateID)
        {
            SetGateState(true);
            SFX.PlayGateOpen(); // FMOD sound effect
            // set our inkle gate var to open
            if (InkInstance != null){
            InkInstance.OpenGate(ID);
            }
        }
    }

    public void SetGateState(bool Open)
    {
        bGateOpen = Open;
        if (bGateOpen)
        {
            if(gameObject != null)
            {
                int LayerIndex = LayerMask.NameToLayer("Interact");
                this.transform.gameObject.layer &= (0x1 << LayerIndex);
            }
            if (GateComponent != null && !fence_rotation)
            {
                GateComponent.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (GateComponent != null && fence_rotation)
            {
                GateComponent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            if (GateComponent != null && !fence_rotation)
            {
                GateComponent.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (GateComponent != null && fence_rotation)
            {
                GateComponent.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }
}
