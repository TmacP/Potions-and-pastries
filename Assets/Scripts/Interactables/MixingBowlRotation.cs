using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingBowlRotation : MonoBehaviour
{
    public GameObject Spoon;

    void Update()
    {
        Spoon.transform.Rotate(Vector3.up, 50 * Time.deltaTime);
    }
}
