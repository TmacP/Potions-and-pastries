using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuBehavoir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnCloseMenu += OnCloseMenu;
    }

    // Update is called once per frame
    void OnDisable()
    {
        GameEventManager.instance.OnCloseMenu -= OnCloseMenu;
    }

    public void CloseMenu()
    {
        GameEventManager.instance.CloseMenu();
    }

    void OnCloseMenu()
    {
        Destroy(this.gameObject);
    }
}
