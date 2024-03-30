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
    FMOD.Studio.VCA sfxvca;
    FMOD.Studio.VCA musicvca;
    [SerializeField] private GameObject _mainMenuCanvasGObject;
    [SerializeField] private GameObject _settingsMenuCanvasGObject;
    // Default Menu Selection Assets
    [SerializeField] private GameObject _PauseMenuFirstSelection;
    [SerializeField] private GameObject _SettingsMenuFirstSelection;

    public TMP_Text helpTextPanel;
    public Transform buttonsPanel;
    public GameObject buttonPrefab;

    // List of buttons texts or FAQ Related
    List<string> buttonTexts = new List<string> { "Movement", "Gathering", "Service", "Crafting" }; 

    public Slider sfxSlider;
    public Slider musicSlider;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private bool isPaused;
    private void Awake()
    {
        // Set Sliders to PlayerPrefs values if they exist
        if (PlayerPrefs.HasKey("Sfx"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("Sfx");
            sfxvca.setVolume(DecibleToLinear(PlayerPrefs.GetFloat("Sfx")));
        }
        if (PlayerPrefs.HasKey("Bgm"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("Bgm");
            musicvca.setVolume(DecibleToLinear(PlayerPrefs.GetFloat("Bgm")));
        }
        // if playerrpefs have sfx or bgm set, set it to 0.5f
        if (!PlayerPrefs.HasKey("Sfx") || !PlayerPrefs.HasKey("Bgm"))
        {
            sfxSlider.value = 0.5f;
            musicSlider.value = 0.5f;
            sfxvca.setVolume(DecibleToLinear(0.5f));
            musicvca.setVolume(DecibleToLinear(0.5f));
            PlayerPrefs.SetFloat("Sfx", 0.5f);
            PlayerPrefs.SetFloat("Bgm", 0.5f);

        }

        EventSystem.current.SetSelectedGameObject(_PauseMenuFirstSelection);
    }
    private void Start()
    {
        _mainMenuCanvasGObject.SetActive(false);
        _settingsMenuCanvasGObject.SetActive(false);
        GameManager.Instance.ChangeGameState(EGameState.MainState);

        GenerateButtons(); // FOR hELP MENU BUTTONS

        // --------------- Audio Settings ----------------
        sfxvca = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        musicvca = FMODUnity.RuntimeManager.GetVCA("vca:/BGM");

        // --------------- Display Resolution ----------------

        // Create a list of resolution options
        InitializeResolutionOptions();
        if (PlayerPrefs.HasKey("Resolution"))
        {
            if (PlayerPrefs.GetInt("Resolution") == 0)
            {
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                resolutionDropdown.captionText.text = "1080p";
            }
            else if (PlayerPrefs.GetInt("Resolution") == 1)
            {
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                resolutionDropdown.captionText.text = "720p";
            }
        }
    

        // Set the initial fullscreen mode based on the current state unless playerprefs has a value
        if (!PlayerPrefs.HasKey("Fullscreen"))
        {
            fullscreenToggle.isOn = Screen.fullScreen;
        }
        else
        {
            fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        }
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
                PlayerPrefs.SetInt("Resolution", 0);
                resolutionDropdown.captionText.text = "1080p";
                break;
            case 1: // 1280x720
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                PlayerPrefs.SetInt("Resolution", 1);
                resolutionDropdown.captionText.text = "720p";
                break;
            default:
                Debug.LogError("Invalid resolution index!");
                break;
        }
        PlayerPrefs.Save(); // Save the resolution to playerprefs
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // save this choice to playerprefs
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(isFullscreen));
        PlayerPrefs.Save();
        Screen.fullScreen = isFullscreen;
    }


    public void SetSfx()
    {
        sfxvca.setVolume(DecibleToLinear(sfxSlider.value));

        // Save volume to PlayerPrefs (idk if this will be useful)
        PlayerPrefs.SetFloat("Sfx", sfxSlider.value); // Save volume to PlayerPrefs
        PlayerPrefs.Save();
    }

    public void SetMusic()
    {
        musicvca.setVolume(DecibleToLinear(musicSlider.value));

        // Save volume to PlayerPrefs (idk if this will be useful) 
        PlayerPrefs.SetFloat("Bgm", musicSlider.value); // Save volume to PlayerPrefs
        PlayerPrefs.Save();
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
                helpTextPanel.text = "You can see recipies to craft by pressing `G`. \n\nUse `Q` to play the selected card at the crafting station. \n\nUse `E` to craft items.";
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
