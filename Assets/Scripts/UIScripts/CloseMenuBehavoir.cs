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

    void OnCloseMenu()
    {
        Destroy(this.gameObject);
    }
}
