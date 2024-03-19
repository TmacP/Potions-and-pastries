using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck_ToolbarUI : MonoBehaviour
{

    public TextMeshProUGUI DeckCount;
    public bool bUseDiscard = false;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnDeckSizeChange += OnDeckSizeChange;
        OnDeckSizeChange();
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnDeckSizeChange -= OnDeckSizeChange;
    }

    void OnDeckSizeChange()
    {
        if (bUseDiscard)
        {
            if (DeckCount != null)
            {
                DeckCount.text = GameManager.Instance.PlayerState.Discard.Count.ToString();
            }
        }
        else
        {
            if (DeckCount != null)
            {
                DeckCount.text = GameManager.Instance.PlayerState.Deck.Count.ToString();
            }
        }
    }

    void OnChangeGameScene(EGameState Newstate, EGameState oldState)
    {
        if(Newstate == EGameState.NightState && oldState == EGameState.MainState)
        {
            this.gameObject.SetActive(true);
        }
        if(Newstate == EGameState.MainState && oldState == EGameState.NightState)
        {
            this.gameObject.SetActive(false);
        }
    }
}
