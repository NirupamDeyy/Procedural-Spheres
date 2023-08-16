using UnityEngine;

public class SwipeRotate : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private bool isSwiping = false;
    private float rotationSpeed = 1f;

    void Update()
    {
        // Check for mouse or touch input
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }

        // Handle swipe rotation
        if (isSwiping && Input.GetMouseButton(0))
        {
            Vector2 currentSwipe = (Vector2)Input.mousePosition - startTouchPosition;
            float rotationAmountX = -currentSwipe.x * rotationSpeed * Time.deltaTime;
            float rotationAmountY = currentSwipe.y * rotationSpeed * Time.deltaTime;

            // Apply rotation to the object around the Y-axis (left and right) and X-axis (up and down)
            transform.Rotate(Vector3.up, rotationAmountX, Space.World);
            transform.Rotate(Vector3.right, rotationAmountY, Space.World);
        }
    }
}