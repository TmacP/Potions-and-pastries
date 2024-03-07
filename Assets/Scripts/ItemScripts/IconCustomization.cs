using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCustomization : MonoBehaviour
{

    public Color fillingColor; 

    public void setFillingColor(Color color)
    {
        this.fillingColor = color;
        transform.Find("Filling").GetComponent<Image>().color = this.fillingColor;
    }

    void Update()
    {
        // For testing and showcase only, delete when ready to use 
        transform.Find("Filling").GetComponent<Image>().color = this.fillingColor;
    }

}
