using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "CozyData/CharacterColourPalette")]
public class CharacterColourPalette : ScriptableObject
{
    public List<ColourPalette> Hat;
    public List<ColourPalette> Hair;
    public List<ColourPalette> Eye;
    public List<ColourPalette> Nose;
    public List<ColourPalette> Torso;
    public List<ColourPalette> Arm;
    public List<ColourPalette> Bottom;
    public List<ColourPalette> Shoe;
}
