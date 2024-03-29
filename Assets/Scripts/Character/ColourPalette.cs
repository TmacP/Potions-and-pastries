using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "CozyData/ColourPalette")]
public class ColourPalette : ScriptableObject
{
    public List<Color> colors;
}
