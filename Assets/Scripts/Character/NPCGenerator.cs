using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.U2D.Animation;

public class NPCGenerator : MonoBehaviour
{
    public int NPCCount = 0;
    public GameObject NPCPrefab;

    public int MaxLikes = 2;
    public int MinLikes = 2;
    public int MaxDislikes = 1;
    public int MinDislikes = 0;

    public List<CharacterColourPalette> CharacterColourPalettes;

    [HideInInspector]
    public List<NPCTracker> NPCS;

    // Start is called before the first frame update
    void Start()
    {

        GameEventManager.instance.OnChangeGameState += OnChangeGameState;

    }

    public void OnDisable()
    {
        GameEventManager.instance.OnChangeGameState -= OnChangeGameState;
    }

    public void OnChangeGameState(EGameState NewState, EGameState OldState)
    {
        if(NewState == EGameState.NightState && OldState == EGameState.MainState) 
        {
            //TrySpawn();
            InvokeRepeating("TrySpawn", 0.5f, 7.5f);
        }
    }

    public void TrySpawn()
    {
        if(NPCS.Count > GameManager.Instance.PersistantGameState.RoomsUnlocked)
        {
            return;
        }

        GameObject GO = Instantiate(NPCPrefab, this.transform.position, Quaternion.identity);

        NPCBehaviour NPC = GO.GetComponent<NPCBehaviour>();
        NPCTracker Tracker = GO.GetComponent<NPCTracker>();
        CharacterCustomization characterCustomization = GO.GetComponent<CharacterCustomization>();
        Tracker.Behavior = NPC;
        Tracker.generator = this;
        Tracker.Customization = characterCustomization;
        NPC.CharacterData = GenerateNPCData(null);
        Assert.IsNotNull(NPC);
        Assert.IsNotNull(Tracker);
        Assert.IsNotNull(characterCustomization);

        
        NPCS.Add(Tracker);
        Tracker.Init();
    }

    public void RemoveNPC(NPCTracker NPC)
    {
        NPCS.Remove(NPC);
    }

