using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class MenuScript : MonoBehaviour
{
    FMOD.Studio.Bus bus;
    [SerializeField] private GameObject _mainMenuCanvasGObject;
    [SerializeField] private GameObject _settingsMenuCanvasGObject;
    // Default Menu Selection Assets
    [SerializeField] private GameObject _PauseMenuFirstSelection;
    [SerializeField] private GameObject _SettingsMenuFirstSelection;

    public TMP_Text helpTextPanel;
    public Transform buttonsPanel;
    public GameObject buttonPrefab;
    List<string> buttonTexts = new List<string> { "Movement", "Gathering", "Service", "Crafting" }; // List of buttons texts or FAQ Related

    public Slider volumeSlider;
    public Toggle muteToggle;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private bool isPaused;
    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(_PauseMenuFirstSelection);
    }
    private void Start()
    {
        _mainMenuCanvasGObject.SetActive(false);
        _settingsMenuCanvasGObject.SetActive(false);
        GameManager.Instance.ChangeGameState(EGameState.MainState);

        GenerateButtons(); // FOR hELP MENU BUTTONS

        // --------------- Audio Settings ----------------
        bus = FMODUnity.RuntimeManager.GetBus("bus:/");

        // --------------- Display Resolution ----------------

        // Create a list of resolution options
        InitializeResolutionOptions();

        // Set the initial fullscreen mode based on the current state
        fullscreenToggle.isOn = Screen.fullScreen;

        // Add listeners for value changes in the dropdown and toggle
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        fullscreenToggle.onValueChanged.AddListener(delegate { SetFullscreen(fullscreenToggle.isOn); });
    }

    private float DecibleToLinear(float db)
    {
        float linear = Mathf.Pow(10.0f, db / 20.0f);
        return linear;
    }

    void InitializeResolutionOptions()
    {
        // Create a list of resolution options
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(new List<string> { "1920x1080", "1280x720" });
    }

    public void SetResolution(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0: // 1920x1080
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1: // 1280x720
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            default:
                Debug.LogError("Invalid resolution index!");
                break;
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume()
    {
        bus.setVolume(DecibleToLinear(volumeSlider.value));

        // Save volume to PlayerPrefs (idk if this will be useful)
        PlayerPrefs.SetFloat("Volume", volumeSlider.value); // Save volume to PlayerPrefs
    }

    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            bus.setVolume(0);
            AudioListener.volume = 0; // Mute audio
        }
        else
        {
            bus.setVolume(DecibleToLinear(volumeSlider.value));
            AudioListener.volume = volumeSlider.value; // Unmute audio
        }

        //save mute state
        PlayerPrefs.SetInt("Mute", Convert.ToInt32(isMuted));
    }

    private void GenerateButtons()
    {
        foreach (string buttonText in buttonTexts)
        {
            // Instantiate button prefab
            GameObject newButton = Instantiate(buttonPrefab, buttonsPanel);

            // Set button text
            newButton.GetComponentInChildren<TMP_Text>().text = buttonText;

            // Add click event listener
            Button buttonComponent = newButton.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnButtonClick(buttonText));
        }
    }

    private void OnButtonClick(string buttonText)
    {

        switch (buttonText)
        {
            case "Movement":
                helpTextPanel.text = "You can move your player with WASD on your keyboard.";
                break;
            case "Gathering":
                helpTextPanel.text = "You can gather by holding E on resources.";
                break;
            case "Service":
                helpTextPanel.text = "Press `TAB` to open inventory. During service your Deck of Cards is locked. Make sure you have everything you need before service. Drag cards from your inventory to your deck to use them. Click start service to begin.";
                break;
            case "Crafting":
                helpTextPanel.text = "You can see recipies to craft by pressing `G`. Use `Q` to play the selected card at the crafting station. Use `E` to craft items.";
                break;

            // add more...

            default:
                Debug.LogWarning("Button text not recognized.");
                break;
        }
    }

    public void PauseMenuSelection()
    {
        EventSystem.current.SetSelectedGameObject(_PauseMenuFirstSelection);
    }
    public void SettingsMenuFirstSelection()
    {
        EventSystem.current.SetSelectedGameObject(_SettingsMenuFirstSelection);
    }
}
