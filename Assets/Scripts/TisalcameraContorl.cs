using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of camera rotation
    private float yaw = 0f; // Horizontal rotation
    private float pitch = 0f; // Vertical rotation

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust yaw and pitch based on mouse movement
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Clamp pitch to prevent excessive rotation (optional)
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply the rotation to the camera
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