    public NPCCharacterData GenerateNPCData(NPCData TemplateData = null)
    {
        NPCCharacterData Data = new NPCCharacterData(TemplateData);

        int NameIndex = UnityEngine.Random.Range(0, fantasyNames.Length);
        Data.Name = fantasyNames[NameIndex];


        //TODO Gaurentee UNIQUE Archetype Index
        int ArchetypeIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ENPCArchetype)).Length);
        Data.Archetype = (ENPCArchetype)ArchetypeIndex;
        Data.Friendship = 1;


        //TODO Get ONLY Unlocked Tags
        List<EItemTags> Tags = Enum.GetValues(typeof(EItemTags)).Cast<EItemTags>().ToList();
        int LikeCount = UnityEngine.Random.Range(MinLikes, MaxLikes + 1);
        int DislikeCount = UnityEngine.Random.Range(MinDislikes, MaxDislikes + 1);

        for(int i = 0; i < LikeCount; i++)
        {
            EItemTags Tag = Tags[UnityEngine.Random.Range(0, Tags.Count)];
            Data.NPCLikes.Add(Tag);
            Tags.Remove(Tag);
        }

        List<EItemTags> MandatoryLikes = new List<EItemTags>()
        {
            EItemTags.Buttery,
            EItemTags.Doughy,
            EItemTags.Sweet,
            EItemTags.Salty
        };
        bool IsPresent = Data.NPCLikes.Any(t => MandatoryLikes.Contains(t));
        if(!IsPresent)
        {
            EItemTags Tag = MandatoryLikes[UnityEngine.Random.Range(0, MandatoryLikes.Count)];
            Data.NPCLikes.Add(Tag);
            Tags.Remove(Tag);
        }

        for (int i = 0; i < DislikeCount; i++)
        {
            EItemTags Tag = Tags[UnityEngine.Random.Range(0, Tags.Count)];
            Data.NPCDislikes.Add(Tag);
            Tags.Remove(Tag);
        }

        


        //***********SPRITE CUSTOMIZATION**********************

        List<ECharacterSpriteAssetSlots> Slots = NPCGenerationData.SpriteLibrary.Keys.ToList();

        Assert.IsNotNull(CharacterColourPalettes);
        Assert.IsTrue(CharacterColourPalettes.Count > 0);
        CharacterColourPalette colours = CharacterColourPalettes[0];

        foreach(var KV in NPCGenerationData.SpriteLibrary)
        {
            ECharacterSpriteAssetSlots key = KV.Key;
            List<string> Options = KV.Value;

            int OptionIndex = UnityEngine.Random.Range(0, Options.Count);
            Color color = Color.black;
            int i = -1;
            int j = -1;
            switch (key)
            {
                case ECharacterSpriteAssetSlots.Hair:
                    i = UnityEngine.Random.Range(0, colours.Hair.Count);
                    j = UnityEngine.Random.Range(0, colours.Hair[i].colors.Count);
                    color = colours.Hair[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Hat:
                    i = UnityEngine.Random.Range(0, colours.Hat.Count);
                    j = UnityEngine.Random.Range(0, colours.Hat[i].colors.Count);
                    color = colours.Hat[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Eye:
                    i = UnityEngine.Random.Range(0, colours.Eye.Count);
                    j = UnityEngine.Random.Range(0, colours.Eye[i].colors.Count);
                    color = colours.Eye[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Nose:
                    i = UnityEngine.Random.Range(0, colours.Nose.Count);
                    j = UnityEngine.Random.Range(0, colours.Nose[i].colors.Count);
                    color = colours.Nose[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Torso:
                    i = UnityEngine.Random.Range(0, colours.Torso.Count);
                    j = UnityEngine.Random.Range(0, colours.Torso[i].colors.Count);
                    color = colours.Torso[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Arm:
                    i = UnityEngine.Random.Range(0, colours.Arm.Count);
                    j = UnityEngine.Random.Range(0, colours.Arm[i].colors.Count);
                    color = colours.Arm[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Bottom:
                    i = UnityEngine.Random.Range(0, colours.Bottom.Count);
                    j = UnityEngine.Random.Range(0, colours.Bottom[i].colors.Count);
                    color = colours.Bottom[i].colors[j];
                    break;
                case ECharacterSpriteAssetSlots.Shoe:
                    i = UnityEngine.Random.Range(0, colours.Shoe.Count);
                    j = UnityEngine.Random.Range(0, colours.Shoe[i].colors.Count);
                    color = colours.Shoe[i].colors[j];
                    break;
                default:
                    break;
            }
            color.a = 1.0f;
            Data.SpriteAssetData.Add(new fCharacterSpriteAssetData(key, Options[OptionIndex], color));
        }

        return Data;
    }


    static readonly string[] fantasyNames = {
        "Aelarion",
        "Thalindra",
        "Galdoril",
        "Eldrin",
        "Lorelei",
        "Draven",
        "Sylvaris",
        "Rhiannon",
        "Thorngrim",
        "Eowyn",
        "Galadriel",
        "Alduin",
        "Faelar",
        "Ariadne",
        "Dagmar",
        "Thornblade",
        "Frostbane",
        "Sylvanas",
        "Morwen",
        "Zephyr",
        "Thalara",
        "Elowen",
        "Seraphina",
        "Grimnir",
        "Arwyn",
        "Aurora",
        "Gandalf",
        "Lunara",
        "Vex",
        "Aurelia",
        "Sindarin",
        "Alaric",
        "Merlin",
        "Ithilien",
        "Eldamar",
        "Nyx",
        "Valerian",
        "Astraea",
        "Shadowfang",
        "Morgana",
        "Eirwyn",
        "Rowan",
        "Avalon",
        "Frostwolf",
        "Eirik",
        "Elara",
        "Artemis",
        "Thranduil",
        "Aurelius",
        "Lilith",
        "Serenity",
        "Caelum",
        "Faelwen",
        "Sable",
        "Zephyra",
        "Meridian",
        "Valkyrie",
        "Eldarion",
        "Isolde",
        "Eclipse",
        "Faolan",
        "Ophelia",
        "Elysium",
        "Azrael",
        "Lyra",
        "Thornheart",
        "Gwyneth",
        "Vespera",
        "Dawnstar",
        "Ravenlock",
        "Aurum",
        "Duskblade",
        "Mordecai",
        "Aurora",
        "Elvandar",
        "Calypso",
        "Aether",
        "Oberon",
        "Eirlys",
        "Sorin",
        "Lilith",
        "Baelor",
        "Thalassa",
        "Sylvara",
        "Elysia",
        "Ezra",
        "Dagmar",
        "Thalorien",
        "Aurelia",
        "Valorian",
        "Lyanna",
        "Eirian",
        "Ragnar",
        "Frostfall",
        "Sylas",
        "Celestia",
        "Thornwielder",
        "Elysium",
        "Aurora"
    };


}
