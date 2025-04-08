using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeManager : MonoBehaviour
{
    public GameObject openText; // UI text for interaction
    public Camera mainCamera; // The player's main camera
    public Camera puzzleCamera; // Camera to view the puzzle

    private bool inReach = false;
    private bool interactingWithPuzzle = false;

    void Start()
    {
        openText.SetActive(false);

        if (puzzleCamera != null)
            puzzleCamera.gameObject.SetActive(false); // Hide puzzle camera initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            openText.SetActive(true); // Show "Press E to interact" prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            openText.SetActive(false); // Hide text when leaving
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact")) // Check for interaction key
        {
            EnterPuzzleView(); // Switch to puzzle camera
        }

        if (interactingWithPuzzle && Input.GetKeyDown(KeyCode.Escape)) // Exit with Escape
        {
            ExitPuzzleView();
        }
    }

    private void EnterPuzzleView()
    {
        interactingWithPuzzle = true;

        // Switch to puzzle camera
        if (mainCamera != null && puzzleCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            puzzleCamera.gameObject.SetActive(true);
        }

        openText.SetActive(false); // Hide UI prompt when interacting
    }

    private void ExitPuzzleView()
    {
        interactingWithPuzzle = false;

        // Switch back to main camera
        if (mainCamera != null && puzzleCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
            puzzleCamera.gameObject.SetActive(false);
        }

        openText.SetActive(true); // Show prompt again if still in range
    }
}
