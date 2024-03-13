using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string Prompt;
    [SerializeField] private EGameScene NewLocationScene;
//**************** IInteractable
    public string InteractionPrompt => Prompt;

    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {
        GameManager.Instance.ChangeGameScene(NewLocationScene);
        return true;
    }
//********* End of IInteractable
}
