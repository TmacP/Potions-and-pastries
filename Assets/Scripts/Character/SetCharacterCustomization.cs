using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SetCharacterCustomization : MonoBehaviour
{
    public string nameHat = "WizardHat";
    public Color colorHat = new Color(0.576f, 0.373f, 0.639f);

    // Hair/Brow variables 
    public string nameHair = "LongBangsHair";
    public Color colorHair = new Color(0.604f, 0.761f, 0.498f); 

    // Eye variables
    public string nameEye = "SleepyEye"; 

    // Nose variables
    public string nameNose = "LongTriangleNose";
    public Color colorNose = new Color(1f, 0.91f, 0.765f); 

    // Mouth variables 
    public string nameMouth = "FlatMouth";

    // Skin variables
    public Color colorSkin = new Color(1f, 0.91f, 0.765f); 

    // Top variables
    public string nameTorso = "PuffyTorso";
    public string nameArm = "PuffyArm";
    public Color colorTop = new Color(0.651f, 0.584f, 0.671f); 

    // Bottom variables 
    public string nameBottom = "JesterPants";
    public Color colorBottom = new Color(0.576f, 0.373f, 0.639f); 

    // Shoe variables
    public string nameShoes = "Slippers";
    public Color colorShoe = new Color(1f, 0.91f, 0.765f); 


    // Start is called before the first frame update
    void Start()
    {
        CharacterCustomization customization = GetComponent<CharacterCustomization>();
        Assert.IsNotNull(customization);
        // Testing purposes only, please delete "Start()" once ready to use 
        customization.setHat(nameHat);
        customization.setHatColor(colorHat);

        customization.setHair(nameHair);
        customization.setHairColor(colorHair);

        customization.setEyes(nameEye);

        customization.setNose(nameNose);
        customization.setNoseColor(colorNose);

        customization.setMouth(nameMouth);

        customization.setSkinColor(colorSkin);

        customization.setTorso(nameTorso);
        customization.setArm(nameArm);
        customization.setTopColor(colorTop);

        customization.setBottom(nameBottom);
        customization.setBottomColor(colorBottom);

        customization.setShoe(nameShoes);
        customization.setShoeColor(colorShoe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
