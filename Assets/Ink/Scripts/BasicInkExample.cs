﻿using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {
	static public BasicInkExample instance;
	bool DebugMode = true;
    public static event Action<Story> OnCreateStory;

	private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            // Remove the default message
            if (HUD == null)
            {
				HUD = GameObject.FindGameObjectWithTag("PlayerHUD");
				Assert.IsNotNull(HUD);
				cardFront = GameObject.Find("UI_Deck");
                cardBack = GameObject.Find("UI_Discard");
				Assert.IsNotNull(cardFront);
				Assert.IsNotNull(cardBack);
            }

            image = GetComponent<Image>();
			image.enabled = false;
			RemoveChildren();
			StartStory();
			
			

        }

    }


    // Creates a new Story object with the compiled story which we can then play!
    void StartStory () {
		// if we are in the tutorial we load the tutorial story
		if (GameManager.Instance.GetGameScene() == EGameScene.Tutorial){
			if (DebugMode) {Debug.Log("Tutorial");}
			story = new Story (inkJSONAssetTutorial.text);
			}

		else if (GameManager.Instance.GetGameScene() == EGameScene.InnExterior){
					story = new Story (inkJSONAssetStory.text);
		}

        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {

        if (HUD == null)
        {
            HUD = GameObject.FindGameObjectWithTag("PlayerHUD");
            Assert.IsNotNull(HUD);
            cardFront = GameObject.Find("UI_Deck");
            cardBack = GameObject.Find("UI_Discard");
            Assert.IsNotNull(cardFront);
            Assert.IsNotNull(cardBack);
        }
        // Remove all the UI on screen
        RemoveChildren ();

		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {

			// pause game
			//GameManager.Instance.ChangeGameState(EGameState.MovementDisabledState);
			Time.timeScale = 0.0f;
			image.enabled = true;
			HUD.SetActive(false);
			cardFront.SetActive(false);
			cardBack.SetActive(false);

			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
	}

public void OpenGate(int GateID)
{
	if (DebugMode) {Debug.Log("OpenGate" + GateID);}
	if (GateID == 0) {story.variablesState["appleGate"] = true;}
	if (GateID == 1) {story.variablesState["cherryGate"] = true;}
	if (GateID == 2) {story.variablesState["vanillaGate"] = true;}
	if (GateID == 3) {story.variablesState["chocolateGate"] = true;}
}

// WE CONTINUE THE STORY WHEN TALK TO NPC *****************
public void ContinueStory(NPCCharacterData npcData)
{
	// here we set the variables in inkle for npc class friendship and name
	if (DebugMode) {
		Debug.Log("ContinueStory");
		Debug.Log("Class: " + npcData.Archetype.ToString() );
		Debug.Log("Friendship: " + npcData.Friendship);
		Debug.Log("Name: " + npcData.Name);
		Debug.Log("Day: " + GameManager.Instance.GameDay);
		}
	// set the variables in inkle
	story.variablesState["class"] = npcData.Archetype.ToString();
	story.variablesState["friendship"] = npcData.Friendship;
	story.variablesState["name"] = npcData.Name;
	story.variablesState["day"] = GameManager.Instance.GameDay;

	// next path string is to continue the story after a DONE
	string nextPathString = (string) story.variablesState["nextPathString"];
	if (DebugMode) {Debug.Log("nextPathString: = " + nextPathString);}
	story.ChoosePathString(nextPathString);
	RefreshView();
}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		if (DebugMode) {Debug.Log("Clicking choice: " + choice.text);}
		// if we click continue we change the game state to main state
		if (choice.text == "Continue") {
			if (DebugMode) {Debug.Log("Continue");}
			//GameManager.Instance.ChangeGameState(EGameState.MainState);
			Time.timeScale = 1.0f;
			image.enabled = false;
			HUD.SetActive(true);
			cardFront.SetActive(true);
			cardBack.SetActive(true);
			}
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
		// set background image

		Text storyText = Instantiate (textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);


	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);

		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	[SerializeField] private TextAsset inkJSONAssetTutorial = null;
	[SerializeField] private TextAsset inkJSONAssetStory = null;
	public Story story;
	[SerializeField] private Canvas canvas = null;
	// UI Prefabs
	[SerializeField] private Text textPrefab = null;
	[SerializeField] private Button buttonPrefab = null;

	[SerializeField] private GameObject talk = null;
	[SerializeField] private GameObject HUD = null;
	[SerializeField] private GameObject cardFront = null;
	[SerializeField] private GameObject cardBack = null;
	public Image image;

}
