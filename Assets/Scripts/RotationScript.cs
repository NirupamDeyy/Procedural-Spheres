using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float rotationChangeSpeed = 10f;
    public float maxRotationAngle = 45f;

    private float currentRotationAngle = 0f;

    public bool rotate = false;

    private void Start()
    {
        rotate = false;
    }

    public void Update()
    {
        if (rotate)
        {
            Rotate();
        } 
    }

    public void RotateBoolControl()
    {
        if (rotate == true && rotate != false ) 
        {
            rotate = false; 
        }

        else if(rotate == false && rotate != true)
        {
            rotate = true;
        }
    }

    public void Rotate()
    {
        // Update the currentRotationAngle over time.
        currentRotationAngle += rotationChangeSpeed * Time.deltaTime;
        currentRotationAngle %= 360f;

        // Calculate the axis of rotation using trigonometry (sin and cos functions).
        float axisX = Mathf.Sin(currentRotationAngle * Mathf.Deg2Rad);
        float axisY = Mathf.Cos(currentRotationAngle * Mathf.Deg2Rad);

        // Create a Quaternion representing the rotating axis.
        Quaternion rotatingAxis = Quaternion.Euler(axisX * maxRotationAngle, axisY * maxRotationAngle, 0f);

        // Rotate the object around the changing axis.
        transform.rotation *= rotatingAxis;

        // Optional: Smoothly dampen the rotation speed if needed.
        //transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * rotatingAxis, Time.deltaTime * rotationSpeed);
    }
}