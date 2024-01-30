using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject mainInventoryGroup; // Assign this in the inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mainInventoryGroup.SetActive(!mainInventoryGroup.activeSelf);
        }
    }
}
