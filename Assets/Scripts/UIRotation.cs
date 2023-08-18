using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotation : MonoBehaviour
{
    public Transform pivot; // The pivot point around which the rotation will occur
    public float rotationSpeed = 45f; // Speed of rotation in degrees per second

    private void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
