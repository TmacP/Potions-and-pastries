

using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class InkTutorial : MonoBehaviour {
	static public InkTutorial instance;
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
			image = GetComponent<Image>();
			image.enabled = false;
			RemoveChildren();
			StartStory();		
        }

    }


    // Creates a new Story object with the compiled story which we can then play!
    void StartStory () {

		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
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



// WE CONTINUE THE STORY WHEN TALK TO NPC *****************
public void ContinueStory(NPCData npcData)
{
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

	[SerializeField] private TextAsset inkJSONAsset = null;
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
