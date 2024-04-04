using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private List<string> menuItems = new List<string> { "New Game", "Continue", "Quit" };

    private VisualElement rootEl;
    private VisualElement _startEl;
    private VisualElement menuItemEl;

    private string activeMenuItemClass = "menu-item-active";
    private string activeClass = "start-active";

    private int selectedIndex = 0;
    private bool mouseOverMenuItem = false; 
    private bool keyboardMode = true; 

    private void Awake()
    {
        Invoke("Open", 0.3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            HandleDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            HandleUp();
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (keyboardMode)
            {
                HandleSelect();
            }
        }
    }

    private void HandleUp()
    {
        if (selectedIndex == -1)
        {
            selectedIndex = menuItems.Count - 1;
        }
        else
        {
            selectedIndex = (selectedIndex + menuItems.Count - 1) % menuItems.Count;
        }

        SelectSelected();
    }

    private void HandleDown()
    {
        if (selectedIndex == -1)
        {
            selectedIndex = 0;
        }
        else
        {
            selectedIndex = (selectedIndex + 1) % menuItems.Count;
        }

        SelectSelected();
    }

    private void SelectSelected()
    {
        rootEl.Q(className: activeMenuItemClass)?.RemoveFromClassList(activeMenuItemClass);
        if (selectedIndex >= 0)
        {
            rootEl.Query(className: "menu-item").AtIndex(selectedIndex)?.AddToClassList(activeMenuItemClass);
        }
    }

    private void HandleSelect()
    {
        if (selectedIndex >= 0 && selectedIndex < menuItems.Count)
        {
            string selectedItem = menuItems[selectedIndex];
            Debug.Log($"{selectedItem} has been selected.");
            if (selectedItem == "Quit")
            {
                Application.Quit();
            }
            else if (selectedItem == "Continue")
            {
                //GameManager.Instance.ChangeGameScene(EGameScene.InnExterior);
                //SceneManager.LoadScene("AlphaExterior");
                GameManager.Instance.LoadGame();
            }
            else if (selectedItem == "New Game")
            {
                GameManager.Instance.clearSave();
                GameManager.Instance.ChangeGameScene(EGameScene.Tutorial);
                //SceneManager.LoadScene("Tutorial");
            }
        }
    }

    private void OnMouseEnterMenuItem()
    {
        mouseOverMenuItem = true;
        if (!keyboardMode) // Only select if in mouse mode
        {
            VisualElement menuItem = rootEl.Query(className: "menu-item").AtIndex(menuItems.IndexOf(menuItems[selectedIndex]));
            if (menuItem != null)
            {
                menuItem.AddToClassList(activeMenuItemClass);
            }
        }
    }

    private void OnMouseLeaveMenuItem()
    {
        mouseOverMenuItem = false;

        rootEl.Q(className: activeMenuItemClass)?.RemoveFromClassList(activeMenuItemClass);
        if (keyboardMode)
        {
            selectedIndex = -1; // Deselect items 
        }
    }

    private void HandleMenuItemClick(string selectedItem)
    {
        selectedIndex = menuItems.IndexOf(selectedItem);
        HandleSelect();
    }

    private void buildMenu()
    {
        int currentIdx = 0;

        foreach (string item in menuItems)
        {
            menuItemEl.Add(BuildMenuItem(item, currentIdx == 0, currentIdx != 0));
            currentIdx++;
        }
    }

    private void OnEnable()
    {
        // Check if the UIDocument is valid
        if (uiDoc != null)
        {
            rootEl = uiDoc.rootVisualElement;
            _startEl = rootEl.Q(className: "start");
            menuItemEl = rootEl.Q(className: "menu-items");

            buildMenu();

        }
        else
        {
            Debug.LogError("UIDocument is not assigned! Please assign it in the Inspector.");
        }
    }

    private void Open()
    {
        if (_startEl != null)
        {
            _startEl.AddToClassList(activeClass);
        }
        else
        {
            Debug.LogError("_startEl is null! Check initialization.");
        }
    }

    private void Close()
    {
        if (_startEl != null)
        {
            _startEl.RemoveFromClassList(activeClass);
        }
        else
        {
            Debug.LogError("_startEl is null! Check initialization.");
        }
    }

    private VisualElement BuildMenuItem(string text, bool active, bool spaceTop)
    {
        VisualElement menuItem = new VisualElement();
        menuItem.AddToClassList("menu-item");

        if (active) menuItem.AddToClassList("menu-item-active");
        if (spaceTop) menuItem.AddToClassList("space-top");

        VisualElement textEl = new VisualElement();
        textEl.AddToClassList("menu-item-text");

        Label TextElementlabel = new Label(text);
        textEl.Add(TextElementlabel);
        textEl.RegisterCallback<MouseEnterEvent>(evt => {
            if (keyboardMode)
            {
                mouseOverMenuItem = true;
                selectedIndex = menuItems.IndexOf(text);
                SelectSelected();
                keyboardMode = false; // Mousable mode
            }
        });

        textEl.RegisterCallback<MouseLeaveEvent>(evt => {
            if (mouseOverMenuItem && selectedIndex == menuItems.IndexOf(text))
            {
                selectedIndex = -1;
                SelectSelected();
                keyboardMode = true; 
            }
        });

        
        textEl.RegisterCallback<ClickEvent>(evt => HandleMenuItemClick(text));

        menuItem.Add(textEl);

        return menuItem;
    }
}

