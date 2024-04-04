using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class UnlockNPCInteractionPrompt : MonoBehaviour
{
    public int UnlockCost;
    [SerializeField] private TextMeshProUGUI PlayerGoldText;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private Button UnlockButton;
    EGameState PreviousState;

    public void Start()
    {
        PreviousState = GameManager.Instance.GetGameState();
        GameManager.Instance.ChangeGameState(EGameState.MovementDisabledState);
        GameEventManager.instance.OnPostPlayerGoldChanged += UpdateButtonEnable;
        GameEventManager.instance.OnCloseMenu += OnCancelled;
    }

    private void OnDisable()
    {
        GameManager.Instance.ChangeGameState(PreviousState);
        GameEventManager.instance.OnPostPlayerGoldChanged -= UpdateButtonEnable;
        GameEventManager.instance.OnCloseMenu -= OnCancelled;
    }

    public void OnCancelled()
    {
        Destroy(gameObject);
    }

    public void OnPurchased()
    {
        if (GameManager.Instance.PlayerState.Gold >= UnlockCost)
        {
            GameEventManager.instance.Purchase(UnlockCost);
            GameManager.Instance.BuyTableSpace();
            Destroy(gameObject);
        }
    }

    public void UpdateButtonEnable(long NewGoldAmount, long DeltaGold)
    {
        UnlockButton.interactable = NewGoldAmount >= UnlockCost;
        if (PlayerGoldText != null)
        {
            PlayerGoldText.text = NewGoldAmount.ToString() + "$";
        }
    }

    public void SetData(int Cost)
    {
        UnlockCost = Cost;
        if (PlayerGoldText != null)
        {
            PlayerGoldText.text = GameManager.Instance.PlayerState.Gold.ToString() + "$";
        }
        if (CostText != null)
        {
            CostText.text = "For " + UnlockCost + "$ more friends can come order everynight";
        }
    }
}
