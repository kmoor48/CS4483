//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;
//using TMPro; // Add this at the top

//public class LockInteraction : MonoBehaviour
//{
//    public Camera playerCamera;
//    public Camera lockCamera;
//    public GameObject keypadCanvas; // UI for the keypad
//    public GameObject interactionPrompt; // UI prompt text
//    public Transform player;
//    public float interactionDistance = 2f;

//    private bool isNearLock = false;
//    private bool isPuzzleActive = false;
//    private string enteredCode = ""; // Stores player's input
//    private string correctCode = "8401"; // The correct code

//    public TMP_Text displayText; // UI text for displaying entered numbers
//    public GameObject successMessage; // UI text that says "Successfully Unlocked"
//    public GameObject lockObject; // The actual lock prefab

//    void Start()
//    {
//        lockCamera.gameObject.SetActive(false);
//        keypadCanvas.SetActive(false);
//        interactionPrompt.SetActive(false);
//        successMessage.SetActive(false);
//    }

//    void Update()
//    {
//        float distance = Vector3.Distance(player.position, transform.position);

//        if (distance <= interactionDistance)
//        {
//            if (!isNearLock)
//            {
//                interactionPrompt.SetActive(true);
//                isNearLock = true;
//            }

//            if (Input.GetKeyDown(KeyCode.P) && !isPuzzleActive)
//            {
//                EnterPuzzleMode();
//            }
//        }
//        else
//        {
//            if (isNearLock)
//            {
//                interactionPrompt.SetActive(false);
//                isNearLock = false;
//            }
//        }

//        if (isPuzzleActive && Input.GetKeyDown(KeyCode.Escape))
//        {
//            ExitPuzzleMode();
//        }
//    }

//    void EnterPuzzleMode()
//    {
//        isPuzzleActive = true;
//        playerCamera.gameObject.SetActive(false);
//        lockCamera.gameObject.SetActive(true);
//        keypadCanvas.SetActive(true);
//        interactionPrompt.SetActive(false);
//        enteredCode = ""; // Reset input
//        displayText.text = ""; // Clear display
//    }

//    void ExitPuzzleMode()
//    {
//        isPuzzleActive = false;
//        playerCamera.gameObject.SetActive(true);
//        lockCamera.gameObject.SetActive(false);
//        keypadCanvas.SetActive(false);
//    }

//    public void AddDigit(string digit)
//    {
//        if (enteredCode.Length < 4)
//        {
//            enteredCode += digit;
//            displayText.text = enteredCode;
//        }
//    }

//    public void SubmitCode()
//    {
//        if (enteredCode == correctCode)
//        {
//            successMessage.SetActive(true);
//            Invoke("UnlockDoor", 2f); // Show success message for 2 seconds before unlocking
//        }
//        else
//        {
//            enteredCode = ""; // Reset input
//            displayText.text = "Incorrect"; // Show error
//            Invoke("ClearDisplay", 1.5f); // Reset display after 1.5 seconds
//        }
//    }

//    void UnlockDoor()
//    {
//        successMessage.SetActive(false);
//        ExitPuzzleMode();
//        lockObject.SetActive(false); // Disable the lock prefab
//    }

//    void ClearDisplay()
//    {
//        displayText.text = "";
//    }
//}

//using UnityEngine;

//public class LockInteraction : MonoBehaviour
//{
//    public GameObject lockCanvas; // Reference to the lock canvas
//    public Camera playerCamera;
//    public Camera lockCamera;
//    public GameObject lockPrefab;
//    public float interactionDistance = 3f; // Distance at which the player can interact with the lock
//    private string enteredCode = "";
//    private bool isInteracting = false;
//    private bool isNearLock = false; // Check if the player is close to the lock

//    void Start()
//    {
//        // Make sure the lock canvas is disabled at the start
//        lockCanvas.SetActive(false);
//    }

//    void Update()
//    {
//        // Check if the player is near the lock
//        float distanceToLock = Vector3.Distance(playerCamera.transform.position, transform.position);
//        if (distanceToLock <= interactionDistance)
//        {
//            if (!isNearLock)
//            {
//                isNearLock = true;
//                // Optionally show a prompt on the screen to let the player know they can press P to interact
//                Debug.Log("Press 'P' to interact with the lock");
//            }

//            // Check for P key press to start the interaction
//            if (Input.GetKeyDown(KeyCode.P) && !isInteracting)
//            {
//                EnterLock(); // Enter puzzle mode
//            }
//        }
//        else
//        {
//            if (isNearLock)
//            {
//                isNearLock = false;
//                // Optionally hide the prompt when the player moves away from the lock
//                Debug.Log("You are too far from the lock to interact.");
//            }
//        }

//        // If interacting with the lock, allow exiting with Escape key
//        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
//        {
//            ExitLock(); // Exit the puzzle
//        }
//    }

//    public void EnterLock()
//    {
//        isInteracting = true;
//        UIInteractionManager.Instance.EnableUIInteraction(); // Enable UI interaction

//        // Switch to the lock camera and show the canvas
//        playerCamera.gameObject.SetActive(false);
//        lockCamera.gameObject.SetActive(true);
//        lockCanvas.SetActive(true);
//    }

