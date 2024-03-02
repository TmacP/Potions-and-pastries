using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvasGObject;
    [SerializeField] private GameObject _settingsMenuCanvasGObject;

    private bool isPaused;

    private void Start()
    {
        _mainMenuCanvasGObject.SetActive(false);
        _settingsMenuCanvasGObject.SetActive(false);
        GameManager.Instance.ChangeGameState(EGameState.MainState);

    }

    private void Update()
    {
        if (PlayerController.instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }

        }
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
        _mainMenuCanvasGObject.SetActive(true);
        _settingsMenuCanvasGObject.SetActive(true);
    }

    private void CloseMainMenu()
    {
        _mainMenuCanvasGObject.SetActive(false);
        _settingsMenuCanvasGObject.SetActive(false);
    }
}
