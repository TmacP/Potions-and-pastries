using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ChangingRoomInteraction : MonoBehaviour
{
    public GameObject VFX;

    public void Start()
    {
        GameEventManager.instance.OnChangeGameState += OnChangeGameState;
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnChangeGameState -= OnChangeGameState;
    }

    void OnChangeGameState(EGameState NewState, EGameState OldState)
    {
        if (NewState == EGameState.NightState)
        {
            int LayerIndex = LayerMask.NameToLayer("Interact");
            this.gameObject.layer &= (0x1 << LayerIndex);

            if(VFX != null)
            {
                Destroy(VFX.gameObject);
            }
        }
    }
}
