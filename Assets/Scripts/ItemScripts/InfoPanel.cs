using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public TMP_Text InfoBoxName;
    public TMP_Text InfoBoxDescription;
    public Image InfoBoxImage;

    public void SetInfo(string name, string description, Sprite image)
    {
        
        Debug.Log(name + description);
        InfoBoxName.text = name;
        InfoBoxDescription.text = description;
        InfoBoxImage.sprite = image;
        InfoBoxImage.color = new Color(255, 255, 255);
    }

    public void ClearInfo()
    {
        InfoBoxName.text = "";
        InfoBoxDescription.text = "";
        InfoBoxImage.sprite = null;
        
    }

    void Start()
    {
        ClearInfo();
    }

}
