using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapPuzzleManager : MonoBehaviour
{
    [Header("Camera References")]
    public Camera mainCamera;
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

    void Start()
    {
        // Initialize UI elements
        interactText.SetActive(false);
        mapPuzzleUI.SetActive(false);
        instructionsText.SetActive(false);
        completionText.SetActive(false);
        puzzleCamera.gameObject.SetActive(false);

        // Setup reset button
        resetButton.onClick.AddListener(ResetPuzzle);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
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
        // Show completion text
        completionText.SetActive(true);

        // Play success sound if you have one
        // if (successSound != null)
        //     successSound.Play();

        yield return new WaitForSeconds(1.5f);

        // Animate the painting moving up
        if (paintingObject != null)
        {
            // Define the target position (move up)
            Vector3 startPosition = paintingObject.position;
            Vector3 endPosition = startPosition + Vector3.up * 2f; // Adjust the 2f to control how far up it moves

            // Simple smooth movement
            float duration = 1.0f;
            float elapsed = 0;

            while (elapsed < duration)
            {
                paintingObject.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure it reaches the exact end position
            paintingObject.position = endPosition;
        }

        // Make safe visible
        if (paintingObject != null)
        {
            safeObject.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1.0f);

        // Return to main view
        ExitPuzzleView();
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

    //private void DisablePlayerMovement()
    //{
    //    // Find player and disable movement components
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    if (player != null)
    //    {
    //        // Adjust these based on your specific character controller setup
    //        var characterController = player.GetComponent<CharacterController>();
    //        if (characterController != null)
    //            characterController.enabled = false;

    //        // First person controller (if using Standard Assets)
    //        var fpsController = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    //        if (fpsController != null)
    //            fpsController.enabled = false;
    //    }
    //}

    //private void EnablePlayerMovement()
    //{
    //    // Find player and enable movement components
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    if (player != null)
    //    {
    //        // Re-enable character controller
    //        var characterController = player.GetComponent<CharacterController>();
    //        if (characterController != null)
    //            characterController.enabled = true;

    //        // First person controller
    //        var fpsController = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    //        if (fpsController != null)
    //            fpsController.enabled = true;
    //    }
    //}
}