using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Assertions;

public class NPCTracker : MonoBehaviour
{

    public NPCGenerator generator = null;
    public NPCBehaviour Behavior = null;
    public CharacterCustomization Customization = null;

    public void Awake()
    {
        if(Behavior == null)
        {
            Behavior = GetComponent<NPCBehaviour>();
        }

        if (Customization == null)
        {
            Customization = GetComponent<CharacterCustomization>();
        }
    }

    public void Init()
    {
        Assert.IsNotNull(Behavior);
        Assert.IsNotNull(Customization);
        Assert.IsNotNull(generator);
        Behavior.WaitSecChangeState(.1f, NPCBehaviour.ENPCState.Idle);

        foreach(fCharacterSpriteAssetData assetData in Behavior.CharacterData.SpriteAssetData)
        {
            switch(assetData.slot)
            {
                case ECharacterSpriteAssetSlots.Hair:
                    Customization.setHair(assetData.AssetName);
                    Customization.setHairColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Hat:
                    Customization.setHat(assetData.AssetName);
                    Customization.setHatColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Eye:
                    Customization.setEyes(assetData.AssetName);
                    Customization.setSkinColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Nose:
                    Customization.setNose(assetData.AssetName);
                    Customization.setNoseColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Torso:
                    Customization.setTorso(assetData.AssetName);
                    Customization.setTopColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Arm:
                    Customization.setArm(assetData.AssetName);
                    //Customization.setHairColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Bottom:
                    Customization.setBottom(assetData.AssetName);
                    Customization.setBottomColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Shoe:
                    Customization.setShoe(assetData.AssetName);
                    Customization.setShoeColor(assetData.AssetColour);
                    break;
                default:
                    break;
            }
        }
        
    }

    public void OnDisable()
    {
        if(generator != null)
        {
            generator.RemoveNPC(this);
        }
    }

}
