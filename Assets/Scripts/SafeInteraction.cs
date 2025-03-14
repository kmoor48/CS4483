using UnityEngine;
using TMPro; // For TextMesh Pro

public class SafeInteraction : MonoBehaviour
{
    public GameObject safeCanvas; // Reference to the lock canvas
    public Camera playerCamera;
    public Camera safeCamera;
    public GameObject safePrefab; // The actual lock object prefab
    public float interactionDistance = 3f; // Distance at which the player can interact with the lock

    public GameObject interactionPrompt; // Interaction prompt (text) to show when near lock

    private bool isInteracting = false;
    private bool isNearLock = false; // Check if the player is close to the lock

    void Start()
    {
        // Make sure the lock canvas and prompt are disabled at the start
        safeCanvas.SetActive(false);
        interactionPrompt.SetActive(false); // Hide the interaction prompt initially
    }

    void Update()
    {
        // Check if the player is near the lock
        float distanceToLock = Vector3.Distance(playerCamera.transform.position, transform.position);
        if (distanceToLock <= interactionDistance)
        {
            if (!isNearLock)
            {
                isNearLock = true;
                interactionPrompt.SetActive(true); // Show interaction prompt when near lock
            }

            // Check for P key press to start the interaction
            if (Input.GetKeyDown(KeyCode.P) && !isInteracting)
            {
                EnterSafe(); // Enter puzzle mode
            }
        }
        else
        {
            if (isNearLock)
            {
                isNearLock = false;
                interactionPrompt.SetActive(false); // Hide the interaction prompt when moving away
            }
        }

        // If interacting with the lock, allow exiting with Escape key
        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitSafe(); // Exit the puzzle
        }
    }

    //public void EnterSafe()
    //{
    //    isInteracting = true;
    //    UIInteractionManager.Instance.EnableUIInteraction(); // Enable UI interaction

    //    // Switch to the lock camera and show the canvas
    //    playerCamera.gameObject.SetActive(false);
    //    safeCamera.gameObject.SetActive(true);
    //    safeCanvas.SetActive(true);
    //    interactionPrompt.SetActive(false); // Hide prompt after starting puzzle mode
    //}
    public void EnterSafe()
    {
        isInteracting = true;
        UIInteractionManager.Instance.EnableUIInteraction();

        // Unlock and show cursor so player can move sliders
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Switch to the safe camera and show the canvas
        playerCamera.gameObject.SetActive(false);
        safeCamera.gameObject.SetActive(true);
        safeCanvas.SetActive(true);
        interactionPrompt.SetActive(false);
    }


    //public void ExitSafe()
    //{
    //    isInteracting = false;
    //    UIInteractionManager.Instance.DisableUIInteraction(); // Disable UI interaction

    //    // Switch back to the player camera and hide the canvas
    //    playerCamera.gameObject.SetActive(true);
    //    safeCamera.gameObject.SetActive(false);
    //    safeCanvas.SetActive(false);
    //}

    public void ExitSafe()
    {
        isInteracting = false;
        UIInteractionManager.Instance.DisableUIInteraction();

        // Lock cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Switch back to player camera and hide canvas
        playerCamera.gameObject.SetActive(true);
        safeCamera.gameObject.SetActive(false);
        safeCanvas.SetActive(false);
    }


}