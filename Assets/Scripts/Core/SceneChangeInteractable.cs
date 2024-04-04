using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string Prompt;
    [SerializeField] private EGameScene NewLocationScene;
    public GameObject transitionObject;
    //**************** IInteractable
    public string InteractionPrompt => Prompt;

    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {
        if (transitionObject == null)
        {
            SFX.PlayDoorOpen();
            GameManager.Instance.ChangeGameScene(NewLocationScene);
            if (NewLocationScene == EGameScene.InnInterior) // if we are going to the alpha interior, we increment the day
            {
                GameManager.Instance.GameDay++;
            }
            return EInteractionResult.Success;
        }
        else
        {
            StartCoroutine(animateTransition());
            return EInteractionResult.Success;
        }
        
    }
    //********* End of IInteractable

    IEnumerator animateTransition()
    {
        transitionObject.GetComponent<TransitionController>().ExitSceneTransition();
        SFX.PlayDoorOpen();
        yield return new WaitForSeconds(0.5f);
        if (NewLocationScene == EGameScene.InnInterior) // if we are going to the alpha interior, we increment the day
        {
            GameManager.Instance.GameDay++;
        }
        GameManager.Instance.ChangeGameScene(NewLocationScene);
    }

}
