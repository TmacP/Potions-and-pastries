using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class DialogueBehavoir : MonoBehaviour
{
    bool DebugMode = false;

    [SerializeField] public NPCDialogueState State;
    [SerializeField] public GameObject DialogueUI;
    [SerializeField] public TextMeshProUGUI DialogueText;
    [SerializeField] public float DialogueDisplayTime = 3.0f;
    [SerializeField] public int DialogueDisplayCounter = 0;

    // Reference to NPCData instance
    public NPCCharacterData npcData;

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

    

    public void TryDialogue()
    {
        if (npcData != null && ((GameManager.Instance.GetGameScene() == EGameScene.InnExterior) || (GameManager.Instance.GetGameScene() == EGameScene.Tutorial)))
        {
            if (DebugMode) { Debug.Log("TryDialogue, npcData: " + npcData); }
            InkInstance.ContinueStory(npcData); // Continue the story when talking to NPC
        }
        
        DialogueData Dialogue = DialogueManager.instance.GetDialogue(State);
        if(Dialogue != null)
        {
            GameEventManager.instance.ShowDialogueQuip(Dialogue);
            if (DialogueUI != null && DialogueText != null)
            {
                DialogueUI.SetActive(true);
                DialogueText.SetText(Dialogue.Dialogue);
                DialogueDisplayCounter++;
                Invoke("EndQuipDialogue", DialogueDisplayTime);
            }
            else
            {
                //Debug.Log(Dialogue.Dialogue);
                //Debug.Log("Null Dialogue UI Elements");
            }
        }
        else
        {
            Debug.Log("Null Dialogue Return");
        }
    }

    public void EndQuipDialogue()
    {
        DialogueDisplayCounter--;
        if(DialogueDisplayCounter <= 0)
        {
            DialogueUI.SetActive(false);
        }
    }
}
