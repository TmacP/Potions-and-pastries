using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static NPCBehaviour;

public class OrderUI : MonoBehaviour
{
    OrderData Order;

    [SerializeField] ItemTags TagRelations;

    [SerializeField] List<Image> Images;

    [SerializeField] TextMeshProUGUI NPCName;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitializeOrderDetails(OrderData InOrder)
    {
        Order = InOrder;

        Assert.IsNotNull(NPCName);

        foreach(Image image in Images)
        {
            image.gameObject.SetActive(false);
        }

        if(InOrder.NPCLikes != null && InOrder.NPCLikes.Count > 0)
        {
            int i = 0;
            foreach(EItemTags Tag in InOrder.NPCLikes)
            {
                ItemTagRelation ITR = TagRelations.TagRelations.First(r => r.Tag == Tag);

                Assert.IsTrue(ITR.Data != null && ITR.Data.image != null);
                
                Images[i].sprite = ITR.Data.image;
                Images[i].gameObject.SetActive(true);
                i++;
                
                if(i >= Images.Count)
                {
                    Debug.Log("Too many likes for Order UI");
                    break;
                }
            }
        }

        NPCBehaviour NPC = Order.NPCTarget?.GetComponent<NPCBehaviour>();
        if (NPC != null)
        {
            NPCName.text = NPC.CharacterData.Name;
        }
    }

    public void ShowTarget()
    {
        NPCBehaviour NPC = Order.NPCTarget.GetComponent<NPCBehaviour>();
        Assert.IsNotNull(NPC);
        NPC.ShowTarget();
    }
}
