using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PauseScript : MonoBehaviour
{
    
    [SerializeField] private UIDocument uiDoc;
    private List<string> menuItems = new List<string> { "Resume","Save", "Settings", "Help", "Quit" };

    private VisualElement rootEl;
    private VisualElement _startEl;
    private VisualElement menuItemEl;

    private VisualElement leftPanel;
    private VisualElement rightPanel;
    private Label helpTextLabel;
    
     private string activeMenuItemClass = "menu-item-active";
     private string activeClass = "start-active";

    private int selectedIndex = 0;
    private void Awake()
    {
        // Wait a half a sec and then run Open()
        Invoke("Open", 0.5f);

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
    else if (selectedItem == "Resume")
    {
        // Just close the menu
        PlayerController playerController = FindObjectOfType<PlayerController>();
        ClosefromMenu(playerController);
    }
    else if (selectedItem == "Settings")
    {
        // Clear the current menu and build the settings menu
        menuItems.Clear();
        menuItems.Add("Volume");
        menuItems.Add("Back");
        selectedIndex = 0;
        buildMenu();
    }
    else if (selectedItem == "Back")
    {
        // Clear the current menu and build the main menu
        menuItems.Clear();
        menuItems.AddRange(new List<string> { "Resume", "Save", "Settings", "Help", "Quit" });
        selectedIndex = 3; // Set it to the index of "Help"
        buildMenu();
    }
    else if (selectedItem == "Help")
    {
        // Clear the current menu and build the help menu
        menuItems.Clear();
        menuItems.AddRange(new List<string> { "Back" });
        selectedIndex = 0; // Set it to the index of "Movement"
        buildMenu();

        // Create two panels for the help menu
        CreateHelpPanels();
    }
}
private void CreateHelpPanels()
{
    // Create left panel with buttons
    leftPanel = new VisualElement();
    leftPanel.AddToClassList("help-panel-left");
    menuItemEl.Add(leftPanel);

    List<string> helpMenu = new List<string> { "Movement", "Gathering", "Service Day", "Back" };
    foreach (string item in helpMenu)
    {
        if (item != "Back")
        {
            Button button = new Button(() => UpdateHelpText(item));
            button.text = item;
            leftPanel.Add(button);
        }
    }

    // Create right panel with text
    rightPanel = new VisualElement();
    rightPanel.AddToClassList("help-panel");
    menuItemEl.Add(rightPanel);

    helpTextLabel = new Label();
    helpTextLabel.AddToClassList("help-text");
    rightPanel.Add(helpTextLabel);

    // Set initial help text
    UpdateHelpText(menuItems[0]);
}

private void UpdateHelpText(string buttonText)
{
    switch (buttonText)
    {
        case "Movement":
            helpTextLabel.text = "You can use WASD to move the character around.";
            break;
        case "Gathering":
            helpTextLabel.text = "Gathering resources helps in crafting items later.";
            break;
        case "Service Day":
            helpTextLabel.text = "Service days occur every Sunday for community service activities.";
            break;
        default:
            helpTextLabel.text = "Select an option from the left panel to view help text.";
            break;
    }
}



    private void buildMenu()
{
    menuItemEl.Clear();
    int currentIdx = 0;

    foreach (string item in menuItems)
    {
        menuItemEl.Add(BuildMenuItem(item, currentIdx == selectedIndex, currentIdx != 0));
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

    public void ClosefromMenu( PlayerController playerController)
    {
        if (_startEl != null)
        {
            _startEl.RemoveFromClassList(activeClass);
            playerController.OnPauseMenuClose();
            //set menu to deactivate

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

        

        VisualElement textEl = new VisualElement();
        textEl.AddToClassList("menu-item-text");

        Label TextElementlabel = new Label(text);

       
        menuItem.Add(textEl);
        textEl.Add(TextElementlabel);

        return menuItem;


    }

    public void showMenu(){
        Invoke("Open", 0.3f);
    }

    
}
