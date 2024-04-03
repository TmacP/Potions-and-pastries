using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerationData : MonoBehaviour
{
    static public readonly Dictionary<ECharacterSpriteAssetSlots, List<string>> SpriteLibrary = new Dictionary<ECharacterSpriteAssetSlots, List<string>>()
    {
        { ECharacterSpriteAssetSlots.Hat, new List<string>() { "ConeHat", "LeatherHat", "None", "WizardHat", "HornedHat"} },
        { ECharacterSpriteAssetSlots.Hair, new List<string>() { "ScruffyHair", "SpikeyHair", "None", "LongBangsHair", "SlickedHair" } },
        { ECharacterSpriteAssetSlots.Eye, new List<string>() { "NormalEye", "LashesEye", "SleepyEye", "XDEye"} },
        { ECharacterSpriteAssetSlots.Nose, new List<string>() { "SmallTriangleNose", "BigNose", "LongTriangleNose", "ButtonNose"} },
        { ECharacterSpriteAssetSlots.Mouth, new List<string>() { "VMouth", "OpenSmileMouth", "FlatMouth", "OMouth"} },
        { ECharacterSpriteAssetSlots.Torso, new List<string>() { "BlankTorso", "GambesonTorso", "PuffyTorso", "ArmoredTorso"} },
        { ECharacterSpriteAssetSlots.Arm, new List<string>() { "BlankArm", "PaddedArm", "PuffyArm", "ArmoredArm"} },
        { ECharacterSpriteAssetSlots.Bottom, new List<string>() { "BlankPants", "TrouserPants", "JesterPants", "ArmoredPants"} },
        { ECharacterSpriteAssetSlots.Shoe, new List<string>() { "Blank", "Boots", "Slippers", "ArmoredBoots"} }
    };
}




