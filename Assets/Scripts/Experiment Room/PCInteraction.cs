using UnityEngine;

public class PCInteraction : MonoBehaviour
{
    public float interactionDistance = 3f; // Adjust this based on your scene
    public LayerMask interactableLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer)) // Check against interactable layer only
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject == gameObject) // Check if it's the PC monitor
                {
                    Debug.Log("PC Monitor Clicked!");
                    OpenPuzzle();
                }
            }
            else
            {
                Debug.Log("Ray didn't hit anything in the interactable layer!");
            }
        }
    }

    void OpenPuzzle()
    {
        Debug.Log("Opening Computer Puzzle...");
        // Add logic to transition to 2D puzzle view
    }
}
