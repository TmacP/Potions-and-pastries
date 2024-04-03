using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class CharacterCustomizationSlotUI : MonoBehaviour
{
    public CharacterCustomizationUI CharHUD;
    public ECharacterSpriteAssetSlots slot;

    public TextMeshProUGUI Text;

    public void Start()
    {
        CharHUD = FindObjectOfType<CharacterCustomizationUI>();
        Assert.IsNotNull(CharHUD);
        Assert.IsNotNull(Text);

        fCharacterSpriteAssetData assetData = GameManager.Instance.PlayerState.SpriteAssetData.Find(r => slot == r.slot);
        Text.text = assetData.AssetName;
    }


    public void IncrementAsset()
    {
        fCharacterSpriteAssetData assetData = GameManager.Instance.PlayerState.SpriteAssetData.Find(r => slot == r.slot);

        List<string> l = NPCGenerationData.SpriteLibrary[slot];

        int index = l.FindIndex(s => s == assetData.AssetName);
        index++;
        if (index >= l.Count || index < 0)
        {
            index = 0;
        }

        assetData.AssetName = l[index];

        CharHUD.ChangeSpriteAsset(slot, assetData);
        Text.text = assetData.AssetName;
    }

    public void DecrementAsset()
    {
        fCharacterSpriteAssetData assetData = GameManager.Instance.PlayerState.SpriteAssetData.Find(r => slot == r.slot);

        List<string> l = NPCGenerationData.SpriteLibrary[slot];

        int index = l.FindIndex(s => s == assetData.AssetName);
        index--;
        if (index < 0 || index >= l.Count)
        {
            index = l.Count-1;
        }


        assetData.AssetName = l[index];
        CharHUD.ChangeSpriteAsset(slot, assetData);
        Text.text = assetData.AssetName;
    }


}
