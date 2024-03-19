using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PauseScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    [SerializeField] private List<string> menuItems ;

    private VisualElement rootEl;
    private VisualElement _startEl;
    private VisualElement menuItemEl;
    
     private string activeMenuItemClass = "menu-item-active";
     private string activeClass = "start-active";

    private int selectedIndex = 0;
    

    public void showMenu(){
        Invoke("Open", 0.3f);
    }
    

    
     private void Update()
    {


        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            HandleDown();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            HandleUp();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            HandleSelect();
        }
    }

    private void HandleUp()
    {
        if (selectedIndex == 0)
        {
            selectedIndex = menuItems.Count - 1;
        }
        else
        {
            selectedIndex--;
        }

        SelectSelected();
    }

    private void HandleDown()
    {
        if (selectedIndex == menuItems.Count - 1)
        {
            selectedIndex = 0;
        }
        else
        {
            selectedIndex++;
        }

        SelectSelected();
    }
     private void SelectSelected()
    {
        rootEl.Q(className: activeMenuItemClass).RemoveFromClassList(activeMenuItemClass);
        rootEl.Query(className: "menu-item").AtIndex(selectedIndex).AddToClassList(activeMenuItemClass);
    }

    private void HandleSelect()
    {
        string selectedItem = menuItems[selectedIndex];
        Debug.Log($"{selectedItem} has been selected.");
        if (selectedItem == "Quit")
        {
            Application.Quit();
        }
        if (selectedItem == "New Game")
        {
            //change scene
            SceneManager.LoadScene("AlphaExterior");

        }
    }


    private void buildMenu (){
        int currentIdx = 0;

        foreach (string item in menuItems)
        {
            menuItemEl. Add(BuildMenuItem(item, currentIdx == 0, currentIdx != 0));
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

    public void Open()
    {
        Debug.Log("Opening the pause menu.");
        if (_startEl != null)
        {
            _startEl.AddToClassList(activeClass);
        }
        else
        {
            Debug.LogError("_startEl is null! Check initialization.");
        }
    }

    public void Close()
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


    private VisualElement BuildMenuItem(string text, bool active, bool spaceTop) {
    VisualElement menuItem = new VisualElement();
    menuItem.AddToClassList("menu-item");

    if (active) menuItem.AddToClassList("menu-item-active");
    if (spaceTop) menuItem.AddToClassList("space-top");

    // Create a button
    Button button = new Button();
    button.AddToClassList("menu-item-button");
    button.clickable.clicked += () => HandleMenuItemClick(text); // Handle click event

    // Create label for button text
    Label label = new Label(text);
    label.AddToClassList("menu-item-label");

    // Add label to button
    button.Add(label);

    // Add button to menu item
    menuItem.Add(button);

    return menuItem;
}

private void HandleMenuItemClick(string itemName) {
    Debug.Log($"Clicked on menu item: {itemName}");

    // Add your logic here based on the clicked menu item
    if (itemName == "Quit") {
        Application.Quit();
    } else if (itemName == "New Game") {
        SceneManager.LoadScene("AlphaExterior");
    }
    // Add more conditions as needed
}
}
