using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleHandVFX : MonoBehaviour
{

    public GameObject VFX;
    // Start is called before the first frame update
    void Start()
    {
        VFX.SetActive(false);
        GameEventManager.instance.OnChangeGameState += OnChangeGameState;
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnChangeGameState -= OnChangeGameState;
    }

    public void OnChangeGameState(EGameState NewState, EGameState OldState)
    {
        if(NewState == EGameState.NightState)
        {
            VFX.SetActive(true);
        }
    }
}
