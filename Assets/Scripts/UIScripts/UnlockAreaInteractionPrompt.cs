using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockAreaInteractionPrompt : MonoBehaviour
{
    public EGameRegion UnlockRegion;
    public int UnlockCost;
    public int DoorId;

    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI PlayerGoldText;
    [SerializeField] private Button UnlockButton;

    private void Awake()
    {
        
    }

    public void Start()
    {
        GameManager.Instance.ChangeGameState(EGameState.MovementDisabledState);
        GameEventManager.instance.OnPostPlayerGoldChanged += UpdateButtonEnable;
        GameEventManager.instance.OnCloseMenu += OnCancelled;
    }

    public void SetData(EGameRegion region, int Cost, int InDoorID)
    {
        UnlockRegion = region;
        UnlockCost = Cost;
        DoorId = InDoorID;
        string RegionName = UnlockRegion.ToString();

        if (TitleText != null)
        {
            TitleText.text = "Unlock " + RegionName;
        }
        if (DescriptionText != null)
        {
            DescriptionText.text = "Unlock " + RegionName + " for " + UnlockCost + " Gold";
        }
        if (PlayerGoldText != null)
        {
            PlayerGoldText.text = GameManager.Instance.PlayerState.Gold.ToString() + "$";
        }
        UpdateButtonEnable(GameManager.Instance.PlayerState.Gold);
    }

    private void OnDisable()
    {
        GameManager.Instance.ChangeGameState(EGameState.MainState);
        GameEventManager.instance.OnPostPlayerGoldChanged -= UpdateButtonEnable;
        GameEventManager.instance.OnCloseMenu -= OnCancelled;
    }

    public void UpdateButtonEnable(long NewGoldAmount)
    {
        UnlockButton.interactable = NewGoldAmount >= UnlockCost;
        if (PlayerGoldText != null)
        {
            PlayerGoldText.text = NewGoldAmount.ToString() + "$";
        }
    }

    public void OnCancelled()
    {
        Debug.Log("Cancelling");
        Destroy(gameObject);
    }

    public void OnPurchased()
    {
        if(GameManager.Instance.PlayerState.Gold >= UnlockCost)
        {
            GameEventManager.instance.Purchase(UnlockCost);
            GameEventManager.instance.UnlockRegion(UnlockRegion);
            GameEventManager.instance.DoorUnlocked(DoorId);
            Destroy(gameObject);
        }
    }
}
