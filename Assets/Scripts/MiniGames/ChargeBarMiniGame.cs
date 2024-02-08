using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ChargeBarMiniGame : MonoBehaviour
{
    [SerializeField] private Scrollbar ProgressBar;
    [SerializeField] private TMP_Text ChargePercentText;
    float CurrentCharge = 0.0f;
    public float ChargeSpeed = 100.0f;
    public float ChargeMax = 100.0f;
    bool IsCharging = false;
 
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnInteractionReleased += StopCharge;
        IsCharging=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCharging)
        {
            CurrentCharge += ChargeSpeed * Time.deltaTime;
            if(CurrentCharge> ChargeMax) 
            {
                ChargeComplete();
            }
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if(ProgressBar != null)
        {
            ProgressBar.size = CurrentCharge / Mathf.Max(ChargeMax, 1f);
        }
        if(ChargePercentText != null)
        {
            ChargePercentText.text = (CurrentCharge / Mathf.Max(ChargeMax, 1f) * 100f).ToString("0.0") + "%";
        }
    }

    void ChargeComplete()
    {
        GameEventManager.instance.OnInteractionReleased -= StopCharge;
        GameEventManager.instance.MiniGameComplete(EMiniGameCompleteResult.Success);
        Destroy(this.gameObject);
    }

    void StopCharge()
    {
        GameEventManager.instance.OnInteractionReleased -= StopCharge;
        GameEventManager.instance.MiniGameComplete(EMiniGameCompleteResult.Cancelled);
        Destroy(this.gameObject);
    }
}
