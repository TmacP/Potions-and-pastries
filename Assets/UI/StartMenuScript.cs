using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    [SerializeField] private List<string> menuItems ;

    private VisualElement rootEl;
    private VisualElement _startEl;
    private VisualElement menuItemEl;
    
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
}
