using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class OrderUI : MonoBehaviour
{

    //[SerializeField] public GameObject OrderTextUI;
    [SerializeField] public TextMeshProUGUI OrderText;


    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnTakeNPCOrder += OnTakeNPCOrder;

    }
    public void OnTakeNPCOrder(OrderData order)
    {
        OrderText.SetText("Can I get something " + Enum.GetName(typeof(EItemTags), order.NPCLikes[0]) + "?");
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnTakeNPCOrder -= OnTakeNPCOrder;
        OrderText.SetText("");
    }


}
