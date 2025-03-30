using UnityEngine;

public class CameraMouseMovemnet : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public float leftLimit = -25f;
    public float rightLimit = 25f;
    public float upLimit = 10f;
    public float downLimit = -10f;

    private bool isActive = false; // Track if the bookshelf view is active
    private float currentYaw = 0f;  // Horizontal movement
    private float currentPitch = 0f; // Vertical movement

    void Update()
    {
        if (!isActive) return; // Stop movement if not in bookshelf view

        // Get mouse input for both X (left/right) and Y (up/down)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Update rotation with clamping
        currentYaw = Mathf.Clamp(currentYaw + mouseX, leftLimit, rightLimit);
        currentPitch = Mathf.Clamp(currentPitch - mouseY, downLimit, upLimit); // Inverted Y-axis

        // Apply rotation
        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    public void ActivateView(bool state)
    {
        isActive = state; // Enable/disable movement
    }
}
