using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChair : MonoBehaviour
{

    GameObject CurrentUser = null;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnNPCLeavingChair += EmptySeat;
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnNPCLeavingChair -= EmptySeat;
    }

    public bool isEmpty()
    {
        return CurrentUser == null;
    }

    public void FillSeat(GameObject User)
    {
        CurrentUser = User;
    }

    public void EmptySeat(GameObject User)
    {
        if(CurrentUser == User)
        {
            CurrentUser = null;
        }
    }

}
