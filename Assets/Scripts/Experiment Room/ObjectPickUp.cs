using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private bool isHolding = false;
    private Vector3 originalPosition;
    public Camera puzzleCamera;

    void Start()
    {
        originalPosition = transform.position; // Store the starting position
    }

    void OnMouseDown()
    {
        if (!isHolding)
        {
            PickUp();
        }
        else
        {
            Drop();
        }
    }

    void PickUp()
    {
        isHolding = true;
    }

    void Drop()
    {
        isHolding = false;
        transform.position = originalPosition; // Place it back on the table
    }

    void Update()
    {
        if (isHolding)
        {
            // Get the mouse position in screen space (relative to your custom camera)
            Vector3 mousePosition = Input.mousePosition;

            // Set the z-value to the dropper's original z (or a fixed value)
            mousePosition.z = puzzleCamera.WorldToScreenPoint(transform.position).z;

            // Convert screen space to world space using your custom camera
            Vector3 worldPosition = puzzleCamera.ScreenToWorldPoint(mousePosition);

            // Update only x and z position, keeping y fixed at original
            transform.position = new Vector3(worldPosition.x, originalPosition.y, worldPosition.z);
        }
    }
}
