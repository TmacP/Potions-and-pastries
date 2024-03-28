using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGlobalScale : MonoBehaviour
{

    public Vector3 GlobalScale = Vector3.one;
    public bool GlobalX = true;
    public bool GlobalY = true;
    public bool GlobalZ = true;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent)
        {
            Vector3 parentScale = transform.parent.localScale;

            Vector3 NewScale = Vector3.one;

            if (GlobalX)
                NewScale.x = transform.localScale.x * transform.localScale.x / parentScale.x;
                
            if(GlobalY)
                NewScale.y = transform.localScale.y * transform.localScale.x / parentScale.y;
                
            if(GlobalZ)
                NewScale.z = transform.localScale.z * transform.localScale.z / parentScale.z;
        }
    }
}
