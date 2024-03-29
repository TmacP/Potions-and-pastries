using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static NPCBehaviour;

public class OrderUI : MonoBehaviour
{

    OrderData Order;

    [SerializeField] TextMeshProUGUI PreferenceText;
    [SerializeField] Image Image;
    [SerializeField] TextMeshProUGUI NPCName;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitializeOrderDetails(OrderData InOrder)
    {
        Order = InOrder;

        Assert.IsNotNull(PreferenceText);
        Assert.IsNotNull(Image);
        Assert.IsNotNull(NPCName);

        if(InOrder.Item != null)
        {
            PreferenceText.text = Order.Item.Name;
        }
        else if(InOrder.NPCLikes != null && InOrder.NPCLikes.Count > 0)
        {
            string preference = "wants something: ";

            foreach(EItemTags Tag in InOrder.NPCLikes)
            {
                preference += (Tag.ToString());
                preference += " ";
            }
            PreferenceText.text = preference;
        }

        NPCBehaviour NPC = Order.NPCTarget?.GetComponent<NPCBehaviour>();
        if (NPC != null)
        {
            NPCName.text = NPC.CharacterData.Name;
        }
    }
}
