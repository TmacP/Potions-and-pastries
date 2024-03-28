using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OrderLogUI : MonoBehaviour
{
    public GameObject OrderDetailPrefab;
    public List<OrderData> Orders = new List<OrderData>();
    public List<OrderUI> OrderDetails = new List<OrderUI>();




    public void Start()
    {
        GameEventManager.instance.OnTakeNPCOrder += OnTakeNPCOrder;
        GameEventManager.instance.OnDoneNPCOrder += OnDoneNPCOrder;
        GameEventManager.instance.OnNPCRecieveOrder += OnReceivedNPCOrder;

    }

    public void OnDisable()
    {
        GameEventManager.instance.OnTakeNPCOrder -= OnTakeNPCOrder;
        GameEventManager.instance.OnDoneNPCOrder -= OnDoneNPCOrder;
        GameEventManager.instance.OnNPCRecieveOrder -= OnReceivedNPCOrder;
    }



    public void OnTakeNPCOrder(OrderData order)
    {
        Orders.Add(order);
        RefreshOrderUI();
    }

    public void OnDoneNPCOrder(OrderData order)
    {
        Orders.Remove(order);
        RefreshOrderUI();
    }

    public void OnReceivedNPCOrder()
    {
        RefreshOrderUI();
    }


    public void RefreshOrderUI()
    {

        for(int j = OrderDetails.Count - 1; j >= 0; j--)
        {
            DestroyImmediate(OrderDetails[j].gameObject);
        }
        OrderDetails.Clear();

        for(int i = Orders.Count - 1; i>=0; i--)
        {
            Assert.IsNotNull(Orders[i]);
            GameObject GO = Instantiate(OrderDetailPrefab, this.transform);

            OrderUI Order = GO.GetComponent<OrderUI>();
            if(Order == null) 
            {
                Order = GO.GetComponentInChildren<OrderUI>();
            }
            Assert.IsNotNull(Order);
            OrderDetails.Add(Order);
            Order.InitializeOrderDetails(Orders[i]);
        }
    }
}
