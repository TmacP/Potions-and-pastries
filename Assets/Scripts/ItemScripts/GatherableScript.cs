using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

public class GatherableBehavoir : MonoBehaviour, IInteractable
{
    [SerializeField] private GatherableData Data;

//********IInteractable Interface **********
    public string InteractionPrompt => "Gather";

    public bool TryInteract(InteractorBehavoir InInteractor)
    {

        if (Data.MiniGame != null)
        {
            GameEventManager.instance.OnMiniGameComplete += OnMiniGameComplete;
            //Start MiniGame
            GameManager.Instance.ChangeGameState(EGameState.MovementDisabledState);

            Data.MiniGame.Instantiate(Vector3.zero, Quaternion.identity);
            return true;
        }

        return false;
    }
//************ End of IInteractable ************

    public void OnMiniGameComplete(EMiniGameCompleteResult Result)
    {
        if(Result == EMiniGameCompleteResult.CriticalSuccess)
        {
            Debug.Log("Gained 2x Item");
            //GameManager.Instance.GainItem();
            Destroy(this.gameObject);
        }
        else if(Result == EMiniGameCompleteResult.Success)
        {
            //GameManager.Instance.GainItem();
            Destroy(this.gameObject);
        }
        GameManager.Instance.ChangeGameState(EGameState.MainState);
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }

    void OnDisable()
    {
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }
}
