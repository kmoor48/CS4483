using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    private float rotationX = 0f;

    void Update()
    {
        // Handle movement (WASD or arrow keys)
        float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveDirectionZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(moveDirectionX, 0f, moveDirectionZ); // Moves player object

        // Handle camera rotation (mouse movement)
        LookAround();
    }

    void LookAround()
    {
        // Get mouse input for horizontal (left-right) movement (affects player rotation)
        float mouseX = Input.GetAxis("Mouse X");

        // Get mouse input for vertical (up-down) movement (affects camera rotation)
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player horizontally (around the Y-axis)
        transform.Rotate(Vector3.up * mouseX * lookSpeedX);

        // Rotate the camera vertically (around the X-axis), clamped between -90 and 90 degrees
        rotationX -= mouseY * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Prevent looking too far up or down
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Apply vertical rotation to camera only
    }
}

