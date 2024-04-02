using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerationData : MonoBehaviour
{
    static public readonly Dictionary<ECharacterSpriteAssetSlots, List<string>> SpriteLibrary = new Dictionary<ECharacterSpriteAssetSlots, List<string>>()
    {
        { ECharacterSpriteAssetSlots.Hat, new List<string>() { "ConeHat", "LeatherHat", "None", "WizardHat"} },
        { ECharacterSpriteAssetSlots.Hair, new List<string>() { "ScruffyHair", "SpikeyHair", "None", "LongBang" } },
        { ECharacterSpriteAssetSlots.Eye, new List<string>() { "NormalEye", "LashesEye", "SleepyEye"} },
        { ECharacterSpriteAssetSlots.Mouth, new List<string>() { "VMouth", "OpenSmile", "FlatMouth", "OMouth"} },
        { ECharacterSpriteAssetSlots.Nose, new List<string>() { "SmallTriangleNose", "BigNose", "LongTriangleNose", "ButtonNose" } },
        { ECharacterSpriteAssetSlots.Torso, new List<string>() { "BlankTorso", "GambesonTorso", "PuffyTorso"} },
        { ECharacterSpriteAssetSlots.Arm, new List<string>() { "BlankArm", "PaddedArm", "PuffyArm"} },
        { ECharacterSpriteAssetSlots.Bottom, new List<string>() { "BlankPants", "TrouserPants", "JesterPants"} },
        { ECharacterSpriteAssetSlots.Shoe, new List<string>() { "Blank", "Boots", "Slippers"} }
    };
}




