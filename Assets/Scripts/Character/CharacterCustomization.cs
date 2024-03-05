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

    // Hair/Brow variables 
    public int positionHair;
    public Color colorHair;
    [SerializeField] public List<Sprite> frontCharacterHair;
    [SerializeField] public List<Sprite> backCharacterHair;

    // Eye variables
    public int positionEye;
    [SerializeField] public List<Sprite> rightCharacterEye;
    [SerializeField] public List<Sprite> leftCharacterEye;

    // Nose variables
    public int positionNose;
    public Color colorNose;
    [SerializeField] public List<Sprite> frontCharacterNose;

    // Mouth variables 
    public int positionMouth;
    [SerializeField] public List<Sprite> frontCharacterMouth;

    // Skin variables
    public Color colorSkin;

    // Top variables
    public int positionTops;
    public Color colorTop;
    //      FrontSprites
    [SerializeField] public List<Sprite> frontCharacterTorso;
    [SerializeField] public List<Sprite> frontCharacterRightUpperArm;
    [SerializeField] public List<Sprite> frontCharacterRightLowerArm;
    [SerializeField] public List<Sprite> frontCharacterLeftUpperArm;
    [SerializeField] public List<Sprite> frontCharacterLeftLowerArm;
    //      BackSprites
    [SerializeField] public List<Sprite> backCharacterTorso;
    [SerializeField] public List<Sprite> backCharacterRightUpperArm;
    [SerializeField] public List<Sprite> backCharacterRightLowerArm;
    [SerializeField] public List<Sprite> backCharacterLeftUpperArm;
    [SerializeField] public List<Sprite> backCharacterLeftLowerArm;

    // Bottom variables 
    public int positionBottoms;
    public Color colorBottom;
    //      FrontSprites
    [SerializeField] public List<Sprite> frontCharacterPelvis;
    [SerializeField] public List<Sprite> frontCharacterRightUpperLeg;
    [SerializeField] public List<Sprite> frontCharacterLeftUpperLeg;
    //      BackSprites
    [SerializeField] public List<Sprite> backCharacterPelvis;
    [SerializeField] public List<Sprite> backCharacterRightUpperLeg;
    [SerializeField] public List<Sprite> backCharacterLeftUpperLeg;

    // Shoe variables
    public int positionShoes;
    public Color colorShoe;
    [SerializeField] public List<Sprite> frontCharacterRightLowerLeg;
    [SerializeField] public List<Sprite> frontCharacterLeftLowerLeg;
    [SerializeField] public List<Sprite> backCharacterRightLowerLeg;
    [SerializeField] public List<Sprite> backCharacterLeftLowerLeg;

    void Awake()
    {
        frontCharacter = transform.Find("F_BaseCharacter");
        backCharacter = transform.Find("B_BaseCharacter");
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
            frontCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite = this.frontCharacterHats[this.positionHat];
            backCharacter.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite = this.backCharacterHats[this.positionHat];
            
        }
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

    public void setHair(int pos)
    {
        if (pos > this.frontCharacterHair.Count)
        {
            Debug.Log("Error: Character hair int position (" + pos + " ) exceeds list of front wearable hair (" + this.frontCharacterHair.Count + ")!");
        }
        else if (pos > this.backCharacterHair.Count)
        {
            Debug.Log("Error: Character hair int position (" + pos + " ) exceeds list of back wearable hair (" + this.backCharacterHair.Count + ")!");
        }
        else
        {
            this.positionHair = pos;
            frontCharacter.transform.Find("Hair").GetComponent<SpriteRenderer>().sprite = this.frontCharacterHair[this.positionHair];
            backCharacter.transform.Find("Hair").GetComponent<SpriteRenderer>().sprite = this.backCharacterHair[this.positionHair];
        }
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

    public void setEyes(int pos)
    {
        if (pos > this.rightCharacterEye.Count)
        {
            Debug.Log("Error: Character right eye int position (" + pos + " ) exceeds list of right eyes (" + this.rightCharacterEye.Count + ")!");
        }
        else if (pos > this.leftCharacterEye.Count)
        {
            Debug.Log("Error: Character left eye int position (" + pos + " ) exceeds list of left eyes (" + this.leftCharacterEye.Count + ")!");
        }
        else
        {
            this.positionEye = pos;
            frontCharacter.transform.Find("R_Eye").GetComponent<SpriteRenderer>().sprite = this.rightCharacterEye[this.positionEye];
            frontCharacter.transform.Find("L_Eye").GetComponent<SpriteRenderer>().sprite = this.leftCharacterEye[this.positionEye];
        }
    }

    public void setNose(int pos)
    {
        if (pos > this.frontCharacterNose.Count)
        {
            Debug.Log("Error: Character nose int position (" + pos + " ) exceeds list of front wearable noses (" + this.frontCharacterNose.Count + ")!");
        }
        else
        {
            this.positionNose = pos;
            frontCharacter.transform.Find("Nose").GetComponent<SpriteRenderer>().sprite = this.frontCharacterNose[this.positionNose];
        }
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

    public void setMouth(int pos)
    {
        if (pos > this.frontCharacterMouth.Count)
        {
            Debug.Log("Error: Character mouth int position (" + pos + " ) exceeds list of front wearable mouthes (" + this.frontCharacterMouth.Count + ")!");
        }
        else
        {
            this.positionMouth = pos;
            frontCharacter.transform.Find("Mouth").GetComponent<SpriteRenderer>().sprite = this.frontCharacterMouth[this.positionMouth];
        }
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

    public void setTop(int pos)
    {
        if (pos > this.frontCharacterTorso.Count || pos > this.backCharacterTorso.Count)
        {
            Debug.Log("Error: Character torso int position exceeds list of front or back wearable torsos!");
        }
        else if (pos > this.frontCharacterRightUpperArm.Count || pos > this.backCharacterRightUpperArm.Count || pos > this.frontCharacterRightLowerArm.Count || pos > this.backCharacterRightLowerArm.Count)
        {
            Debug.Log("Error: Character right arm(upper or lower) int position exceeds list of front or back wearable right arms (upper or lower)!");
        } 
        else if (pos > this.frontCharacterLeftUpperArm.Count || pos > this.backCharacterLeftUpperArm.Count || pos > this.frontCharacterLeftLowerArm.Count || pos > this.backCharacterLeftLowerArm.Count)
        {
            Debug.Log("Error: Character left arm(upper or lower) int position exceeds list of front or back wearable left arms (upper or lower)!");
        }
        else
        {
            this.positionTops = pos;
            frontCharacter.transform.Find("Torso").GetComponent<SpriteRenderer>().sprite = this.frontCharacterTorso[this.positionTops];
            backCharacter.transform.Find("Torso").GetComponent<SpriteRenderer>().sprite = this.backCharacterTorso[this.positionTops];

            frontCharacter.transform.Find("R_UpperArm").GetComponent<SpriteRenderer>().sprite = this.frontCharacterRightUpperArm[this.positionTops];
            backCharacter.transform.Find("R_UpperArm").GetComponent<SpriteRenderer>().sprite = this.backCharacterRightUpperArm[this.positionTops];
            frontCharacter.transform.Find("R_LowerArm").GetComponent<SpriteRenderer>().sprite = this.frontCharacterRightLowerArm[this.positionTops];
            backCharacter.transform.Find("R_LowerArm").GetComponent<SpriteRenderer>().sprite = this.backCharacterRightLowerArm[this.positionTops];

            frontCharacter.transform.Find("L_UpperArm").GetComponent<SpriteRenderer>().sprite = this.frontCharacterLeftUpperArm[this.positionTops];
            backCharacter.transform.Find("L_UpperArm").GetComponent<SpriteRenderer>().sprite = this.backCharacterLeftUpperArm[this.positionTops];
            frontCharacter.transform.Find("L_LowerArm").GetComponent<SpriteRenderer>().sprite = this.frontCharacterLeftLowerArm[this.positionTops];
            backCharacter.transform.Find("L_LowerArm").GetComponent<SpriteRenderer>().sprite = this.backCharacterLeftLowerArm[this.positionTops];
        }
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

    public void setBottom(int pos)
    {
        if (pos > this.frontCharacterPelvis.Count || pos > this.backCharacterPelvis.Count)
        {
            Debug.Log("Error: Character pelvis int position exceeds list of front or back wearable pelvises!");
        }
        else if (pos > this.frontCharacterRightUpperLeg.Count || pos > this.backCharacterRightUpperLeg.Count)
        {
            Debug.Log("Error: Character right upper leg int position exceeds list of front or back wearable right upper legs!");
        }
        else if (pos > this.frontCharacterLeftUpperLeg.Count || pos > this.backCharacterLeftUpperLeg.Count)
        {
            Debug.Log("Error: Character left upper leg int position exceeds list of front or back wearable left upper legs!"); ;
        }
        else
        {
            this.positionBottoms = pos;
            frontCharacter.transform.Find("Pelvis").GetComponent<SpriteRenderer>().sprite = this.frontCharacterPelvis[this.positionBottoms];
            backCharacter.transform.Find("Pelvis").GetComponent<SpriteRenderer>().sprite = this.backCharacterPelvis[this.positionBottoms];

            frontCharacter.transform.Find("R_UpperLeg").GetComponent<SpriteRenderer>().sprite = this.frontCharacterRightUpperLeg[this.positionBottoms];
            backCharacter.transform.Find("R_UpperLeg").GetComponent<SpriteRenderer>().sprite = this.backCharacterRightUpperLeg[this.positionBottoms];

            frontCharacter.transform.Find("L_UpperLeg").GetComponent<SpriteRenderer>().sprite = this.frontCharacterLeftUpperLeg[this.positionBottoms];
            backCharacter.transform.Find("L_UpperLeg").GetComponent<SpriteRenderer>().sprite = this.backCharacterLeftUpperLeg[this.positionBottoms];
        }
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

    public void setShoe(int pos)
    {
        if (pos > this.frontCharacterRightLowerLeg.Count || pos > this.backCharacterRightLowerLeg.Count)
        {
            Debug.Log("Error: Character right lower leg int position exceeds list of front or back wearable right lower legs!");
        }
        else if (pos > this.frontCharacterLeftLowerLeg.Count || pos > this.backCharacterLeftLowerLeg.Count)
        {
            Debug.Log("Error: Character left lower leg int position exceeds list of front or back wearable left lower legs!"); ;
        }
        else
        {
            this.positionShoes = pos;

            frontCharacter.transform.Find("R_LowerLeg").GetComponent<SpriteRenderer>().sprite = this.frontCharacterRightLowerLeg[this.positionShoes];
            backCharacter.transform.Find("R_LowerLeg").GetComponent<SpriteRenderer>().sprite = this.backCharacterRightLowerLeg[this.positionShoes];

            frontCharacter.transform.Find("L_LowerLeg").GetComponent<SpriteRenderer>().sprite = this.frontCharacterLeftLowerLeg[this.positionShoes];
            backCharacter.transform.Find("L_LowerLeg").GetComponent<SpriteRenderer>().sprite = this.backCharacterLeftLowerLeg[this.positionShoes];
        }
    }

    void Start()
    {
        setHat(1);
        setHatColor(new Color(255, 0, 0, 255));

        setHair(1);
        setHairColor(new Color(137, 98, 67, 255));

        setEyes(0);

        setNose(0);
        setNoseColor(new Color(224, 183, 115, 255));

        setMouth(0); 

        setSkinColor(new Color(224, 183, 115, 255));

        setTop(0);
        setTopColor(new Color(210, 227, 119, 255));

        setBottom(0);
        setBottomColor(new Color(239, 209, 132, 255));

        setShoe(0);
        setShoeColor(new Color(0, 0, 255, 255));

    }
}
