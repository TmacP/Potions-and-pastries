using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static NPCBehaviour;

public class OrderUI : MonoBehaviour
{

    //[SerializeField] public GameObject OrderTextUI;
    [SerializeField] public TextMeshProUGUI OrderText;
    [SerializeField] public NPCBehaviour NPC;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnTakeNPCOrder += OnTakeNPCOrder;
        GameEventManager.instance.OnDoneNPCOrder += OnDoneNPCOrder;
        GameEventManager.instance.OnNPCRecieveOrder += OnReceivedNPCOrder;

    }
    public void OnTakeNPCOrder(OrderData order)
    {
        OrderText.SetText("Can I get something " + Enum.GetName(typeof(EItemTags), order.NPCLikes[0]) + "?");
    }

    public void OnDoneNPCOrder(OrderData order)
    {
        OrderText.SetText("");
    }

    public void OnReceivedNPCOrder()
    {
        OrderText.SetText("Order done");
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnTakeNPCOrder -= OnTakeNPCOrder;
        GameEventManager.instance.OnDoneNPCOrder -= OnTakeNPCOrder;
        GameEventManager.instance.OnNPCRecieveOrder -= OnReceivedNPCOrder;
    }


}
