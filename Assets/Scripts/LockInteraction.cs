using UnityEngine;
using TMPro; // For TextMesh Pro

public class LockInteraction : MonoBehaviour
{
    public GameObject lockCanvas; // Reference to the lock canvas
    public Camera playerCamera;
    public Camera lockCamera;
    public GameObject lockPrefab; // The actual lock object prefab
    public GameObject successMessage; // Success message to be displayed when unlocked
    public GameObject closetDoorRight;
    public GameObject closetDoorLeft;
    public TMP_Text displayText; // UI text for displaying entered numbers
    public string correctCode = "8401"; // The correct code to unlock the lock
    public float interactionDistance = 3f; // Distance at which the player can interact with the lock

    public GameObject interactionPrompt; // Interaction prompt (text) to show when near lock

    private string enteredCode = ""; // Stores player's input
    private bool isInteracting = false;
    private bool isNearLock = false; // Check if the player is close to the lock

    void Start()
    {
        // Make sure the lock canvas and prompt are disabled at the start
        lockCanvas.SetActive(false);
        successMessage.SetActive(false); // Hide success message initially
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
                EnterLock(); // Enter puzzle mode
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
            ExitLock(); // Exit the puzzle
        }
    }

    public void EnterLock()
    {
        isInteracting = true;
        UIInteractionManager.Instance.EnableUIInteraction(); // Enable UI interaction

        // Switch to the lock camera and show the canvas
        playerCamera.gameObject.SetActive(false);
        lockCamera.gameObject.SetActive(true);
        lockCanvas.SetActive(true);
        interactionPrompt.SetActive(false); // Hide prompt after starting puzzle mode
    }

    public void ExitLock()
    {
        isInteracting = false;
        UIInteractionManager.Instance.DisableUIInteraction(); // Disable UI interaction

        // Switch back to the player camera and hide the canvas
        playerCamera.gameObject.SetActive(true);
        lockCamera.gameObject.SetActive(false);
        lockCanvas.SetActive(false);
        successMessage.SetActive(false); // Hide success message when exiting
        enteredCode = ""; // Reset the code when exiting
        displayText.text = ""; // Clear the display text
    }

    public void AddDigit(string digit)
    {
        if (enteredCode.Length < 4)
        {
            enteredCode += digit;
            displayText.text = enteredCode; // Display the entered code
        }
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            successMessage.SetActive(true); // Show success message
            Invoke("UnlockLock", 2f); // Show success message for 2 seconds before unlocking
        }
        else
        {
            enteredCode = ""; // Reset input
            displayText.text = "Incorrect"; // Show error message
            Invoke("ClearDisplay", 1.5f); // Reset display after 1.5 seconds
        }
    }

    void UnlockLock()
    {
        successMessage.SetActive(false); // Hide success message
        ExitLock(); // Exit the puzzle mode
        lockPrefab.SetActive(false); // Disable the lock object (unlock the door)
        closetDoorLeft.SetActive(false);
        closetDoorRight.SetActive(false);
    }

    void ClearDisplay()
    {
        displayText.text = ""; // Clear the display text
    }
}
