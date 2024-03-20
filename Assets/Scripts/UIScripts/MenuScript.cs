using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

    //[SerializeField] private GameObject _mainMenuCanvasGObject;
    //[SerializeField] private GameObject _settingsMenuCanvasGObject;

    // Default Menu Selection Assets
    [SerializeField] private GameObject _PauseMenuFirstSelection;
    [SerializeField] private GameObject _SettingsMenuFirstSelection;


    private bool isPaused;
    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(_PauseMenuFirstSelection);
    }
    private void Start()
    {
        //_mainMenuCanvasGObject.SetActive(false);
        //_settingsMenuCanvasGObject.SetActive(false);
        GameManager.Instance.ChangeGameState(EGameState.MainState);

    }

    private void Pause()
    {
        isPaused = true;
        GameManager.Instance.ChangeGameState(EGameState.PauseState);
        OpenMainMenu();
    }

    private void Unpause()
    {
        isPaused = false;
        GameManager.Instance.ChangeGameState(EGameState.MainState);

        CloseMainMenu();

    }

    private void OpenMainMenu()
    {
        //_mainMenuCanvasGObject.SetActive(true);
       // _settingsMenuCanvasGObject.SetActive(true);
    }

    private void CloseMainMenu()
    {
        //_mainMenuCanvasGObject.SetActive(false);
        //_settingsMenuCanvasGObject.SetActive(false);
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
