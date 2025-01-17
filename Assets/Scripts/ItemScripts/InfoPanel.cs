using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public TMP_Text InfoBoxName;
    public TMP_Text InfoBoxDescription;
    public TMP_Text InfoBoxTags;
    public Image InfoBoxImage;
    //public Button UseItemButton;
    //public Button DestroyItemButton;

    public void SetInfo(string name, string description, HashSet<EItemTags> Tags, Sprite image)
    {
        this.gameObject.SetActive(true);
        InfoBoxName.text = name;
        InfoBoxDescription.text = description;

        string TagString = "";

        if(Tags != null && Tags.Count > 0 )
        {
            foreach (EItemTags tag in Tags)
            {
                TagString += tag.ToString() + ", ";
            }
        }
        

        InfoBoxTags.text = TagString;
        InfoBoxImage.sprite = image;
        InfoBoxImage.color = new Color(1, 1, 1);
        //UseItemButton.gameObject.SetActive(true);
        //DestroyItemButton.gameObject.SetActive(true);
       
    }

    public void ClearInfo()
    {
        this.gameObject.SetActive(false);
        InfoBoxName.text = "";
        InfoBoxDescription.text = "";
        InfoBoxImage.sprite = null;
        //UseItemButton.gameObject.SetActive(false);
        //DestroyItemButton.gameObject.SetActive(false);
        // Convert color values from 0-255 range to 0-1 range
        float r = 212f / 255f;
        float g = 182f / 255f;
        float b = 139f / 255f;
        InfoBoxImage.color = new Color(r, g, b, 1); // Alpha is 1 for fully opaque
    }

    void Start()
    {
        //ClearInfo();
    }

}
