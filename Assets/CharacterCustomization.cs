using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    private Transform frontCharacter;
    private Transform backCharacter;

    // Hat variables
    public int positionHat;
    public Color colorHat;
    [SerializeField] public List<Sprite> frontCharacterHats;
    [SerializeField] public List<Sprite> backCharacterHats;

    // Skin variables
    public Color colorSkin;



    void Awake()
    {
        frontCharacter = transform.Find("F_BaseCharacter");
        backCharacter = transform.Find("B_BaseCharacter");
        colorHat = Color.white;
        colorSkin = Color.white;
    }

    public void setHat(int pos)
    {
        if (pos > this.frontCharacterHats.Count)
        {
            Debug.Log("Error: Character hat int position (" + pos + " ) exceeds list of front wearable hats ("+ this.frontCharacterHats.Count + ")!");
        }
        else if (pos > this.backCharacterHats.Count)
        {
            Debug.Log("Error: Character hat int position (" + pos + " ) exceeds list of back wearable hats (" + this.backCharacterHats.Count + ")!");
        }
        else
        {
            this.positionHat = pos;
        }

    }

    void Update()
    {
        frontCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite = this.frontCharacterHats[this.positionHat];
        frontCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().color = this.colorHat;
        backCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite = this.backCharacterHats[this.positionHat];
        backCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().color = this.colorHat;

        frontCharacter.transform.Find("Head").GetComponent<SpriteRenderer>().color = this.colorSkin;
        frontCharacter.transform.Find("Neck").GetComponent<SpriteRenderer>().color = this.colorSkin;
        frontCharacter.transform.Find("R_Hand").GetComponent<SpriteRenderer>().color = this.colorSkin;
        frontCharacter.transform.Find("L_Hand").GetComponent<SpriteRenderer>().color = this.colorSkin;
        backCharacter.transform.Find("Head").GetComponent<SpriteRenderer>().color = this.colorSkin;
        backCharacter.transform.Find("Neck").GetComponent<SpriteRenderer>().color = this.colorSkin;
        backCharacter.transform.Find("R_Hand").GetComponent<SpriteRenderer>().color = this.colorSkin;
        backCharacter.transform.Find("L_Hand").GetComponent<SpriteRenderer>().color = this.colorSkin;
    }
}
