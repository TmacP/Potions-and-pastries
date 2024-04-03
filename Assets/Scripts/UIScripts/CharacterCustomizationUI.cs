using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class CharacterCustomizationUI : MonoBehaviour
{
    public List<CharacterCustomization> customizationList;

    public void Start()
    {
        FixPlayer();
        if(GameManager.Instance.PlayerState.SpriteAssetData != null)
        {
            List<fCharacterSpriteAssetData> l = new List<fCharacterSpriteAssetData>(GameManager.Instance.PlayerState.SpriteAssetData);
            foreach (fCharacterSpriteAssetData asset in l)
            {
                ChangeSpriteAsset(asset.slot, asset);
            }
        }
        
    }
    
    public void OnConfirm()
    {
        GameManager.Instance.ChangeGameScene(EGameScene.InnInterior);
    }

    public void OnCancel()
    {
        GameManager.Instance.ChangeGameScene(EGameScene.InnInterior);
    }

    public void ChangeSpriteAsset(ECharacterSpriteAssetSlots slot, fCharacterSpriteAssetData assetData)
    {
        int index = GameManager.Instance.PlayerState.SpriteAssetData.FindIndex(s => s.slot == slot);
        if(index < 0)
        {
            Debug.Log(slot);
            FixPlayer();
        }
        index = GameManager.Instance.PlayerState.SpriteAssetData.FindIndex(s => s.slot == slot);
        Assert.IsTrue(index >= 0);
        GameManager.Instance.PlayerState.SpriteAssetData[index] = assetData;

        foreach (CharacterCustomization Customization in customizationList)
        {
            switch(slot)
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

    public void FixPlayer()
    {
        if (GameManager.Instance.PlayerState.SpriteAssetData.Count < 8)
        {
            SetCharacterCustomization C = FindObjectOfType<SetCharacterCustomization>();
            Assert.IsNotNull(C);

            List<fCharacterSpriteAssetData> assetData = new List<fCharacterSpriteAssetData>();

            fCharacterSpriteAssetData HatData = new fCharacterSpriteAssetData();
            HatData.slot = ECharacterSpriteAssetSlots.Hat;
            HatData.AssetName = C.nameHat;
            HatData.AssetColour = C.colorHat;
            assetData.Add(HatData);

            fCharacterSpriteAssetData HairData = new fCharacterSpriteAssetData();
            HairData.slot = ECharacterSpriteAssetSlots.Hair;
            HairData.AssetName = C.nameHair;
            HairData.AssetColour = C.colorHair;
            assetData.Add(HairData);

            fCharacterSpriteAssetData EyeData = new fCharacterSpriteAssetData();
            EyeData.slot = ECharacterSpriteAssetSlots.Eye;
            EyeData.AssetName = C.nameEye;
            EyeData.AssetColour = C.colorSkin;
            assetData.Add(EyeData);

            fCharacterSpriteAssetData MouthData = new fCharacterSpriteAssetData();
            MouthData.slot = ECharacterSpriteAssetSlots.Mouth;
            MouthData.AssetName = C.nameMouth;
            assetData.Add(MouthData);

            fCharacterSpriteAssetData TorsoData = new fCharacterSpriteAssetData();
            TorsoData.slot = ECharacterSpriteAssetSlots.Torso;
            TorsoData.AssetName = C.nameTorso;
            TorsoData.AssetColour = C.colorTop;
            assetData.Add(TorsoData);

            fCharacterSpriteAssetData ArmData = new fCharacterSpriteAssetData();
            ArmData.slot = ECharacterSpriteAssetSlots.Arm;
            ArmData.AssetName = C.nameArm;
            assetData.Add(ArmData);

            fCharacterSpriteAssetData BottomData = new fCharacterSpriteAssetData();
            BottomData.slot = ECharacterSpriteAssetSlots.Bottom;
            BottomData.AssetName = C.nameBottom;
            BottomData.AssetColour = C.colorBottom;
            assetData.Add(BottomData);

            fCharacterSpriteAssetData ShoeData = new fCharacterSpriteAssetData();
            ShoeData.slot = ECharacterSpriteAssetSlots.Shoe;
            ShoeData.AssetName = C.nameShoes;
            ShoeData.AssetColour = C.colorShoe;
            assetData.Add(ShoeData);

            GameManager.Instance.PlayerState.SpriteAssetData = assetData;
            Assert.IsTrue(GameManager.Instance.PlayerState.SpriteAssetData.Count == 8);
        }
    }
}
