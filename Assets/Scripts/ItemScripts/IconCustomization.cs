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
        //transform.Find("Filling").GetComponent<SpriteRenderer>().color = this.fillingColor;
    }

    void Update()
    {
        // For testing only
        transform.Find("Filling").GetComponent<Image>().color = this.fillingColor;
    }

}
