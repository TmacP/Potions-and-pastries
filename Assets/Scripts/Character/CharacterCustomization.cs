using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class CharacterCustomization : MonoBehaviour
{
    private Transform frontCharacter;
    private Transform backCharacter;

    // Hat variables
    public string nameHat;
    public Color colorHat;

    // Hair/Brow variables 
    public string nameHair;
    public Color colorHair;

    // Eye variables
    public string nameEye;

    // Nose variables
    public string nameNose;
    public Color colorNose;

    // Mouth variables 
    public string nameMouth;

    // Skin variables
    public Color colorSkin;

    // Top variables
    public string nameTorso;
    public string nameArm;
    public Color colorTop;
    
    // Bottom variables 
    public string nameBottom;
    public Color colorBottom;

    // Shoe variables
    public string nameShoes;
    public Color colorShoe;

    void Awake()
    {
        frontCharacter = transform.Find("F_BaseCharacter");
        backCharacter = transform.Find("B_BaseCharacter");
    }

    public void setHat(string hatName)
    {
         this.nameHat = hatName;
         frontCharacter.transform.Find("Hat").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontHat", this.nameHat);
         backCharacter.transform.Find("Hat").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackHat", this.nameHat);
    } 

    public void setHatColor(Color color)
    {
        if (!frontCharacter.transform.Find("Hat"))
        {
            Debug.Log("Error: Character doesn't have front 'Hat' sprite!");
        }
        else if (!backCharacter.transform.Find("Hat"))
        {
            Debug.Log("Error: Character doesn't have back 'Hat' sprite!");
        }
        else
        {
            this.colorHat = color;
            frontCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().color = this.colorHat;

            backCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().color = this.colorHat;
        }
    }

    public void setHair(string hairName)
    {
        this.nameHair = hairName;
        frontCharacter.transform.Find("Hair").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontHair", this.nameHair);
        backCharacter.transform.Find("Hair").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackHair", this.nameHair);
    }

    public void setHairColor(Color color)
    {
        if (!frontCharacter.transform.Find("Hair") || !frontCharacter.transform.Find("R_Brow") || !frontCharacter.transform.Find("L_Brow"))
        {
            Debug.Log("Error: Character doesn't have front 'Hair', 'R_Brow', and or 'L_Brow' sprites!");
        }
        else if (!backCharacter.transform.Find("Hair"))
        {
            Debug.Log("Error: Character doesn't have back 'Hair' sprite!");
        }
        else
        {
            this.colorHair = color;
            frontCharacter.transform.Find("Hair").GetComponent<SpriteRenderer>().color = this.colorHair;
            backCharacter.transform.Find("Hair").GetComponent<SpriteRenderer>().color = this.colorHair;
            frontCharacter.transform.Find("R_Brow").GetComponent<SpriteRenderer>().color = this.colorHair;
            frontCharacter.transform.Find("L_Brow").GetComponent<SpriteRenderer>().color = this.colorHair;
        }
    }

    public void setEyes(string eyeName)
    {
        this.nameEye = eyeName;
        frontCharacter.transform.Find("R_Eye").GetComponent<SpriteResolver>().SetCategoryAndLabel("RightEye", this.nameEye);
        frontCharacter.transform.Find("L_Eye").GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftEye", this.nameEye);
    }

    public void setNose(string noseName)
    {
        this.nameNose = noseName;
        frontCharacter.transform.Find("Nose").GetComponent<SpriteResolver>().SetCategoryAndLabel("Nose", this.nameNose);
    }

    public void setNoseColor(Color color)
    {
        if (!frontCharacter.transform.Find("Nose"))
        {
            Debug.Log("Error: Character doesn't have front 'Nose' sprite!");
        }
        else
        {
            this.colorNose = color;
            frontCharacter.transform.Find("Nose").GetComponent<SpriteRenderer>().color = this.colorNose;
        }
    }

    public void setMouth(string mouthName)
    {
        this.nameMouth = mouthName;
        frontCharacter.transform.Find("Mouth").GetComponent<SpriteResolver>().SetCategoryAndLabel("Mouth", this.nameMouth);
    }

    public void setSkinColor(Color color)
    {
        if (!frontCharacter.transform.Find("Head") || !frontCharacter.transform.Find("Neck") || !frontCharacter.transform.Find("R_Hand") || !frontCharacter.transform.Find("L_Hand"))
        {
            Debug.Log("Error: Character doesn't have front 'Head','Neck', 'R_Hand', and or 'L_Hand' sprite!");
        }
        else if (!backCharacter.transform.Find("Head") || !backCharacter.transform.Find("Neck") || !backCharacter.transform.Find("R_Hand") || !backCharacter.transform.Find("L_Hand"))
        {
            Debug.Log("Error: Character doesn't have back 'Head','Neck', 'R_Hand', and or 'L_Hand' sprite!");
        }
        else
        {
            this.colorSkin = color;

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

    public void setTopColor(Color color)
    {
        if (!frontCharacter.transform.Find("Torso") || !frontCharacter.transform.Find("R_UpperArm") || !frontCharacter.transform.Find("R_LowerArm") || !frontCharacter.transform.Find("L_UpperArm") || !frontCharacter.transform.Find("L_LowerArm"))
        {
            Debug.Log("Error: Character doesn't have front 'Torso','R_UpperArm', 'R_LowerArm', 'L_UpperArm' and or 'L_LowerArm' sprite!");
        }
        else if (!backCharacter.transform.Find("Torso") || !backCharacter.transform.Find("R_UpperArm") || !backCharacter.transform.Find("R_LowerArm") || !backCharacter.transform.Find("L_UpperArm") || !backCharacter.transform.Find("L_LowerArm"))
        {
            Debug.Log("Error: Character doesn't have back 'Torso','R_UpperArm', 'R_LowerArm', 'L_UpperArm' and or 'L_LowerArm' sprite!");
        }
        else
        {
            this.colorTop = color;

            frontCharacter.transform.Find("Torso").GetComponent<SpriteRenderer>().color = this.colorTop;
            frontCharacter.transform.Find("R_UpperArm").GetComponent<SpriteRenderer>().color = this.colorTop;
            frontCharacter.transform.Find("R_LowerArm").GetComponent<SpriteRenderer>().color = this.colorTop;
            frontCharacter.transform.Find("L_UpperArm").GetComponent<SpriteRenderer>().color = this.colorTop;
            frontCharacter.transform.Find("L_LowerArm").GetComponent<SpriteRenderer>().color = this.colorTop;

            backCharacter.transform.Find("Torso").GetComponent<SpriteRenderer>().color = this.colorTop;
            backCharacter.transform.Find("R_UpperArm").GetComponent<SpriteRenderer>().color = this.colorTop;
            backCharacter.transform.Find("R_LowerArm").GetComponent<SpriteRenderer>().color = this.colorTop;
            backCharacter.transform.Find("L_UpperArm").GetComponent<SpriteRenderer>().color = this.colorTop;
            backCharacter.transform.Find("L_LowerArm").GetComponent<SpriteRenderer>().color = this.colorTop;
        }
    } 

    public void setTorso(string torsoName)
    {
        this.nameTorso = torsoName;
        frontCharacter.transform.Find("Torso").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontTorso", this.nameTorso);
        backCharacter.transform.Find("Torso").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackTorso", this.nameTorso);
    }

    public void setArm(string armName)
    {
        this.nameArm = armName;

        frontCharacter.transform.Find("R_UpperArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontRightUpperArm", this.nameArm);
        backCharacter.transform.Find("R_UpperArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackRightUpperArm", this.nameArm);
        frontCharacter.transform.Find("R_LowerArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontRightLowerArm", this.nameArm);
        backCharacter.transform.Find("R_LowerArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackRightLowerArm", this.nameArm);

        frontCharacter.transform.Find("L_UpperArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontLeftUpperArm", this.nameArm);
        backCharacter.transform.Find("L_UpperArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackLeftUpperArm", this.nameArm);
        frontCharacter.transform.Find("L_LowerArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontLeftLowerArm", this.nameArm);
        backCharacter.transform.Find("L_LowerArm").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackLeftLowerArm", this.nameArm);
    }

    public void setBottomColor(Color color)
    {
        if (!frontCharacter.transform.Find("Pelvis") || !frontCharacter.transform.Find("R_UpperLeg") || !frontCharacter.transform.Find("L_UpperLeg"))
        {
            Debug.Log("Error: Character doesn't have front 'Pelvis','R_UpperLeg', and or'L_UpperLeg' sprite!");
        }
        else if (!backCharacter.transform.Find("Pelvis") || !backCharacter.transform.Find("R_UpperLeg") || !backCharacter.transform.Find("L_UpperLeg"))
        {
            Debug.Log("Error: Character doesn't have back 'Pelvis','R_UpperLeg', and or'L_UpperLeg' sprite!");
        }
        else
        {
            this.colorBottom = color;

            frontCharacter.transform.Find("Pelvis").GetComponent<SpriteRenderer>().color = this.colorBottom;
            frontCharacter.transform.Find("R_UpperLeg").GetComponent<SpriteRenderer>().color = this.colorBottom;
            frontCharacter.transform.Find("L_UpperLeg").GetComponent<SpriteRenderer>().color = this.colorBottom;

            backCharacter.transform.Find("Pelvis").GetComponent<SpriteRenderer>().color = this.colorBottom;
            backCharacter.transform.Find("R_UpperLeg").GetComponent<SpriteRenderer>().color = this.colorBottom;
            backCharacter.transform.Find("L_UpperLeg").GetComponent<SpriteRenderer>().color = this.colorBottom;
        }
    }

    public void setBottom(string bottomName)
    {
        this.nameBottom = bottomName;
        frontCharacter.transform.Find("Pelvis").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontPelvis", this.nameBottom);
        backCharacter.transform.Find("Pelvis").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackPelvis", this.nameBottom);

        frontCharacter.transform.Find("R_UpperLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontRightUpperLeg", this.nameBottom);
        backCharacter.transform.Find("R_UpperLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackRightUpperLeg", this.nameBottom);

        frontCharacter.transform.Find("L_UpperLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontLeftUpperLeg", this.nameBottom);
        backCharacter.transform.Find("L_UpperLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackLeftUpperLeg", this.nameBottom);
    }

    public void setShoeColor(Color color)
    {
        if (!frontCharacter.transform.Find("R_LowerLeg") || !frontCharacter.transform.Find("L_UpperLeg"))
        {
            Debug.Log("Error: Character doesn't have front 'R_LowerLeg', and or 'L_LowerLeg' sprite!");
        }
        else if (!backCharacter.transform.Find("R_LowerLeg") || !backCharacter.transform.Find("L_UpperLeg"))
        {
            Debug.Log("Error: Character doesn't have back 'R_LowerLeg', and or 'L_LowerLeg' sprite!");
        }
        else
        {
            this.colorShoe = color;

            frontCharacter.transform.Find("R_LowerLeg").GetComponent<SpriteRenderer>().color = this.colorShoe;
            frontCharacter.transform.Find("L_LowerLeg").GetComponent<SpriteRenderer>().color = this.colorShoe;

            backCharacter.transform.Find("R_LowerLeg").GetComponent<SpriteRenderer>().color = this.colorShoe;
            backCharacter.transform.Find("L_LowerLeg").GetComponent<SpriteRenderer>().color = this.colorShoe;
        }
    }

    public void setShoe(string shoeName)
    {
        this.nameShoes = shoeName;

        frontCharacter.transform.Find("R_LowerLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontRightLowerLeg", this.nameShoes);
        backCharacter.transform.Find("R_LowerLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackRightLowerLeg", this.nameShoes);

        frontCharacter.transform.Find("L_LowerLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("FrontLeftLowerLeg", this.nameShoes);
        backCharacter.transform.Find("L_LowerLeg").GetComponent<SpriteResolver>().SetCategoryAndLabel("BackRightLowerLeg", this.nameShoes);
    }

    void Start()
    {
        setHat("None");
        setHatColor(new Color(255, 00, 0, 255));

        setHair("SpikeyHair");
        setHairColor(new Color(0.137f, 0.196f, 0.361f));

        setEyes("LashesEye");

        setNose("BigNose");
        setNoseColor(new Color(1f, 0.91f, 0.765f));

        setMouth("OpenSmileMouth"); 

        setSkinColor(new Color(1f, 0.91f, 0.765f));

        setTorso("GambesonTorso");
        setArm("PaddedArm");
        setTopColor(new Color(0.82f, 0.82f, 0.82f));

        setBottom("TrouserPants");
        setBottomColor(new Color(0.341f, 0.204f, 0.102f));

        setShoe("Boots");
        setShoeColor(new Color(0.341f, 0.2f, 0f));

    }
}
