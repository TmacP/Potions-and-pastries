using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    // Rotation speed of the windmill
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the windmill on the x-axis
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
