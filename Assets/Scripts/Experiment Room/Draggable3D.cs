using UnityEngine;

public class Draggable3D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public Camera dragCamera;

    void Start()
    {
        if (dragCamera == null)
        {
            Debug.LogError("No camera assigned to Draggable3D! Assign a camera in the Inspector.");
        }
    }

    void OnMouseDown()
    {
        if (dragCamera == null) return; // Prevent errors

        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging && dragCamera != null)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (dragCamera == null) return Vector3.zero; // Prevent errors

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragCamera.WorldToScreenPoint(transform.position).z; // Keep depth consistent
        return dragCamera.ScreenToWorldPoint(mousePos);
    }
}