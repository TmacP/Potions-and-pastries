using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerCustomization : MonoBehaviour
{
    public CharacterCustomization Customization;
    public SetCharacterCustomization SetCustomization;
    // Start is called before the first frame update
    void Start()
    {
        if(SetCustomization != null)
        {
            SetCustomization.SetData();
        }
        Assert.IsNotNull(Customization);
        foreach (fCharacterSpriteAssetData assetData in GameManager.Instance.PlayerState.SpriteAssetData)
        {
            switch (assetData.slot)
            {
                case ECharacterSpriteAssetSlots.Hair:
                    Customization.setHair(assetData.AssetName);
                    Customization.setHairColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Hat:
                    Customization.setHat(assetData.AssetName);
                    Customization.setHatColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Mouth:
                    Customization.setMouth(assetData.AssetName);
                    break;
                case ECharacterSpriteAssetSlots.Eye:
                    Customization.setEyes(assetData.AssetName);
                    Customization.setSkinColor(assetData.AssetColour);
                    Customization.setNoseColor(assetData.AssetColour);
                    break;
                case ECharacterSpriteAssetSlots.Nose:
                    Customization.setNose(assetData.AssetName);
                    //Customization.setNoseColor(assetData.AssetColour);
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

}