//    public void ExitLock()
//    {
//        isInteracting = false;
//        UIInteractionManager.Instance.DisableUIInteraction(); // Disable UI interaction

//        // Switch back to the player camera and hide the canvas
//        playerCamera.gameObject.SetActive(true);
//        lockCamera.gameObject.SetActive(false);
//        lockCanvas.SetActive(false);
//    }

//    public void AddDigit(string digit)
//    {
//        if (enteredCode.Length < 4)
//        {
//            enteredCode += digit;
//        }
//    }

//    public void SubmitCode()
//    {
//        if (enteredCode == "8401")
//        {
//            Debug.Log("Successfully Unlocked!");
//            lockPrefab.SetActive(false); // Remove the lock
//            ExitLock(); // Exit the puzzle mode
//        }
//        else
//        {
//            Debug.Log("Wrong code!");
//            enteredCode = ""; // Reset input if the code is incorrect
//        }
//    }
//}
//using UnityEngine;
//using TMPro; // For TextMesh Pro

//public class LockInteraction : MonoBehaviour
//{
//    public GameObject lockCanvas; // Reference to the lock canvas
//    public Camera playerCamera;
//    public Camera lockCamera;
//    public GameObject lockPrefab; // The actual lock object prefab
//    public GameObject successMessage; // Success message to be displayed when unlocked
//    public TMP_Text displayText; // UI text for displaying entered numbers
//    public string correctCode = "8401"; // The correct code to unlock the lock
//    public float interactionDistance = 3f; // Distance at which the player can interact with the lock

//    private string enteredCode = ""; // Stores player's input
//    private bool isInteracting = false;
//    private bool isNearLock = false; // Check if the player is close to the lock

//    void Start()
//    {
//        // Make sure the lock canvas is disabled at the start
//        lockCanvas.SetActive(false);
//        successMessage.SetActive(false); // Hide success message initially
//    }

//    void Update()
//    {
//        // Check if the player is near the lock
//        float distanceToLock = Vector3.Distance(playerCamera.transform.position, transform.position);
//        if (distanceToLock <= interactionDistance)
//        {
//            if (!isNearLock)
//            {
//                isNearLock = true;
//                // Optionally show a prompt on the screen to let the player know they can press P to interact
//                Debug.Log("Press 'P' to interact with the lock");
//            }

//            // Check for P key press to start the interaction
//            if (Input.GetKeyDown(KeyCode.P) && !isInteracting)
//            {
//                EnterLock(); // Enter puzzle mode
//            }
//        }
//        else
//        {
//            if (isNearLock)
//            {
//                isNearLock = false;
//                // Optionally hide the prompt when the player moves away from the lock
//                Debug.Log("You are too far from the lock to interact.");
//            }
//        }

//        // If interacting with the lock, allow exiting with Escape key
//        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
//        {
//            ExitLock(); // Exit the puzzle
//        }
//    }

//    public void EnterLock()
//    {
//        isInteracting = true;
//        UIInteractionManager.Instance.EnableUIInteraction(); // Enable UI interaction

//        // Switch to the lock camera and show the canvas
//        playerCamera.gameObject.SetActive(false);
//        lockCamera.gameObject.SetActive(true);
//        lockCanvas.SetActive(true);
//    }

//    public void ExitLock()
//    {
//        isInteracting = false;
//        UIInteractionManager.Instance.DisableUIInteraction(); // Disable UI interaction

//        // Switch back to the player camera and hide the canvas
//        playerCamera.gameObject.SetActive(true);
//        lockCamera.gameObject.SetActive(false);
//        lockCanvas.SetActive(false);
//        successMessage.SetActive(false); // Hide success message when exiting
//        enteredCode = ""; // Reset the code when exiting
//        displayText.text = ""; // Clear the display text
//    }

//    public void AddDigit(string digit)
//    {
//        if (enteredCode.Length < 4)
//        {
//            enteredCode += digit;
//            displayText.text = enteredCode; // Display the entered code
//        }
//    }

//    public void SubmitCode()
//    {
//        if (enteredCode == correctCode)
//        {
//            successMessage.SetActive(true); // Show success message
//            Invoke("UnlockLock", 2f); // Show success message for 2 seconds before unlocking
//        }
//        else
//        {
//            enteredCode = ""; // Reset input
//            displayText.text = "Incorrect"; // Show error message
//            Invoke("ClearDisplay", 1.5f); // Reset display after 1.5 seconds
//        }
//    }

//    void UnlockLock()
//    {
//        successMessage.SetActive(false); // Hide success message
//        ExitLock(); // Exit the puzzle mode
//        lockPrefab.SetActive(false); // Disable the lock object (unlock the door)
//    }

//    void ClearDisplay()
//    {
//        displayText.text = ""; // Clear the display text
//    }
//}


using UnityEngine;
using TMPro; // For TextMesh Pro

public class LockInteraction : MonoBehaviour
{
    public GameObject lockCanvas; // Reference to the lock canvas
    public Camera playerCamera;
    public Camera lockCamera;
    public GameObject lockPrefab; // The actual lock object prefab
    public GameObject successMessage; // Success message to be displayed when unlocked
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
    }

    void ClearDisplay()
    {
        displayText.text = ""; // Clear the display text
    }
}
