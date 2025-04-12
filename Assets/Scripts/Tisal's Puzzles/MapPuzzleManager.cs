using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapPuzzleManager : MonoBehaviour
{
    [Header("Camera References")]
    private Camera mainCamera;
    public Camera puzzleCamera;

    [Header("UI Elements")]
    public GameObject mapPuzzleUI;
    public GameObject interactText;
    public GameObject instructionsText;
    public GameObject completionText;
    public Button resetButton;

    [Header("Puzzle Settings")]
    public List<ProvinceButton> provinceButtons;
    public GameObject linePrefab;
    public GameObject lineContainer;
    public Transform safeObject;
    public Transform paintingObject;
   // public AudioSource successSound;

    [Header("Journal References")]
    // The correct order of provinces from the journal entries found in Puzzle 1
    public List<string> correctOrder = new List<string>()
    { "NovaScotia", "Ontario", "Alberta", "Yukon", "BritishColumbia" };

    // Runtime variables
    private bool inReach = false;
    private bool interactingWithMap = false;
    private ProvinceButton selectedProvince = null;
    private List<ConnectionLine> connections = new List<ConnectionLine>();
    private int currentLineNumber = 1;
    private GameObject universalLogicHandler;


    void Start()
    {
        mainCamera = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<Camera>();

        // Initialize UI elements
        interactText.SetActive(false);
        mapPuzzleUI.SetActive(false);
        instructionsText.SetActive(false);
        completionText.SetActive(false);
        puzzleCamera.gameObject.SetActive(false);
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");


        // Setup reset button
        resetButton.onClick.AddListener(ResetPuzzle);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            interactText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && !interactingWithMap)
        {
            EnterPuzzleView();
        }

        if (interactingWithMap && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPuzzleView();
        }
    }

    public void ProvinceSelected(ProvinceButton province)
    {
        if (selectedProvince == null)
        {
            // First province selection
            selectedProvince = province;
            province.SetHighlighted(true);
        }
        else if (selectedProvince != province)
        {
            // Second province selection - create connection
            CreateConnection(selectedProvince, province);
            selectedProvince.SetHighlighted(false);
            selectedProvince = null;
        }
        else
        {
            // Deselect if clicked on the same province
            selectedProvince.SetHighlighted(false);
            selectedProvince = null;
        }
    }

    private void CreateConnection(ProvinceButton from, ProvinceButton to)
    {
        // Check if we already have 4 connections (since 5 provinces need 4 lines)
        if (connections.Count >= 4)
        {
            Debug.Log("Maximum number of connections reached");
            return;
        }

        Debug.Log("Creating connection between: " + from.provinceName + " and " + to.provinceName);

        // Create the visual line between provinces
        GameObject lineObj = Instantiate(linePrefab, lineContainer.transform);

        ConnectionLine connection = lineObj.GetComponent<ConnectionLine>();

        if (connection != null)
        {
            // Setup the connection
            connection.Initialize(from, to, currentLineNumber.ToString());

            // Store the connection
            connections.Add(connection);

            // Increment line number
            currentLineNumber++;

            // Check for puzzle completion after 4 connections
            if (connections.Count == 4)
            {
                CheckSolution();
            }
        }
        else
        {
            Debug.LogError("ConnectionLine component not found on prefab: " + lineObj.name);
        }
    }

    private void CheckSolution()
    {
        bool isCorrect = true;

        // Debug the current state
        Debug.Log("Checking solution with " + connections.Count + " connections");

        // We need at least 4 connections to connect 5 provinces
        if (connections.Count < 4)
        {
            Debug.LogWarning("Not enough connections to check solution. Needed: 4, Current: " + connections.Count);
            return;
        }

        // Check if the provinces are connected in the correct order
        List<string> connectionOrder = new List<string>();

        // Add first province of first connection
        connectionOrder.Add(connections[0].GetStartProvince().provinceName);

        // Add end province of each connection
        foreach (ConnectionLine conn in connections)
        {
            connectionOrder.Add(conn.GetEndProvince().provinceName);
        }

        // Now we should have a list of provinces in the order they were connected
        Debug.Log("Connection Order: " + string.Join(" -> ", connectionOrder));
        Debug.Log("Correct Order: " + string.Join(" -> ", correctOrder));

        // Compare with correct order
        if (connectionOrder.Count != correctOrder.Count)
        {
            Debug.LogWarning("Connection count doesn't match correct order");
            isCorrect = false;
        }
        else
        {
            for (int i = 0; i < connectionOrder.Count; i++)
            {
                if (connectionOrder[i] != correctOrder[i])
                {
                    Debug.LogWarning($"Mismatch at index {i}: Expected {correctOrder[i]}, Got {connectionOrder[i]}");
                    isCorrect = false;
                    break;
                }
            }
        }

        Debug.Log("Solution is " + (isCorrect ? "CORRECT" : "INCORRECT"));

        if (isCorrect)
        {
            // Success!
            Debug.Log("Puzzle Solved Successfully!");
            completionText.SetActive(true);
            instructionsText.GetComponent<TMPro.TextMeshProUGUI>().text = "Correct! You've solved the puzzle.";

            // Uncomment when you're ready to implement the safe reveal
            StartCoroutine(RevealSafe());
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();
        }
        else
        {
            // Show feedback for incorrect solution
            Debug.LogWarning("Puzzle solution is incorrect");
            instructionsText.GetComponent<TMPro.TextMeshProUGUI>().text = "Incorrect sequence. Try again.";
        }
    }

    private System.Collections.IEnumerator RevealSafe()
    {
        // Exit puzzle view to ensure player is in main view
        ExitPuzzleView();

        // Temporarily disable player movement
        DisablePlayerMovement();

        // Force player to look at the painting
        RotatePlayerToFacePainting();

        // Wait a moment to let the player focus
        yield return new WaitForSeconds(1.5f);

        // Hide the painting
        if (paintingObject != null)
        {
            paintingObject.gameObject.SetActive(false);
        }

        // Make safe visible
        if (safeObject != null)
        {
            safeObject.gameObject.SetActive(true);
        }

        // Wait a moment to let the player see the revealed safe
        yield return new WaitForSeconds(1.0f);

        // Re-enable player movement
        EnablePlayerMovement();
    }

    // You'll need to implement these methods based on your specific player and camera setup
    private void DisablePlayerMovement()
    {
        // Find player and disable movement components
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Disable character controller or first-person controller
            var characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
                characterController.enabled = false;

            // Add any other movement-related component disabling here
        }
    }

    private void EnablePlayerMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
                characterController.enabled = true;

            // Re-enable any other movement-related components
        }
    }

    private void RotatePlayerToFacePainting()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && paintingObject != null)
        {
            // Calculate direction from player to painting
            Vector3 directionToPainting = (paintingObject.position - player.transform.position).normalized;

            // Only rotate around Y axis
            directionToPainting.y = 0;

            // Create a rotation that looks at the painting
            Quaternion lookRotation = Quaternion.LookRotation(directionToPainting);

            // Smoothly rotate the player
            player.transform.rotation = lookRotation;
        }
    }

    private void LockPlayerCamera()
    {
        // Implement camera locking logic
        // This might involve:
        // - Disabling camera rotation
        // - Freezing the camera's current rotation
    }

    private void UnlockPlayerCamera()
    {
        // Implement camera unlocking logic
        // Reverse the effects of LockPlayerCamera()
    }

    public void ResetPuzzle()
    {
        // Clear all connections
        foreach (ConnectionLine connection in connections)
        {
            Destroy(connection.gameObject);
        }
        connections.Clear();

        // Reset line number counter
        currentLineNumber = 1;

        // Clear selected province if any
        if (selectedProvince != null)
        {
            selectedProvince.SetHighlighted(false);
            selectedProvince = null;
        }

        // Reset instruction text
        instructionsText.GetComponent<TMPro.TextMeshProUGUI>().text = "Connect the provinces in the order mentioned in the journal entries.";
        // Hide completion text if visible
        completionText.SetActive(false);
    }

    private void EnterPuzzleView()
    {
        interactingWithMap = true;

        // Switch cameras
        mainCamera.gameObject.SetActive(false);
        puzzleCamera.gameObject.SetActive(true);

        // Show UI elements
        mapPuzzleUI.SetActive(true);
        instructionsText.SetActive(true);
        interactText.SetActive(false);

        // Reset puzzle state just in case
        ResetPuzzle();

        // Disable player movement - Adjust based on your character controller
        
    }

    private void ExitPuzzleView()
    {
        interactingWithMap = false;

        // Switch cameras back
        mainCamera.gameObject.SetActive(true);
        puzzleCamera.gameObject.SetActive(false);

        // Hide UI elements
        mapPuzzleUI.SetActive(false);
        instructionsText.SetActive(false);
        completionText.SetActive(false);

        // Show interaction text if still in range
        if (inReach)
        {
            interactText.SetActive(true);
        }

        // Enable player movement
        
    }


}