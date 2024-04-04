using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNPCInteraction : MonoBehaviour, IInteractable
{
    public GameObject PurchasePrompt;
    public int Cost = 10;

    public string InteractionPrompt => "Purchase Table Space";

    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null)
    {
        if (PurchasePrompt != null)
        {
            GameObject Go = Instantiate(PurchasePrompt, this.gameObject.transform);
            UnlockNPCInteractionPrompt prompt = Go.GetComponent<UnlockNPCInteractionPrompt>();
            if(prompt != null)
            {
                prompt.SetData(Cost);
            }
            return EInteractionResult.Success;
        }
        return EInteractionResult.Failure;
    }
}
