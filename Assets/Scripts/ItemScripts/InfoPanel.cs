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
        InfoBoxImage.color = new Color(1, 1, 1);
        
    }

    public void ClearInfo()
    {
        InfoBoxName.text = "";
        InfoBoxDescription.text = "";
        InfoBoxImage.sprite = null;

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
