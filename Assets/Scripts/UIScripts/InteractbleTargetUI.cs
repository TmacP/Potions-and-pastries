using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractbleTargetUI : MonoBehaviour
{
    [SerializeField] private TMP_Text PromptText;
    [SerializeField] private TMP_Text SecondaryPromptText;
    [SerializeField] private GameObject PromptUI;
    [SerializeField] private GameObject SecondaryPromptUI;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnChangeInteractionTarget += OnChangeInteractionTarget;
        PromptText.text = "";
        SecondaryPromptText.text = "";
        PromptUI.SetActive(false);
        SecondaryPromptUI.SetActive(false);
    }

    private void OnEnable()
    {
        if(GameEventManager.instance)
        {
            GameEventManager.instance.OnChangeInteractionTarget += OnChangeInteractionTarget;
        }
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
                PromptText.text = "E: " + Interactable.InteractionPrompt;

                IInteractableExtension Extension = Interactable as IInteractableExtension;
                if(Extension != null)
                {
                    SecondaryPromptText.text = "Q: " + Extension.GetSecondaryInteractionPrompt();
                    SecondaryPromptUI.SetActive(true);
                }
                else
                {
                    SecondaryPromptText.text = "";
                    SecondaryPromptUI.SetActive(false);
                }

            }
            else
            {
                PromptUI.SetActive(false);
                PromptText.text = "";
                SecondaryPromptText.text = "";
                SecondaryPromptUI.SetActive(false);
            }
        }
    }
}
