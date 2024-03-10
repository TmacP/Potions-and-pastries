using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class GateInteractionBehavoir : MonoBehaviour, IInteractable
{
    public GameObject PurchasePrompt;
    public EGameRegion BlockedRegion = EGameRegion.None;
    public int OpenCost = 10;
    public int ID = -1;
    public bool fence_rotation;


    public bool bGateOpen;
    [SerializeField] private GameObject GateComponent; //This is the component we should rotate to open the gate

//************ Start of IINteractable Interface***********
    public string InteractionPrompt => "Unlock " + BlockedRegion.ToString();

    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null)
    {
        if(PurchasePrompt != null)
        {
            GameObject Go = Instantiate(PurchasePrompt, this.gameObject.transform);
            UnlockAreaInteractionPrompt Prompt = Go.GetComponent<UnlockAreaInteractionPrompt>();
            if(Prompt != null)
            {
                Prompt.SetData(BlockedRegion, OpenCost, ID);
            }
            return true;
        }
        return false;
    }

//************* END of Interface****************


    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnDoorUnlocked += OnGateUnlocked;
    }

    public void  OnGateUnlocked(int GateID)
    {
        if(ID == GateID)
        {
            SetGateState(true);
        }
    }

    public void SetGateState(bool Open)
    {
        bGateOpen = Open;
        if (bGateOpen)
        {
            
            int LayerIndex = LayerMask.NameToLayer("Interact");
            this.gameObject.layer &= (0x1 << LayerIndex);
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
