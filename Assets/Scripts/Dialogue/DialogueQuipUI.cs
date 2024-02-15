using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueQuipUI : MonoBehaviour
{
    public GameObject QuipUIPanel;
    public TextMeshProUGUI QuipUIText;

    float LastUpdate;
    float PanelLifetime = 2.75f;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnShowDialogueQuip += OnShowDialogueQuip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnShowDialogueQuip -= OnShowDialogueQuip; 
    }

    public void OnShowDialogueQuip(DialogueData Data)
    {
        QuipUIText.SetText(Data.Dialogue);
        QuipUIPanel.SetActive(true);
        LastUpdate = Time.time;
        Invoke("CloseDialogue", PanelLifetime);
    }

    public void CloseDialogue()
    {
        if (Time.time - LastUpdate > PanelLifetime/1.5) 
        {
            QuipUIPanel.SetActive(false);
        }
    }

}
