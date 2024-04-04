using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnTableSpaceChangedBehavoir : MonoBehaviour
{
    public int Threshold = 4;
    public bool bShouldEnable = true;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnTableSpaceChanged += TableSpaceChanged;
        TableSpaceChanged();
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnTableSpaceChanged -= TableSpaceChanged;
    }

    // Update is called once per frame
    public void TableSpaceChanged()
    {
        if(GameManager.Instance.PersistantGameState.RoomsUnlocked >= Threshold)
        {
            if(Target != null)
            {
                Target.gameObject.SetActive(bShouldEnable);
            }
        }
    }
}
