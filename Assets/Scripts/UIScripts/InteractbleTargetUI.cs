using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractbleTargetUI : MonoBehaviour
{
    [SerializeField] private TMP_Text PromptText;
    [SerializeField] private GameObject PromptUI;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnChangeInteractionTarget += OnChangeInteractionTarget;
        PromptText.text = "";
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnChangeInteractionTarget -= OnChangeInteractionTarget;
    }

    void OnChangeInteractionTarget(IInteractable Interactable, InteractorBehavoir Interactor)
    {
        if(PromptText != null)
        {
            if(Interactable != null)
            {
                PromptUI.SetActive(true);
                PromptText.text = Interactable.InteractionPrompt;
            }
            else
            {
                PromptUI.SetActive(false);
                PromptText.text = "";
            }
        }
    }
}
